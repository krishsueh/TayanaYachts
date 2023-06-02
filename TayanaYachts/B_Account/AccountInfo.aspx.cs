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
using System.Windows.Forms;


namespace TayanaYachts.B_Account
{
    public partial class AccountInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAccount();
            }
        }

        protected void BindAccount()
        {
            SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM users WHERE UserID = @id";
            SqlCommand cmd = new SqlCommand(sql, cnn);

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ddl_Authority.SelectedValue = reader["Authority"].ToString();
                DateTime date = DateTime.Parse(reader["OnBoard"].ToString());
                lbl_OnBoardDate.Text = date.ToString("D");
                tbx_Name.Text = reader["Name"].ToString();
                lbl_AccountName.Text = reader["AccountName"].ToString();
                tbx_Email.Text = reader["Email"].ToString();
                tbx_Mobile.Text = reader["Mobile"].ToString();
                tbx_Address.Text = reader["Address"].ToString();
                CheckBoxList_System.Items[0].Selected = Convert.ToBoolean(reader["Yachts_R"]);
                CheckBoxList_System.Items[1].Selected = Convert.ToBoolean(reader["News_R"]);
                CheckBoxList_System.Items[2].Selected = Convert.ToBoolean(reader["Company_R"]);
                CheckBoxList_System.Items[3].Selected = Convert.ToBoolean(reader["Dealers_R"]);
                CheckBoxList_System.Items[4].Selected = Convert.ToBoolean(reader["Account_M"]);
            }
            cnn.Close();
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

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            warning1.Visible = false;
            warning2.Visible = false;
            if (ddl_Authority.SelectedValue != "0" && tbx_Name.Text != "" && tbx_Password.Text != "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE users SET Authority=@Authority, Name=@Name, Password=@Password, Salt=@Salt, Email=@Email, Mobile=@Mobile, Address=@Address, " +
                    "Yachts_R=@Yachts_R, News_R=@News_R, Company_R=@Company_R, Dealers_R=@Dealers_R, Account_M=@Account_M " +
                    "WHERE UserID=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                //Hash 加鹽加密
                string password = tbx_Password.Text;
                var salt = CreateSalt();
                string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
                var hash = HashPassword(password, salt);
                string hashPassword = Convert.ToBase64String(hash);

                cmd.Parameters.AddWithValue("@Authority", ddl_Authority.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Name", tbx_Name.Text);
                cmd.Parameters.AddWithValue("@Password", hashPassword);
                cmd.Parameters.AddWithValue("@Salt", saltStr);
                cmd.Parameters.AddWithValue("@Email", tbx_Email.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbx_Mobile.Text);
                cmd.Parameters.AddWithValue("@Address", tbx_Address.Text);
                cmd.Parameters.AddWithValue("@Yachts_R", CheckBoxList_System.Items[0].Selected.ToString());
                cmd.Parameters.AddWithValue("@News_R", CheckBoxList_System.Items[1].Selected.ToString());
                cmd.Parameters.AddWithValue("@Company_R", CheckBoxList_System.Items[2].Selected.ToString());
                cmd.Parameters.AddWithValue("@Dealers_R", CheckBoxList_System.Items[3].Selected.ToString());
                cmd.Parameters.AddWithValue("@Account_M", CheckBoxList_System.Items[4].Selected.ToString());
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("更新資料完畢!", "Tayana Yachts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ddl_Authority.SelectedValue != "0" && tbx_Name.Text != "" && tbx_Password.Text == "")
            {
                SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "UPDATE users SET Authority=@Authority, Name=@Name, Email=@Email, Mobile=@Mobile, Address=@Address, " +
                    "Yachts_R=@Yachts_R, News_R=@News_R, Company_R=@Company_R, Dealers_R=@Dealers_R, Account_M=@Account_M " +
                    "WHERE UserID=@id";
                SqlCommand cmd = new SqlCommand(sql, cnn);

                cmd.Parameters.AddWithValue("@Authority", ddl_Authority.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Name", tbx_Name.Text);
                cmd.Parameters.AddWithValue("@Email", tbx_Email.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbx_Mobile.Text);
                cmd.Parameters.AddWithValue("@Address", tbx_Address.Text);
                cmd.Parameters.AddWithValue("@Yachts_R", CheckBoxList_System.Items[0].Selected.ToString());
                cmd.Parameters.AddWithValue("@News_R", CheckBoxList_System.Items[1].Selected.ToString());
                cmd.Parameters.AddWithValue("@Company_R", CheckBoxList_System.Items[2].Selected.ToString());
                cmd.Parameters.AddWithValue("@Dealers_R", CheckBoxList_System.Items[3].Selected.ToString());
                cmd.Parameters.AddWithValue("@Account_M", CheckBoxList_System.Items[4].Selected.ToString());
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();

                MessageBox.Show("更新資料完畢!", "Tayana Yachts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (ddl_Authority.SelectedValue == "0")
                {
                    warning1.Visible = true;
                }
                if (tbx_Name.Text == "")
                {
                    warning2.Visible = true;
                }
            }



        }
    }
}