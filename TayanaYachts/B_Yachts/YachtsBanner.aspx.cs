using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace TayanaYachts.B_Yachts
{
    public partial class YachtsBanner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUploadedImg();
                if (CheckBoxList1.Items.Count == 0)
                {
                    AttachedImg.Visible = false;
                    isCover.Visible = false;
                }
            }

            bannerImg_warning.Text = "";
            img_warning.Text = "";
        }

        private void LoadUploadedImg()
        {
            CheckBoxList1.Items.Clear(); //無條件先清空CheckBoxList1後再渲染
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * " +
                "FROM yachts INNER JOIN yachtsImg ON yachts.id = yachtsImg.id " +
                "WHERE yachts.model = @model";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@model", Request.QueryString["id"].ToString());

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["isIndexCover"].ToString() == "True")
                {
                    ListItem listItem = new ListItem();
                    listItem.Text = string.Format($"<img width='100' style='border: 3px solid #4A6FDC; padding: 5px; margin: 5px' src='../upload/Yachts/{Request.QueryString["id"]}/{reader["ImgFileName"]}' />");
                    listItem.Value = reader["ImgID"].ToString();
                    CheckBoxList1.Items.Add(listItem);
                }
                else
                {
                    ListItem listItem = new ListItem();
                    listItem.Text = string.Format($"<img width='100' style='padding: 5px; margin: 5px'src='../upload/Yachts/{Request.QueryString["id"]}/{reader["ImgFileName"]}' />");
                    listItem.Value = reader["ImgID"].ToString();
                    CheckBoxList1.Items.Add(listItem);
                }

            }
            cnn.Close();
        }

        protected void btn_ImgUpload_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\Yachts";
            string savePath = $"{folderName}\\{Request.QueryString["id"]}\\";

            string FinalName;
            if (ImgUpload.HasFile)
            {
                foreach (HttpPostedFile postedFile in ImgUpload.PostedFiles)
                {
                    string fileName = postedFile.FileName;

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

                        postedFile.SaveAs(PathToCheck);

                        if (TempFileName != "")
                        {
                            FinalName = TempFileName;
                        }
                        else
                        {
                            FinalName = fileName;
                        }
                        Session["FinalName"] = FinalName;

                        //查詢是否已設定過封面
                        SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                        string sqlFindCover = "SELECT * " +
                            "FROM yachts INNER JOIN yachtsImg ON yachts.id = yachtsImg.id " +
                            "WHERE yachts.model = @model AND yachtsImg.isIndexCover=@isIndexCover";

                        SqlCommand cmdFindCover = new SqlCommand(sqlFindCover, cnn);

                        cmdFindCover.Parameters.AddWithValue("@model", Request.QueryString["id"]);
                        cmdFindCover.Parameters.AddWithValue("@isIndexCover", "True");

                        cnn.Open();
                        SqlDataReader reader = cmdFindCover.ExecuteReader();
                        if (reader.Read())
                        {
                            CoverAlreadyExist();
                        }
                        else
                        {
                            NoCoverExist();

                        }
                        cnn.Close();

                    }
                    else
                    {
                        img_warning.Text = "存在格式不符合的檔案";
                    }
                }
                if (img_warning.Text != "存在格式不符合的檔案")
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
            else
            {
                img_warning.Text = "未選擇檔案，請重新上傳";
            }
        }

        private void CoverAlreadyExist()
        {
            SqlConnection cnn_id = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql_id = "SELECT id FROM yachts WHERE model=@model";
            SqlCommand cmd_id = new SqlCommand(sql_id, cnn_id);

            cmd_id.Parameters.AddWithValue("@model", Request.QueryString["id"]);

            cnn_id.Open();
            SqlDataReader reader = cmd_id.ExecuteReader();
            if (reader.Read())
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO yachtsImg (id, ImgFileName) VALUES  (@yachtsId, @ImgFileName)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@yachtsId", reader["id"].ToString());
                cmd.Parameters.AddWithValue("@ImgFileName", Session["FinalName"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            cnn_id.Close();
        }

        private void NoCoverExist()
        {
            SqlConnection cnn_id = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql_id = "SELECT id FROM yachts WHERE model=@model";
            SqlCommand cmd_id = new SqlCommand(sql_id, cnn_id);

            cmd_id.Parameters.AddWithValue("@model", Request.QueryString["id"]);

            cnn_id.Open();
            SqlDataReader reader = cmd_id.ExecuteReader();
            if (reader.Read())
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO yachtsImg (id, ImgFileName, isIndexCover) VALUES  (@yachtsId, @ImgFileName, @isIndexCover)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@yachtsId", reader["id"].ToString());
                cmd.Parameters.AddWithValue("@ImgFileName", Session["FinalName"]);
                cmd.Parameters.AddWithValue("@isIndexCover", "True");

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            cnn_id.Close();
        }

        protected void btn_ImgDelete_Click(object sender, EventArgs e)
        {
            getPreCoverID();

            //批次刪除勾選的項目
            foreach (ListItem item in CheckBoxList1.Items)
            {
                string selectedValue = item.Value;

                if (item.Selected)
                {
                    if (selectedValue == Session["preCoverImgID"].ToString())
                    {
                        bannerImg_warning.Text = "禁止刪除封面";
                    }
                    else
                    {
                        //Upload 資料夾刪除
                        SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                        string sql = "SELECT ImgFileName FROM yachtsImg WHERE (ImgID = @ImgID)";
                        SqlCommand cmd = new SqlCommand(sql, cnn);
                        cmd.Parameters.AddWithValue("@ImgID", selectedValue);

                        cnn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string savePath = Server.MapPath("~/upload/Yachts/" + Request.QueryString["id"] + "/");
                            File.Delete(savePath + reader["ImgFileName"].ToString());
                        }
                        cnn.Close();


                        //資料庫刪除
                        string sql2 = "DELETE FROM yachtsImg WHERE (ImgID = @ImgID)";
                        SqlCommand cmd2 = new SqlCommand(sql2, cnn);
                        cmd2.Parameters.AddWithValue("@ImgID", selectedValue);

                        cnn.Open();
                        cmd2.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
            }
            LoadUploadedImg();
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_ImgDelete.Visible = false;
            btn_setIndexCover.Visible = false;
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    btn_ImgDelete.Visible = true;
                    btn_setIndexCover.Visible = true;
                }
            }
        }

        protected void btn_setIndexCover_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (ListItem checkedItem in CheckBoxList1.Items)
            {
                if (checkedItem.Selected)
                {
                    count++;
                }
            }

            if (count != 1)
            {
                bannerImg_warning.Text = "僅能選擇一張為封面";
            }
            else
            {
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected)
                    {
                        getPreCoverID();

                        string selectedValue = item.Value;
                        SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                        string sql = "UPDATE yachtsImg SET isIndexCover = @notIndexCover WHERE ImgID = @preCoverImgID;" +
                            "UPDATE yachtsImg SET isIndexCover = @isIndexCover WHERE ImgID = @ImgID";
                        SqlCommand cmd = new SqlCommand(sql, cnn);

                        //把上一張被設為封面的圖片的 isIndexCover 改為 False
                        cmd.Parameters.AddWithValue("@preCoverImgID", Session["preCoverImgID"]);
                        cmd.Parameters.AddWithValue("@notIndexCover", "False");

                        //把被點選的圖片設為封面，isIndexCover 改為 True
                        cmd.Parameters.AddWithValue("@isIndexCover", "True");
                        cmd.Parameters.AddWithValue("@ImgID", selectedValue);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();

                        bannerImg_warning.Text = "封面設定成功!";
                    }
                }

                btn_ImgDelete.Visible = false;
                btn_setIndexCover.Visible = false;
                LoadUploadedImg();
            }
        }

        private void getPreCoverID()
        {
            SqlConnection cnn_id = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql_id = "SELECT id FROM yachts WHERE model=@model";
            SqlCommand cmd_id = new SqlCommand(sql_id, cnn_id);

            cmd_id.Parameters.AddWithValue("@model", Request.QueryString["id"]);

            cnn_id.Open();
            SqlDataReader reader_id = cmd_id.ExecuteReader();
            if (reader_id.Read())
            {
                //查詢被設為封面的 ImgID
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sqlFindCover = "SELECT ImgID FROM yachtsImg WHERE id=@id AND isIndexCover=@isIndexCover";
                SqlCommand cmdFindCover = new SqlCommand(sqlFindCover, cnn);

                cmdFindCover.Parameters.AddWithValue("@id", reader_id["id"].ToString());
                cmdFindCover.Parameters.AddWithValue("@isIndexCover", "True");

                cnn.Open();
                SqlDataReader reader = cmdFindCover.ExecuteReader();
                if (reader.Read())
                {
                    //把查詢到的 ImgID 記錄在 Session 裡
                    Session["preCoverImgID"] = reader["ImgID"];
                }
                cnn.Close();
            }
            cnn_id.Close();
        }
    }
}