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
    public partial class dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //顯示左邊國家選單
                ShowCountry();

                //顯示右邊
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT dealersCountry.Country, dealersArea.Area, dealers.ImgPath, dealers.CompanyName, dealers.ContactPerson, " +
                    "dealers.Address, dealers.TEL, dealers.Fax, dealers.Email, dealers.Website FROM dealers " +
                    "INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID " +
                    "INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID " +
                    "WHERE(dealersArea.CountryID = @id)";

                //顯示右邊國家 Title
                SqlCommand cmd_title = new SqlCommand(sql, cnn);
                cmd_title.Parameters.AddWithValue("@id", "3");     //一進入 dealers.aspx 指定顯示 USA (CountryID=3) 的抬頭
                cnn.Open();
                SqlDataReader reader_title = cmd_title.ExecuteReader();
                if (reader_title.Read())
                {
                    CountryTitle.Text = reader_title["Country"].ToString();
                    NavTitle.Text = reader_title["Country"].ToString();
                }
                cnn.Close();

                //顯示右邊對應國家的 Dealers
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@id", "3");     //一進入 dealers.aspx 指定顯示 USA (CountryID=3) 的資料
                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DealerRepeater.DataSource = reader;
                DealerRepeater.DataBind();
                cnn.Close();
            }

        }

        protected void ShowCountry()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CountryID, Country FROM dealersCountry";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            CountryRepeater.DataSource = reader;
            CountryRepeater.DataBind();
            cnn.Close();
        }

        protected void ShowDealers_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dealersCountry.Country, dealersArea.Area, dealers.ImgPath, dealers.CompanyName, dealers.ContactPerson, dealers.Address, " +
                "dealers.TEL, dealers.Fax, dealers.Email, dealers.Website FROM dealers " +
                "INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID " +
                "INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID " +
                "WHERE(dealersArea.CountryID = @id)";

            SqlCommand cmd_title = new SqlCommand(sql, cnn);
            cmd_title.Parameters.AddWithValue("id", Request.QueryString["id"]);
            cnn.Open();
            SqlDataReader reader_title = cmd_title.ExecuteReader();
            if (reader_title.Read())
            {
                CountryTitle.Text = reader_title["Country"].ToString();
                NavTitle.Text = reader_title["Country"].ToString();
            }
            cnn.Close();


            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.Parameters.AddWithValue("id", Request.QueryString["id"]);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DealerRepeater.DataSource = reader;
            DealerRepeater.DataBind();
            cnn.Close();
        }


    }
}