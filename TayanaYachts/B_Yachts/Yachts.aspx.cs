using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace TayanaYachts.B_Yachts
{
    public partial class Yachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadModel();
                LoadCkeditorOverview();
                LoadCkeditorDimension();
                LoadUploadedFile();
                LoadCkeditorLayout();
                LoadCkeditorSpec();
            }

            //必須加入下面，選取圖片時才會出現 "瀏覽伺服器"
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditor_Overview);
            fileBrowser.SetupCKEditor(CKEditor_Dimension);
            fileBrowser.SetupCKEditor(CKEditor_Layout);
            fileBrowser.SetupCKEditor(CKEditor_Spec);
        }

        private void LoadModel()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                Lit_Model.Text = reader["model"].ToString();
                CheckBoxList_Yachts.Items[0].Selected = Convert.ToBoolean(reader["isNewDesign"]);
                CheckBoxList_Yachts.Items[1].Selected = Convert.ToBoolean(reader["isNewBuilding"]);
            }
            cnn.Close();
        }

        private void LoadCkeditorOverview()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditor_Overview.Text = HttpUtility.HtmlDecode(reader["overviewCKeditor"].ToString());
            }
            cnn.Close();
        }

        private void LoadCkeditorDimension()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditor_Dimension.Text = HttpUtility.HtmlDecode(reader["dimensionCKeditor"].ToString());
            }
            cnn.Close();
        }

        private void LoadCkeditorLayout()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditor_Layout.Text = HttpUtility.HtmlDecode(reader["layoutCKeditor"].ToString());
            }
            cnn.Close();
        }

        private void LoadCkeditorSpec()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditor_Spec.Text = HttpUtility.HtmlDecode(reader["specCKeditor"].ToString());
            }
            cnn.Close();
        }

        private void LoadUploadedFile()
        {
            CheckBoxList_File.Items.Clear();
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT   yachtsFile.FileID, yachtsFile.id, yachtsFile.FileName, yachts.model " +
                "FROM yachts INNER JOIN yachtsFile ON yachts.id = yachtsFile.id " +
                "WHERE yachts.model = @model";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@model", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = $"<a href ='../upload/Yachts/{reader["model"]}/{reader["FileName"]}' target='_blank'>{reader["FileName"]}</a>";
                listItem.Value = reader["FileID"].ToString();
                CheckBoxList_File.Items.Add(listItem);
            }
            cnn.Close();
        }

        protected void btn_FileUpload_Click(object sender, EventArgs e)
        {
            file_warning.Text = "";
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\Yachts";
            string savePath = $"{folderName}\\{Request.QueryString["id"]}\\";

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

                        SqlConnection cnn_id = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                        string sql_id = "SELECT id FROM yachts WHERE model=@model";
                        SqlCommand cmd_id = new SqlCommand(sql_id, cnn_id);

                        cmd_id.Parameters.AddWithValue("@model", Request.QueryString["id"]);

                        cnn_id.Open();
                        SqlDataReader reader = cmd_id.ExecuteReader();
                        if (reader.Read())
                        {
                            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                            string sql = "INSERT INTO yachtsFile (id, FileName) VALUES  (@id, @FileName)";
                            SqlCommand cmd = new SqlCommand(sql, cnn);

                            cmd.Parameters.AddWithValue("@id", reader["id"].ToString());
                            cmd.Parameters.AddWithValue("@FileName", FinalName);

                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                            LoadUploadedFile();
                        }
                        cnn_id.Close();
                    }
                    else
                    {
                        file_warning.Text = "存在格式不符合的檔案";
                    }
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
            foreach (ListItem item in CheckBoxList_File.Items)
            {
                if (item.Selected)
                {
                    string selectedValue = item.Value;

                    //刪除 Upload 資料夾內的附檔
                    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT FileName FROM yachtsFile WHERE (FileID = @FileID)";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@FileID", selectedValue);

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string savePath = Server.MapPath("~/upload/Yachts/" + Request.QueryString["id"] + "/");
                        File.Delete(savePath + reader["FileName"].ToString());
                    }
                    cnn.Close();


                    //從資料庫刪除附檔
                    string sql2 = "DELETE FROM yachtsFile WHERE (FileID = @FileID)";
                    SqlCommand cmd2 = new SqlCommand(sql2, cnn);
                    cmd2.Parameters.AddWithValue("@FileID", selectedValue);

                    cnn.Open();
                    cmd2.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            Response.Redirect(Request.Url.ToString());
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
        
        protected void btn_UpdateYachts_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE yachts SET isNewDesign=@isNewDesign, isNewBuilding=@isNewBuilding, overviewCKeditor=@overviewCKeditor, dimensionCKeditor=@dimensionCKeditor, layoutCKeditor=@layoutCKeditor, specCKeditor=@specCKeditor WHERE model=@model";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@isNewDesign", CheckBoxList_Yachts.Items[0].Selected.ToString());
            cmd.Parameters.AddWithValue("@isNewBuilding", CheckBoxList_Yachts.Items[1].Selected.ToString());
            cmd.Parameters.AddWithValue("@overviewCKeditor", HttpUtility.HtmlEncode(CKEditor_Overview.Text));
            cmd.Parameters.AddWithValue("@dimensionCKeditor", HttpUtility.HtmlEncode(CKEditor_Dimension.Text));
            cmd.Parameters.AddWithValue("@layoutCKeditor", HttpUtility.HtmlEncode(CKEditor_Layout.Text));
            cmd.Parameters.AddWithValue("@specCKeditor", HttpUtility.HtmlEncode(CKEditor_Spec.Text));
            cmd.Parameters.AddWithValue("@model", Request.QueryString["id"]);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            MessageBox.Show("更新資料完畢!", "Tayana Yachts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Response.Redirect(Request.Url.ToString());
        }
    }
}