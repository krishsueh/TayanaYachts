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
    public partial class AreaSorting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDealerArea();
            }
        }

        protected void BindDealerArea()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM dealersArea WHERE CountryID=@id ORDER BY Area";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["country"]);

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
            string sql = "delete from dealersArea where AreaID =" + id;

            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect(Request.Url.ToString());
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindDealerArea();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindDealerArea();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE dealersArea SET Area=@area, CountryID=@CountryID WHERE AreaID=@id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@Area", ((TextBox)GridView1.Rows[e.RowIndex].FindControl("tbx_editArea")).Text);
            cmd.Parameters.AddWithValue("@CountryID", Request.QueryString["country"].ToString());
            cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.RowIndex].Value.ToString());

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            GridView1.EditIndex = -1;
            BindDealerArea();
        }

        protected void btn_addArea_Click(object sender, EventArgs e)
        {
            lbl_warning.ForeColor = System.Drawing.Color.Red;
            if (tbx_addArea.Text.Trim() == "")
            {
                lbl_warning.Text = "請輸入地區名稱";
            }
            else
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "select upper(Area) AS Area from dealersArea where Area=@area";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@area", tbx_addArea.Text.Trim().ToUpper());

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lbl_warning.Text = "此地區名稱已存在";
                    cnn.Close();
                }
                else
                {
                    reader.Close();
                    string sql_add = "INSERT INTO dealersArea (Area, CountryID) VALUES (@area, @countryID)";
                    SqlCommand cmd_add = new SqlCommand(sql_add, cnn);
                    cmd_add.Parameters.AddWithValue("@area", tbx_addArea.Text.Trim());
                    cmd_add.Parameters.AddWithValue("@countryID", Request.QueryString["country"]);

                    cmd_add.ExecuteNonQuery();
                    cnn.Close();

                    //新增完畢後，提示新增成功並清空欄位
                    lbl_warning.ForeColor = System.Drawing.Color.Blue;
                    lbl_warning.Text = "已新增地區名稱 " + tbx_addArea.Text;
                    tbx_addArea.Text = "";

                    BindDealerArea();
                }
            }
        }
    }
}