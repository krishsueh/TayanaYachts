using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_News
{
    public partial class NewsList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsList();
            }
        }

        protected void BindNewsList()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM news ORDER BY goTop DESC, dateTitle DESC";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
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
            string sql = "DELETE FROM news WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", id);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            BindNewsList();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (Request.QueryString["goTop"] == "False")
            {
                //成功置頂
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET goTop = @isTop WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@isTop", "True");
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                BindNewsList();

            }
            else
            {
                //取消置頂
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE news SET goTop = @notTop WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@notTop", "False");
                cmd.Parameters.AddWithValue("@id", GridView1.DataKeys[e.NewSelectedIndex].Value.ToString());

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                BindNewsList();

            }
        }
    }
}