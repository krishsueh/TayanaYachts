using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts.B_Account
{
    public partial class AddAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Argon2 加密，產生 Salt 功能
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 2; // 迭代運算次數
            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }

        protected void addAccount()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "INSERT INTO users (Name, AccountName, Password, Salt, Email, Mobile, Address, OnBoard, Authority, Yachts_R, News_R, Company_R, Dealers_R, Account_M) VALUES (@Name, @AccountName, @Password, @Salt, @Email, @Mobile, @Address, @OnBoard, @Authority, @Yachts_R, @News_R, @Company_R, @Dealers_R, @Account_M)";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            //Hash 加鹽加密
            string password = tbx_Password.Text;
            var salt = CreateSalt();
            string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
            var hash = HashPassword(password, salt);
            string hashPassword = Convert.ToBase64String(hash);

            cmd.Parameters.AddWithValue("@Name", tbx_Name.Text.Trim());
            cmd.Parameters.AddWithValue("@AccountName", tbx_AccountName.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", hashPassword);
            cmd.Parameters.AddWithValue("@Salt", saltStr);
            cmd.Parameters.AddWithValue("@Email", tbx_Email.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", tbx_Mobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", tbx_Add.Text.Trim());
            cmd.Parameters.AddWithValue("@OnBoard", OnBoardDate.Value.Trim());
            cmd.Parameters.AddWithValue("@Authority", ddl_Authority.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Yachts_R", CheckBoxList_System.Items[0].Selected.ToString());
            cmd.Parameters.AddWithValue("@News_R", CheckBoxList_System.Items[1].Selected.ToString());
            cmd.Parameters.AddWithValue("@Company_R", CheckBoxList_System.Items[2].Selected.ToString());
            cmd.Parameters.AddWithValue("@Dealers_R", CheckBoxList_System.Items[3].Selected.ToString());
            cmd.Parameters.AddWithValue("@Account_M", CheckBoxList_System.Items[4].Selected.ToString());

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();

            Response.Redirect("Account.aspx");
        }

        protected void btn_addAccount_Click(object sender, EventArgs e)
        {
            warning1.Visible = false;
            warning2.Visible = false;
            warning3.Visible = false;
            warning4.Visible = false;
            warning5.Visible = false;

            if (ddl_Authority.SelectedIndex != 0 && OnBoardDate.Value != "" && tbx_Name.Text != "" && tbx_AccountName.Text != "" && tbx_Password.Text != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "select upper(AccountName) AS AccountName from users WHERE (AccountName = @AccountName)";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@AccountName", tbx_AccountName.Text.ToUpper());

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    warning4.Text = "此帳號已存在";
                    warning4.Visible = true;
                    cnn.Close();
                }
                else
                {
                    addAccount();
                }
            }
            else
            {
                if (ddl_Authority.SelectedIndex == 0)
                {
                    warning1.Visible = true;
                }
                if (OnBoardDate.Value == "")
                {
                    warning2.Visible = true;
                }
                if (tbx_Name.Text == "")
                {
                    warning3.Visible = true;
                }
                if (tbx_AccountName.Text == "")
                {
                    warning4.Visible = true;
                }
                if (tbx_Password.Text == "")
                {
                    warning5.Visible = true;
                }
            }
        }
    }
}