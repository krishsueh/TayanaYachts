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
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDealers();
            }

        }

        protected void BindDealers()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dealers.*, dealersCountry.Country, dealersArea.Area FROM dealers INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID WHERE (dealers.DealersID=@DealersID)";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@DealersID", Request.QueryString["DealersID"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tbx_Country.Text = reader["Country"].ToString();
                tbx_Area.Text = reader["Area"].ToString();
                ImgPath.ImageUrl = "~/upload/Dealers/" + reader["ImgPath"].ToString();
                tbx_CompanyName.Text = reader["CompanyName"].ToString();
                tbx_ContactPerson.Text = reader["ContactPerson"].ToString();
                tbx_Address.Text = reader["Address"].ToString();
                tbx_TEL.Text = reader["TEL"].ToString();
                tbx_Fax.Text = reader["Fax"].ToString();
                tbx_Email.Text = reader["Email"].ToString();
                tbx_Website.Text = reader["Website"].ToString();
                tbx_initDate.Text = reader["initDate"].ToString();
                tbx_UpdateTime.Text = reader["UpdateTime"].ToString();
            }
            cnn.Close();

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

                    string ImgUrl = "~/upload/Dealers/" + FinalName;
                    ImgPath.ImageUrl = ResolveUrl(ImgUrl);

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

        protected void updateDealers()
        {

            if (ImgPath.ImageUrl != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE dealers SET CompanyName=@CompanyName, ContactPerson=@ContactPerson, ImgPath=@ImgPath, " +
                    "Address=@Address,TEL=@TEL, Fax=@Fax, Email=@Email, Website=@Website, UpdateTime=@UpdateTime WHERE DealersID=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@CompanyName", tbx_CompanyName.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", tbx_ContactPerson.Text);
                cmd.Parameters.AddWithValue("@ImgPath", ImgPath.ImageUrl.ToString().Substring(16));
                cmd.Parameters.AddWithValue("@Address", tbx_Address.Text);
                cmd.Parameters.AddWithValue("@TEL", tbx_TEL.Text);
                cmd.Parameters.AddWithValue("@Fax", tbx_Fax.Text);
                cmd.Parameters.AddWithValue("@Email", tbx_Email.Text);
                cmd.Parameters.AddWithValue("@Website", tbx_Website.Text);
                cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", Request.QueryString["DealersID"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                BindDealers();
            }
            else
            {
                img_warning.Text = "請上傳圖片，僅支援: jpg、jpeg、png、gif";
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            warning3.Visible = false;
            warning4.Visible = false;
            warning5.Visible = false;
            warning6.Visible = false;
            if (tbx_CompanyName.Text != "" && tbx_ContactPerson.Text != "" && tbx_Address.Text != "" && tbx_TEL.Text != "")
            {
                updateDealers();
            }
            else
            {
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