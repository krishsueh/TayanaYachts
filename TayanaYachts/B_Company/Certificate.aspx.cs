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

namespace TayanaYachts.B_Company
{
    public partial class Certificate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCkeditor();
            }

            //必須加入下面，選取圖片時才會出現 "瀏覽伺服器"
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditor_Certificate);
        }

        private void LoadCkeditor()
        {
            //取得 certificate 頁面 HTML 資料
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateHtml, CertificateUpdateTime FROM Company WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                CKEditor_Certificate.Text = HttpUtility.HtmlDecode(reader["CertificateHtml"].ToString());
                lbl_LastUpdated.Text = "更新時間：" + reader["CertificateUpdateTime"].ToString();
            }
            cnn.Close();
        }


        protected void btn_TextUpload_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE Company SET CertificateHtml = @CertificateHtml, CertificateUpdateTime = @CertificateUpdateTime WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@CertificateHtml", HttpUtility.HtmlEncode(CKEditor_Certificate.Text));
            cmd.Parameters.AddWithValue("@CertificateUpdateTime", DateTime.Now);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            LoadCkeditor();

        }
    }

}