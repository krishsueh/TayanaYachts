using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_Account
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUser();
            }
        }

        protected void BindUser()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM users ORDER BY initDate";
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

            //if (Session["Authority"] == null)
            //{
            //    string RedirectUrl = ResolveUrl("Login.aspx"); //導回登入頁
            //    Response.Redirect(RedirectUrl);
            //}
            //else
            //{
            //    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            //    string sql = "SELECT * FROM users WHERE Authority < @Authority ORDER BY initDate";
            //    SqlCommand cmd = new SqlCommand(sql, cnn);

            //    cmd.Parameters.AddWithValue("@Authority", Session["Authority"]);

            //    //創建適配器 (適配器會自己開關)-非連線作法
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            //    //創建一張表 (空的) 
            //    DataTable table = new DataTable();

            //    //數據填充表(已自動開關)
            //    adapter.Fill(table);

            //    //拿資料到畫面上
            //    //控制器資料來源
            //    GridView1.DataSource = table;

            //    //控制器綁定
            //    GridView1.DataBind();
            //}
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "delete from users where UserID =" + id;
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            BindUser();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindUser();
        }
    }
}