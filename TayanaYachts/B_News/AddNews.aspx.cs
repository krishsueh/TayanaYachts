using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_News
{
    public partial class AddNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NewsArea.Visible = false;
                btn_AddNews.Visible = false;
            }
            date_warning.Text = "";
            headline_warning.Text = "";
            cover_warning.Text = "";
            file_warning.Text = "";

        }

        protected void btn_addHeadline_Click(object sender, EventArgs e)
        {
            if (tbx_Headline.Text != "" && ReleasedDate.Value != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO news (dateTitle, headline, goTop) VALUES (@dateTitle, @headline, @goTop)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@dateTitle", ReleasedDate.Value);
                cmd.Parameters.AddWithValue("@headline", tbx_Headline.Text.ToString());
                cmd.Parameters.AddWithValue("@goTop", CheckBoxList_Yachts.Items[0].Selected.ToString());


                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                //渲染畫面
                headline_warning.Text = "新增標題成功!";

                //建立標題的同時，創立該型號的資料夾存放圖片或附檔
                createDirectory();

                //新增完型號後，型號禁止修改 
                tbx_Headline.Enabled = false;
                btn_addHeadline.Visible = false;
                NewsArea.Visible = true;
                btn_AddNews.Visible = true;
            }
            else
            {
                if (tbx_Headline.Text == "")
                {
                    headline_warning.Text = "標題不可空白!";
                }
                if (ReleasedDate.Value == "")
                {
                    date_warning.Text = "日期不可空白!";
                }

            }
        }

        protected void createDirectory()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT id FROM news WHERE headline = @headline";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@headline", tbx_Headline.Text.Trim());

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
                string pathString = Path.Combine(folderName, reader["id"].ToString());

                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                Session["id"] = reader["id"].ToString();
            }
            cnn.Close();
        }

        protected void btn_UploadCover_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
            string savePath = folderName + Session["id"] + "\\";

            string FinalName;
            if (CoverUpload.HasFile)
            {
                string fileName = CoverUpload.FileName;

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

                    CoverUpload.SaveAs(PathToCheck);

                    if (TempFileName != "")
                    {
                        FinalName = TempFileName;
                    }
                    else
                    {
                        FinalName = fileName;
                    }

                    Session["FinalName"] = FinalName;

                    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "UPDATE news SET coverImg = @coverImg WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    cmd.Parameters.AddWithValue("@coverImg", FinalName);
                    cmd.Parameters.AddWithValue("@id", Session["id"]);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    CoverPath.ImageUrl = "~/upload/News/" + Session["id"] + "/" + Session["FinalName"].ToString();

                }
                else
                {
                    cover_warning.Text = "存在格式不符合的檔案";
                }
            }
            else
            {
                cover_warning.Text = "未選擇檔案，請重新上傳";
            }
        }

        protected void btn_AddNews_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CoverPath.ImageUrl))
            {
                cover_warning.Text = "請上傳縮圖";
            }
            else
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET dateTitle=@dateTitle, goTop=@goTop, coverImg=@coverImg, summary=@summary, newsHtml=@newsHtml WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@dateTitle", ReleasedDate.Value);
                cmd.Parameters.AddWithValue("@goTop", CheckBoxList_Yachts.Items[0].Selected.ToString());
                cmd.Parameters.AddWithValue("@coverImg", Session["FinalName"].ToString());
                cmd.Parameters.AddWithValue("@summary", tbx_Summary.Text);
                cmd.Parameters.AddWithValue("@newsHtml", HttpUtility.HtmlEncode(CKEditor_Content.Text));
                cmd.Parameters.AddWithValue("@id", Session["id"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                Response.Redirect("NewsList.aspx");
            }
        }

        private void LoadUploadedFile()
        {
            CheckBoxList_File.Items.Clear(); //無條件先清空CheckBoxList1後再渲染
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM newsFile WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Session["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = $"<a href ='../upload/News/{Session["id"]}/{reader["FileName"]}' target='_blank'>{reader["FileName"]}</a>";
                listItem.Value = reader["FileID"].ToString();
                CheckBoxList_File.Items.Add(listItem);
            }
            cnn.Close();
        }

        protected void btn_FileUpload_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
            string savePath = folderName + Session["id"] + "\\";

            string FinalName;
            if (FileUpload.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload.PostedFiles)
                {
                    string fileName = postedFile.FileName;

                    string extension = System.IO.Path.GetExtension(fileName);

                    if (extension == ".pdf" || extension == ".word" || extension == ".txt" || extension == ".rar")
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

                        postedFile.SaveAs(PathToCheck);

                        if (TempFileName != "")
                        {
                            FinalName = TempFileName;
                        }
                        else
                        {
                            FinalName = fileName;
                        }

                        SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                        string sql = "INSERT INTO newsFile (id, FileName) VALUES  (@newsId, @FileName)";
                        SqlCommand cmd = new SqlCommand(sql, cnn);

                        cmd.Parameters.AddWithValue("@newsId", Session["id"]);
                        cmd.Parameters.AddWithValue("@FileName", FinalName);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    else
                    {
                        file_warning.Text = "存在格式不符合的檔案";
                    }
                }
                LoadUploadedFile();
            }
            else
            {
                file_warning.Text = "未選擇檔案，請重新上傳";
            }
        }

        protected void btn_FileDelete_Click(object sender, EventArgs e)
        {
            //批次刪除勾選的項目
            foreach (ListItem item in CheckBoxList_File.Items)
            {
                if (item.Selected)
                {
                    string selectedValue = item.Value;

                    //刪除 Upload 資料夾內的附檔
                    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT FileName FROM newsFile WHERE (FileID = @FileID)";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@FileID", selectedValue);

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string savePath = Server.MapPath("~/upload/News/" + Session["id"] + "/");
                        File.Delete(savePath + reader["FileName"].ToString());
                    }
                    cnn.Close();


                    //從資料庫刪除附檔
                    string sql2 = "DELETE FROM newsFile WHERE (FileID = @FileID)";
                    SqlCommand cmd2 = new SqlCommand(sql2, cnn);
                    cmd2.Parameters.AddWithValue("@FileID", selectedValue);

                    cnn.Open();
                    cmd2.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            LoadUploadedFile();
        }

        protected void CheckBoxList_File_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_FileDelete.Visible = false;
            foreach (ListItem item in CheckBoxList_File.Items)
            {
                if (item.Selected)
                {
                    btn_FileDelete.Visible = true;
                }
            }
        }
    }
}