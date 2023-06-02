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
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDateAndCoverAndSummaryAndCKeditor();
                LoadUploadedFile();
            }

            cover_warning.Text = "";
            content_warning.Text = "";
            file_warning.Text = "";
        }

        private void LoadDateAndCoverAndSummaryAndCKeditor()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT convert(varchar, dateTitle, 23) AS dateTitle, headline, coverImg, summary, NewsHtml FROM news WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ReleasedDate.Value = reader["dateTitle"].ToString();
                NewsTitle.Text = reader["headline"].ToString();
                CoverPath.ImageUrl = "~/upload/News/" + Request.QueryString["id"] + "/" + reader["coverImg"].ToString();
                tbx_Summary.Text = reader["summary"].ToString();
                CKEditor_Content.Text = HttpUtility.HtmlDecode(reader["NewsHtml"].ToString());
            }
            cnn.Close();
        }

        protected void btn_UploadCover_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
            string savePath = folderName + Request.QueryString["id"] + "\\";

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

                    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "UPDATE news SET coverImg = @coverImg WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    cmd.Parameters.AddWithValue("@coverImg", FinalName);
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();

                    CoverPath.ImageUrl = "~/upload/News/" + Request.QueryString["id"] + "/" + FinalName;

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

        private void LoadUploadedFile()
        {
            CheckBoxList2.Items.Clear(); //無條件先清空CheckBoxList1後再渲染
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM newsFile WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = $"<a href ='../upload/News/{Request.QueryString["id"]}/{reader["FileName"]}' target='_blank'>{reader["FileName"]}</a>";
                listItem.Value = reader["FileID"].ToString();
                CheckBoxList2.Items.Add(listItem);
            }
            cnn.Close();
        }

        protected void btn_FileUpload_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
            string savePath = folderName + Request.QueryString["id"] + "\\";

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

                        cmd.Parameters.AddWithValue("@newsId", Request.QueryString["id"]);
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
                if (file_warning.Text != "存在格式不符合的檔案")
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
            else
            {
                file_warning.Text = "未選擇檔案，請重新上傳";
            }
        }

        protected void btn_FileDelete_Click(object sender, EventArgs e)
        {
            //批次刪除勾選的項目
            foreach (ListItem item in CheckBoxList2.Items)
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
                        string savePath = Server.MapPath("~/upload/News/" + Request.QueryString["id"] + "/");
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
            btn_FileDelete.Visible = false;
        }

        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_FileDelete.Visible = false;
            foreach (ListItem item in CheckBoxList2.Items)
            {
                if (item.Selected)
                {
                    btn_FileDelete.Visible = true;
                }
            }
        }

        protected void btn_UpdateNews_Click(object sender, EventArgs e)
        {
            if (CKEditor_Content.Text != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET dateTitle=@dateTitle, summary = @summary, newsHtml = @newsHtml WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@dateTitle", ReleasedDate.Value);
                cmd.Parameters.AddWithValue("@summary", tbx_Summary.Text);
                cmd.Parameters.AddWithValue("@newsHtml", HttpUtility.HtmlEncode(CKEditor_Content.Text));
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            else
            {
                content_warning.Text = "內文必填!";
            }
        }
    }
}