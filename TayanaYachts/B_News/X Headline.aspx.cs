using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_News
{
    public partial class Headline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.SelectedDate = Calendar1.TodaysDate; //預設選取當日日期
                loadDayNewsHeadline();
            }
            lbl_warning.Text = "";
        }

        private void loadDayNewsHeadline()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM news WHERE dateTitle = @dateTitle ORDER BY goTop DESC, initDate DESC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@dateTitle", Calendar1.SelectedDate.ToString("yyyy-M-dd"));

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            cnn.Close();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();

            //刪除標題前先刪除該標題的資料夾
            string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
            if (Directory.Exists(folderName + id))
            {
                Directory.Delete(folderName + id, true);  //加 true 就可以連同資料夾內的檔案一起刪除，不然會報錯
            }

            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "delete from news where id =" + id;
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect(Request.Url.ToString());
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (Request.QueryString["goTop"] == "False")
            {
                Response.Write("進入SelectedIndexChanging()，Request.QueryString[goTop]判斷為:" + Request.QueryString["goTop"] + "(上)");

                //成功置頂
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET goTop = @isTop WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@isTop", "True");
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                loadDayNewsHeadline();

            }
            else
            {
                Response.Write("進入SelectedIndexChanging()，Request.QueryString[goTop]判斷為:" + Request.QueryString["goTop"] + "(下)");

                //取消置頂
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET goTop = @notTop WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@notTop", "False");
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                loadDayNewsHeadline();

            }
        }

        protected void btn_AddHeadline_Click(object sender, EventArgs e)
        {

            if (tbx_addHeadline.Text == "")
            {
                lbl_warning.Text = "請輸入標題!";
            }
            else
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT * FROM news WHERE headline = @headline";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@headline", tbx_addHeadline.Text);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lbl_warning.Text = "此標題已存在!";
                }
                else
                {
                    AddHeadline();
                }
                cnn.Close();
            }
        }

        protected void AddHeadline()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "INSERT INTO news (dateTitle, headline, goTop) VALUES (@dateTitle, @headline, @goTop)";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@dateTitle", Calendar1.SelectedDate.ToString("yyyy-M-dd"));
            cmd.Parameters.AddWithValue("@headline", tbx_addHeadline.Text);
            cmd.Parameters.AddWithValue("@goTop", "False");

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            //渲染畫面
            loadDayNewsHeadline();
            lbl_warning.Text = "新增標題成功!";

            //建立標題的同時，創立該標題的資料夾存放圖片或附檔
            createDirectory();

            //清空輸入欄位
            tbx_addHeadline.Text = "";
        }

        protected void createDirectory()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM news WHERE headline = @headline";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@headline", tbx_addHeadline.Text);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string folderName = @"C:\Users\KRIS\Desktop\後端課程\20230119 YachtsProject\TayanaYachts\TayanaYachts\upload\News\";
                string pathString = Path.Combine(folderName, reader["id"].ToString());

                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
            }
            cnn.Close();
        }

    }
}