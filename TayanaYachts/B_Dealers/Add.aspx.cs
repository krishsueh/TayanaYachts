using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_Dealers
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddl_country_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_area.Items.Clear();
            //ddl_area.Items.Add("請選擇");  這個寫法 text & value 都是 "請選擇"
            ddl_area.Items.Add(new ListItem("篩選地區類別", "0"));
        }

        protected void btn_UploadImg_Click(object sender, EventArgs e)
        {
            img_warning.Text = "";

            string savePath = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\Dealers\";

            string FinalName;
            if (ImageUpload.HasFile)
            {
                string fileName = ImageUpload.FileName;

                string extension = System.IO.Path.GetExtension(fileName);

                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                {
                    //檢查檔名衝突
                    string PathToCheck = savePath + fileName;
                    string TempFileName = "";

                    if (File.Exists(PathToCheck))
                    {
                        int count = 2;
                        while (File.Exists(PathToCheck))
                        {
                            string[] FileNameSpilt = fileName.Split('.');
                            TempFileName = FileNameSpilt[0] + "(" + count.ToString() + ")." + FileNameSpilt[1];
                            PathToCheck = savePath + TempFileName;
                            count++;
                        }
                    }

                    ImageUpload.SaveAs(PathToCheck);

                    if (TempFileName != "")
                    {
                        FinalName = TempFileName;
                    }
                    else
                    {
                        FinalName = fileName;
                    }

                    Session["ImgPath"] = FinalName;
                    string ImgUrl = "~/upload/Dealers/" + FinalName;
                    img_dealer.ImageUrl = ResolveUrl(ImgUrl);

                }
                else
                {
                    img_warning.Text = "存在格式不符合的檔案";
                }
            }
            else
            {
                img_warning.Text = "未選擇檔案，請重新上傳";
            }    
        }

        protected void addDealer()
        {
            img_warning.Text = "";

            if (img_dealer.ImageUrl != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO dealers (AreaID, ImgPath, CompanyName, ContactPerson, Address, TEL, Fax, Email, Website) " +
                    "VALUES (@AreaID, @ImgPath, @CompanyName, @ContactPerson, @Address, @TEL, @Fax, @Email, @Website)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@AreaID", ddl_area.SelectedValue);
                cmd.Parameters.AddWithValue("@ImgPath", Session["ImgPath"]);
                cmd.Parameters.AddWithValue("@CompanyName", tbx_CompanyName.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", tbx_ContactPerson.Text);
                cmd.Parameters.AddWithValue("@Address", tbx_Address.Text);
                cmd.Parameters.AddWithValue("@TEL", tbx_TEL.Text);
                cmd.Parameters.AddWithValue("@Fax", tbx_Fax.Text);
                cmd.Parameters.AddWithValue("@Email", tbx_Email.Text);
                cmd.Parameters.AddWithValue("@Website", tbx_Website.Text);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                Response.Redirect("Front.aspx");
            }
            else
            {
                img_warning.Text = "請上傳圖片";
            }
        }

        protected void btn_addDealer_Click(object sender, EventArgs e)
        {
            warning1.Visible = false;
            warning2.Visible = false;
            warning3.Visible = false;
            warning4.Visible = false;
            warning5.Visible = false;
            warning6.Visible = false;
            if (ddl_country.SelectedIndex != 0 && ddl_area.SelectedIndex != 0 && tbx_CompanyName.Text != "" && tbx_ContactPerson.Text != "" && tbx_Address.Text != "" && tbx_TEL.Text != "")
            {
                addDealer();
            }
            else
            {
                if (ddl_country.SelectedIndex == 0)
                {
                    warning1.Visible = true;
                }
                if (ddl_area.SelectedIndex == 0)
                {
                    warning2.Visible = true;
                }
                if (tbx_CompanyName.Text == "")
                {
                    warning3.Visible = true;
                }
                if (tbx_ContactPerson.Text == "")
                {
                    warning4.Visible = true;
                }
                if (tbx_Address.Text == "")
                {
                    warning5.Visible = true;
                }
                if (tbx_TEL.Text == "")
                {
                    warning6.Visible = true;
                }
            }

        }
    }
}