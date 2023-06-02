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
    public partial class Front : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDealerFront();
            }
        }

        protected void BindDealerFront()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dealers.*, dealersCountry.Country, dealersArea.Area FROM dealers " +
                "INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID " +
                "INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID " +
                "ORDER BY dealers.initDate DESC";
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

        protected void BindDealerFront_CountrySorting()
        {
            if (ddl_CountrySorting.SelectedValue != "0")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT dealers.*, dealersCountry.Country, dealersArea.Area FROM dealers " +
                    "INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID " +
                    "INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID " +
                    "WHERE dealersArea.CountryID=@CountryID " +
                    "ORDER BY dealers.initDate DESC";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@CountryID", ddl_CountrySorting.SelectedValue);

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
            else
            {
                BindDealerFront();
            }
        }

        protected void BindDealerFront_AreaSorting()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dealers.*, dealersCountry.Country, dealersArea.Area FROM dealers " +
                "INNER JOIN dealersArea ON dealers.AreaID = dealersArea.AreaID " +
                "INNER JOIN dealersCountry ON dealersArea.CountryID = dealersCountry.CountryID " +
                "WHERE dealersArea.CountryID=@CountryID AND dealersArea.AreaID=@AreaID " +
                "ORDER BY dealers.initDate DESC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@CountryID", ddl_CountrySorting.SelectedValue);
            cmd.Parameters.AddWithValue("@AreaID", ddl_AreaSorting.SelectedValue);

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

        protected void ddl_CountrySorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            //篩選器設定
            if (ddl_CountrySorting.SelectedValue != "0")
            {
                ddl_AreaSorting.Visible = true;
                ddl_AreaSorting.Items.Clear();
                ddl_AreaSorting.Items.Add(new ListItem("篩選地區類別", "0"));
            }
            else
            {
                ddl_AreaSorting.Visible = false;
            }

            //顯示篩選結果
            BindDealerFront_CountrySorting();
        }

        protected void ddl_AreaSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            //地區篩選器設定
            if (ddl_AreaSorting.SelectedValue.ToString() == "0")
            {
                ddl_AreaSorting.Visible = false;
                BindDealerFront_CountrySorting();
            }
            else
            {
                BindDealerFront_AreaSorting();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "DELETE FROM dealers WHERE DealersID =" + id;

            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect(Request.Url.ToString());
        }

        //以下是全選功能，還有BUG待修
        //protected void DeleteSelectedProducts_Click(object sender, EventArgs e)
        //{
        //    bool atLeastOneRowDeleted = false;
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        CheckBox cb = (CheckBox)row.FindControl("dealerSelector");
        //        if (cb != null && cb.Checked)
        //        {
        //            atLeastOneRowDeleted = true;
        //            int DealersID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
        //            DeleteResults.Text += string.Format(
        //                "This would have deleted DealersID {0}<br />", DealersID);
        //        }
        //    }
        //    // Show the Label if at least one row was deleted...
        //    DeleteResults.Visible = atLeastOneRowDeleted;
        //}

        //private void ToggleCheckState(bool checkState)
        //{
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        CheckBox cb = (CheckBox)row.FindControl("dealerSelector");
        //        if (cb != null)
        //            cb.Checked = checkState;
        //    }
        //}

        protected void allSelect_CheckedChanged(object sender, EventArgs e)
        {
            //這個全選功能還有 bug 存在
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("dealerSelector");
                if (cb.Checked == false)
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
        }
    }
}