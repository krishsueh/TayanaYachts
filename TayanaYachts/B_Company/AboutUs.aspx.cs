using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_Company
{
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCkeditor();
            }

            //必須加入下面三行，選取圖片時才會出現 "瀏覽伺服器"
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl1);
        }

        private void LoadCkeditor()
        {
            //取得 About Us 頁面 HTML 資料
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AboutUsHtml, AboutUsUpdateTime FROM Company WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditorControl1.Text = HttpUtility.HtmlDecode(reader["aboutUsHtml"].ToString());
                lbl_LastUpdated.Text = "更新時間：" + reader["AboutUsUpdateTime"].ToString();
            }
            cnn.Close();
        }
        protected void btn_UploadAboutUs_Click(object sender, EventArgs e)
        {
            //更新 About Us 頁面 HTML 資料
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE Company SET aboutUsHtml = @aboutUsHtml, AboutUsUpdateTime=@AboutUsUpdateTime WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@aboutUsHtml", HttpUtility.HtmlEncode(CKEditorControl1.Text));
            cmd.Parameters.AddWithValue("@AboutUsUpdateTime", DateTime.Now);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            LoadCkeditor();
        }
    }
}