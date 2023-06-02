using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowAboutUs();
            }
        }

        protected void ShowAboutUs()
        {
            //取得 About Us 頁面 HTML 資料
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AboutUsHtml FROM Company WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                AboutUs.Text = HttpUtility.HtmlDecode(reader["aboutUsHtml"].ToString());
            }
            cnn.Close();
        }
    }
}