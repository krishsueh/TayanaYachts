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
    public partial class YachtsOverView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowOverview();
                ShowDimension();

                //如果資料庫內該型號沒有任何附件，則 Download 區隱藏
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT yachts.model, yachtsFile.FileName FROM yachts INNER JOIN yachtsFile ON yachts.id = yachtsFile.id WHERE (yachts.model = @model)";
                //顯示右邊對應的型號下載
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@model", Session["yachtsModel"]);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ShowDownload();
                }
                else
                {
                    downloadArea.Visible = false;
                }
                cnn.Close();
            }
        }

        protected void ShowOverview()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT overviewCKeditor FROM yachts WHERE (model = @model)";

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@model", Session["yachtsModel"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //渲染畫面
                Overview_CKeditor.Text = HttpUtility.HtmlDecode(reader["overviewCKeditor"].ToString());
            }
            cnn.Close();
        }
        protected void ShowDimension()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dimensionCKeditor FROM yachts WHERE (model = @model)";

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@model", Session["yachtsModel"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //渲染畫面
                Dimension_CKeditor.Text = HttpUtility.HtmlDecode(reader["dimensionCKeditor"].ToString());
            }
            cnn.Close();
        }

        protected void ShowDownload()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT yachts.model, yachtsFile.FileName FROM yachts INNER JOIN yachtsFile ON yachts.id = yachtsFile.id WHERE (yachts.model = @model)";
            //顯示右邊對應的型號下載
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@model", Session["yachtsModel"]);
            
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            FileRepeater.DataSource = reader;
            FileRepeater.DataBind();
            cnn.Close();
        }
    }
}