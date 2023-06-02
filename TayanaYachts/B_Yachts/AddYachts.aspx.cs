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
    public partial class AddYachts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                overviewCard.Visible = false;
                layoutCard.Visible = false;
                specCard.Visible = false;
                btn_AddYachts.Visible = false;
            }
            
            model_warning.Text = "";

            //必須加入下面，選取圖片時才會出現 "瀏覽伺服器"
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditor_Overview);
            fileBrowser.SetupCKEditor(CKEditor_Dimension);
            fileBrowser.SetupCKEditor(CKEditor_Layout);
            fileBrowser.SetupCKEditor(CKEditor_Spec);
        }

        protected void btn_addModel_Click(object sender, EventArgs e)
        {

            if (tbx_Model.Text != "" && tbx_Length.Text != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT * FROM yachts WHERE model = @model";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
                cmd.Parameters.AddWithValue("@model", model);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    model_warning.Text = "此型號已存在!";
                    cnn.Close();
                }
                else
                {
                    reader.Close();
                    AddModel();
                    overviewCard.Visible = true;
                    layoutCard.Visible = true;
                    specCard.Visible = true;
                    btn_AddYachts.Visible = true;
                }
            }
            else
            {
                model_warning.Text = "型號不可空白!";
            }
        }

        protected void AddModel()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "INSERT INTO yachts (model, isNewDesign, isNewBuilding) VALUES (@model, @isNewDesign, @isNewBuilding)";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@isNewDesign", CheckBoxList_Yachts.Items[0].Selected.ToString());
            cmd.Parameters.AddWithValue("@isNewBuilding", CheckBoxList_Yachts.Items[1].Selected.ToString());


            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            //渲染畫面
            model_warning.Text = "新增型號成功!";

            //建立標題的同時，創立該型號的資料夾存放圖片或附檔
            createDirectory();

            //新增完型號後，型號禁止修改 
            tbx_Model.Enabled = false;
            tbx_Length.Enabled = false;
            btn_addModel.Visible = false;
        }

        protected void createDirectory()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts WHERE model = @model";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
            cmd.Parameters.AddWithValue("@model", model);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\Yachts\";
                string pathString = Path.Combine(folderName, reader["model"].ToString());

                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
            }
            cnn.Close();
        }

        protected void btn_AddYachts_Click(object sender, EventArgs e)
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
            string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
            cmd.Parameters.AddWithValue("@model", model);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect("YachtsList.aspx");
        }

        protected void btn_FileUpload_Click(object sender, EventArgs e)
        {
            file_warning.Text = "";
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\Yachts\";
            string savePath = $"{folderName}\\{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}\\";

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

                        string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
                        cmd_id.Parameters.AddWithValue("@model", model);

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

        private void LoadUploadedFile()
        {
            CheckBoxList_File.Items.Clear();
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT   yachtsFile.FileID, yachtsFile.id, yachtsFile.FileName, yachts.model " +
                "FROM yachts INNER JOIN yachtsFile ON yachts.id = yachtsFile.id " +
                "WHERE yachts.model = @model";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
            cmd.Parameters.AddWithValue("@model", model);

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
                        string model = $"{tbx_Model.Text.Trim()} {tbx_Length.Text.Trim()}";
                        string savePath = Server.MapPath($"~/upload/Yachts/{model}/");
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
            LoadUploadedFile();
            btn_FileDelete.Visible = false;
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