using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TayanaYachts
{
    public class UserData
    {
        public static string Name;
        public static string AccountName;
        public static string Authority;
        public static bool Yachts_R;
        //public static bool News_R;
        //public static bool Company_R;
        //public static bool Dealers_R;
        //public static bool Account_M;


        public static void PermissionCheck()
        {
            //取得驗證票夾帶資訊
            string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            string[] ticketUserDataArr = ticketUserData.Split(';');
            bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;

            if (haveRight)
            {
                Name = ticketUserDataArr[0];
                AccountName = ticketUserDataArr[1];
                Authority = ticketUserDataArr[2];
                Yachts_R = Convert.ToBoolean(ticketUserDataArr[3]);
                //News_R = Convert.ToBoolean(ticketUserDataArr[4]);
                //Company_R = Convert.ToBoolean(ticketUserDataArr[5]);
                //Dealers_R = Convert.ToBoolean(ticketUserDataArr[6]);
                //Account_M = Convert.ToBoolean(ticketUserDataArr[7]);
            }
        }
    }
}