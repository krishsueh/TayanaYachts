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
    public partial class Yachts_Specification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowSpecification();
            }
        }

        protected void ShowSpecification()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT specCKeditor FROM yachts WHERE (model = @model)";

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@model", Session["yachtsModel"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //渲染畫面
                Spec_CKeditor.Text = HttpUtility.HtmlDecode(reader["specCKeditor"].ToString());
            }
            cnn.Close();
        }
    }
}