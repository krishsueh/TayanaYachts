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
    public partial class yachts_overview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //顯示左邊型號選單
                ShowModel();
                foreach (RepeaterItem item in ModelRepeater.Items)
                {
                    LinkButton btn_Model = (LinkButton)item.FindControl("btn_Model");
                    LinkButton btn_NewDesign = (LinkButton)item.FindControl("btn_NewDesign");
                    LinkButton btn_NewBuilding = (LinkButton)item.FindControl("btn_NewBuilding");
                    if (btn_NewDesign.Text != "")
                    {
                        btn_Model.Visible = false;
                        btn_NewDesign.Visible = true;
                    }
                    else if (btn_NewBuilding.Text != "")
                    {
                        btn_Model.Visible = false;
                        btn_NewBuilding.Visible = true;
                    }
                }

                ShowOverviewFirst();
                ShowDownloadFirst();
            }
        }

        protected void ShowModel()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM yachts ORDER BY initDate DESC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ModelRepeater.DataSource = reader;
            ModelRepeater.DataBind();
            cnn.Close();

        }

        protected void ShowOverviewFirst()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT model, overviewCKeditor FROM yachts WHERE (id = '31')";

            //顯示右邊型號 Title
            SqlCommand cmd_title = new SqlCommand(sql, cnn);
            cnn.Open();
            SqlDataReader reader_title = cmd_title.ExecuteReader();
            if (reader_title.Read())
            {
                ModelTitle.Text = reader_title["model"].ToString();
                NavTitle.Text = reader_title["model"].ToString();
            }
            cnn.Close();

            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //渲染畫面
                Overview.Text = HttpUtility.HtmlDecode(reader["overviewCKeditor"].ToString());
            }
            cnn.Close();
        }

        protected void ShowOverview()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT model, overviewCKeditor FROM yachts WHERE (id = @id)";

            //顯示右邊型號 Title
            SqlCommand cmd_title = new SqlCommand(sql, cnn);
            cmd_title.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cnn.Open();
            SqlDataReader reader_title = cmd_title.ExecuteReader();
            if (reader_title.Read())
            {
                ModelTitle.Text = reader_title["model"].ToString();
                NavTitle.Text = reader_title["model"].ToString();
            }
            cnn.Close();

            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //渲染畫面
                Overview.Text = HttpUtility.HtmlDecode(reader["overviewCKeditor"].ToString());
            }
            cnn.Close();
        }

        protected void ShowDownloadFirst()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT FileName FROM yachtsFile WHERE (id = '31')";

            //顯示右邊對應的型號下載
            SqlCommand cmd_download = new SqlCommand(sql, cnn);
            cnn.Open();
            SqlDataReader reader_download = cmd_download.ExecuteReader();
            FileRepeater.DataSource = reader_download;
            FileRepeater.DataBind();
            cnn.Close();
        }

        protected void ShowDownload()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT FileName FROM yachtsFile WHERE (id = @id)";

            //顯示右邊對應的型號下載
            SqlCommand cmd_download = new SqlCommand(sql, cnn);
            cmd_download.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cnn.Open();
            SqlDataReader reader_download = cmd_download.ExecuteReader();
            FileRepeater.DataSource = reader_download;
            FileRepeater.DataBind();
            cnn.Close();
        }

        protected void btn_Model_Click(object sender, EventArgs e)
        {
            ShowOverview();
            ShowDownload();
        }

        protected void btn_NewDesign_Click(object sender, EventArgs e)
        {
            ShowOverview();
            ShowDownload();
        }

        protected void btn_NewBuilding_Click(object sender, EventArgs e)
        {
            ShowOverview();
            ShowDownload();
        }

        protected void btn_overview_Click(object sender, EventArgs e)
        {
            ShowOverview();
            ShowDownload();
        }
    }
}