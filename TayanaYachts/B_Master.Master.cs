using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class B_Master : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //清除Cache，避免登出後按上一頁還會顯示Cache頁面
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //權限關門判斷 (Cookie)
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                String RedirectUrl = ResolveUrl("Login.aspx"); //導回登入頁
                Response.Redirect(RedirectUrl);
            }
            else
            {
                //取得驗證票夾帶資訊
                string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                string[] ticketUserDataArr = ticketUserData.Split(';');
                bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;

                //依管理權限導頁
                if (haveRight)
                {
                    SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "select * from users where AccountName=@AccountName";
                    SqlCommand cmd = new SqlCommand(sql, cnn);

                    cmd.Parameters.AddWithValue("@AccountName", ticketUserDataArr[1]);

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lbl_name.Text = ticketUserDataArr[0];  //將用戶名顯示在右上角
                        Nav_Yachts.Visible = Convert.ToBoolean(reader["Yachts_R"]);
                        Nav_News.Visible = Convert.ToBoolean(reader["News_R"]);
                        Nav_Company.Visible = Convert.ToBoolean(reader["Company_R"]);
                        Nav_Dealers.Visible = Convert.ToBoolean(reader["Dealers_R"]);
                        Nav_Authority.Visible = Convert.ToBoolean(reader["Account_M"]);
                    }
                    cnn.Close();

                }
            }

        }


        protected void btn_LogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            Session.RemoveAll();
            Session.Abandon();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            String RedirectUrl = ResolveUrl("Login.aspx"); //導回登入頁
            Response.Redirect(RedirectUrl);
        }

    }
}