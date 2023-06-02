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
    public partial class company1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCertificate();
        }

        protected void ShowCertificate()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateHtml FROM Company WHERE id = 1";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                textCertificate.Text = HttpUtility.HtmlDecode(reader["CertificateHtml"].ToString());
            }
            cnn.Close();
        }


    }
}