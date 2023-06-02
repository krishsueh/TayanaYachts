using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_News
{
    public partial class NewsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.SelectedDate = Calendar1.TodaysDate; //預設選取當日日期
                loadDayNewsHeadline();
            }
        }

        private void loadDayNewsHeadline()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM news WHERE dateTitle = @dateTitle ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@dateTitle", Calendar1.SelectedDate.ToString("yyyy-M-dd"));

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string Headline = reader["headline"].ToString();
                string goTop = reader["goTop"].ToString();

                if (goTop.Equals("True"))
                {
                    ListItem listItem = new ListItem();
                    listItem.Text = Headline + " [置頂]";
                    listItem.Value = Headline;
                    rbl_headline.Items.Add(listItem);
                }
                else
                {
                    ListItem listItem = new ListItem();
                    listItem.Text = Headline;
                    listItem.Value = Headline;
                    rbl_headline.Items.Add(listItem);
                }
            }
            cnn.Close();
        }

        protected void btn_AddHeadline_Click(object sender, EventArgs e)
        {
            warning.Text = "";
            if (tbx_headline.Text == "")
            {
                warning.Visible = true;
                warning.Text = "請輸入標題!";
            }
            else
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO news (dateTitle, headline, guid, goTop) VALUES (@dateTitle, @headline, @guid, @goTop)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@dateTitle", Calendar1.SelectedDate.ToString("yyyy-M-dd"));
                cmd.Parameters.AddWithValue("@headline", tbx_headline.Text);
                cmd.Parameters.AddWithValue("@guid", Guid.NewGuid().ToString().Trim());
                cmd.Parameters.AddWithValue("@goTop", ckb_goTop.Checked.ToString());

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                //渲染畫面
                rbl_headline.Items.Clear();
                loadDayNewsHeadline();

                //清空輸入欄位
                tbx_headline.Text = "";
                ckb_goTop.Checked = false;
            }

        }

        protected void btn_DeleteNews_Click(object sender, EventArgs e)
        {
            btn_DeleteNews.Visible = true;
            btn_DeleteCancel.Visible = true;

            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "DELETE FROM news WHERE dateTitle = @dateTitle AND headline = @headline";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@dateTitle", Calendar1.SelectedDate.ToString("yyyy-M-dd"));
            cmd.Parameters.AddWithValue("@headline", rbl_headline.SelectedValue);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            //渲染畫面
            btn_DeleteNews.Visible = false;
            btn_DeleteCancel.Visible = false;
            rbl_headline.Items.Clear();
            loadDayNewsHeadline();
        }

        protected void rbl_headline_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_DeleteNews.Visible = true;
            btn_DeleteCancel.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            btn_DeleteNews.Visible = false;
            btn_DeleteCancel.Visible = false;
            //lbl_goTop.Visible = false;
            rbl_headline.Items.Clear();
            loadDayNewsHeadline();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            //取得新聞日期
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT dateTitle FROM news";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //轉換為 DateTime 型別
                DateTime newsTime = DateTime.Parse(reader["dateTitle"].ToString());
                //有新聞的日期 且 此日期不是選中的日期時 就修改日期外觀
                if (e.Day.Date.Date == newsTime && e.Day.Date.Date != Calendar1.SelectedDate)
                {
                    e.Cell.Font.Underline = true; 
                    e.Cell.Font.Bold = true; 
                    e.Cell.ForeColor = System.Drawing.Color.DodgerBlue; 
                }
            }
            cnn.Close();
        }

        protected void btn_DeleteCancel_Click(object sender, EventArgs e)
        {
            btn_DeleteNews.Visible = false;
            btn_DeleteCancel.Visible = false;
            rbl_headline.SelectedIndex = -1;
        }
    }
}