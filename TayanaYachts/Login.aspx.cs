using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Argon2 驗證加密密碼
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 2; //迭代運算次數
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }
        //驗證
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash); // LINEQ
        }

        //設定驗證票
        private void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立 Cookie
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將 Cookie 寫入回應
            Response.Cookies.Add(authenticationCookie);
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            if (tbx_AccountName.Text.Trim() == "" || tbx_Password.Text.Trim() == "")
            {
                lbl_Error.Text = "請輸入帳號和密碼!";
            }
            else
            {
                string password = tbx_Password.Text;

                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT * FROM users WHERE AccountName=@AccountName";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@AccountName", tbx_AccountName.Text);

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //將字串轉回 byte
                    byte[] hash = Convert.FromBase64String(reader["Password"].ToString());
                    byte[] salt = Convert.FromBase64String(reader["Salt"].ToString());

                    //驗證密碼
                    bool success = VerifyHash(password, salt, hash);

                    if (success)
                    {
                        Session["Authority"] = reader["Authority"].ToString();
                        //宣告驗證票要夾帶的資料 (用;區隔)
                        string userData = reader["Name"].ToString() + ";" + reader["AccountName"].ToString() + ";" + reader["Authority"].ToString();

                        //設定驗證票(夾帶資料，cookie 命名)
                        SetAuthenTicket(userData, tbx_AccountName.Text);

                        cnn.Close();
                        Response.Redirect("B_index.aspx"); //通過帳密後，進入後端管理區首頁
                    }
                    else
                    {
                        lbl_Error.Text = "密碼錯誤，請再試一次。";
                        cnn.Close();
                    }
                }
                else
                {
                    lbl_Error.Text = "帳號無效，請再試一次。";
                    cnn.Close();
                }
            }
        }
    }
}