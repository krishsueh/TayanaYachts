using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace TayanaYachts
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetCaptchaText();
            }
        }

        private void SetCaptchaText()
        {
            Random oRandom = new Random();
            int iNumber = oRandom.Next(100000, 999999);
            Session["Captcha"] = iNumber.ToString();
        }

        protected void reCaptcha_Click(object sender, ImageClickEventArgs e)
        {
            SetCaptchaText();
        }

        protected void SendGmail()
        {
            MailMessage mail = new MailMessage();
            NetworkCredential cred = new NetworkCredential("hbmanikin@gmail.com", "biexetltjxivvfeh");
            //收件者
            mail.To.Add(Email.Text);
            mail.Subject = "Tayana Yachts Auto-Reply";
            //寄件者
            mail.From = new System.Net.Mail.MailAddress("hbmanikin@gmail.com", "Tayana Yachts");
            mail.IsBodyHtml = true;
            mail.Body =
                $"Hi {Name.Text.Trim()}," +
                "<br /><p>Thanks for contactinf us, here below is your message. We will get back to you soon.</p><br />" +
                $"<p>Name : {Name.Text.Trim()}</p>" +
                $"<p>Email : {Email.Text.Trim()}</p>" +
                $"<p>Phone : {Phone.Text.Trim()}</p>" +
                $"<p>Country : {Country.SelectedItem}</p>" +
                $"<p>Type : {Yachts.SelectedItem}</p>" +
                $"<p>Comments : </p>" +
                $"{Comments.Text.Trim()}<br /><br /><br />" +
                "Best Regards,<br /> Tayana Yachts";

            //設定SMTP
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            //送出Mail
            smtp.Send(mail);
        }

        protected void ContactSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["Captcha"].ToString() != txtCaptcha.Text.Trim())
            {
                Warning.Text = " Wrong code! Please try again.";
            }
            else
            {
                SendGmail();
                Response.Redirect("contact1.aspx");
            }
        }


    }
}