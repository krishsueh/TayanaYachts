using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_Dealers
{
    public partial class DealerCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDealerCountry();
            }
        }

        protected void BindDealerCountry()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM dealersCountry ORDER BY initDate DESC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            //創建適配器 (適配器會自己開關)-非連線作法
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //創建一張表 (空的) 
            DataTable table = new DataTable();

            //數據填充表(已自動開關)
            adapter.Fill(table);

            //拿資料到畫面上
            //控制器資料來源
            GridView1.DataSource = table;

            //控制器綁定
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "delete from dealersCountry where CountryID =" + id;

            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect(Request.Url.ToString());
        }

        protected void btn_addCountry_Click(object sender, EventArgs e)
        {
            if (tbx_addCountry.Text.Trim() == "")
            {
                lbl_warning.Text = "請輸入國家名稱";
            }
            else
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "select upper(Country) AS Country from dealersCountry where Country=@country";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@country", tbx_addCountry.Text.Trim().ToUpper());

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lbl_warning.Text = "此國家名稱已存在";
                    cnn.Close();
                }
                else
                {
                    reader.Close();
                    string sql_add = "INSERT INTO dealersCountry (Country) VALUES (@country)";
                    SqlCommand cmd_add = new SqlCommand(sql_add, cnn);
                    cmd_add.Parameters.AddWithValue("@country", tbx_addCountry.Text.Trim());

                    cmd_add.ExecuteNonQuery();
                    cnn.Close();

                    //新增完畢後，提示新增成功並清空欄位
                    lbl_warning.ForeColor = System.Drawing.Color.Blue;
                    lbl_warning.Text = "已新增國家名稱 " + tbx_addCountry.Text.Trim();
                    tbx_addCountry.Text = "";

                    BindDealerCountry();
                }
            }
        }
    }
}