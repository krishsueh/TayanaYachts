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
    public partial class news1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowNews();
                ShowNewsImg();
                ShowNewsFile();
            }

        }

        protected void ShowNews()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM news WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                title.Text = reader["headline"].ToString();
                content.Text = HttpUtility.HtmlDecode(reader["newsHtml"].ToString());
            }
            cnn.Close();
        }

        protected void ShowNewsImg()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT id, ImgFileName FROM newsImg WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ImgRepeater.DataSource = reader;
            ImgRepeater.DataBind();
            cnn.Close();
        }

        protected void ShowNewsFile()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT id, FileName FROM newsFile WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            FileRepeater.DataSource = reader;
            FileRepeater.DataBind();
            cnn.Close();
        }
    }
}