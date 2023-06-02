using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadList();
            }
        }

        private void loadList()
        {
            //預設為第1頁
            int page = 1;
            //判斷網址後有無參數
            //也可用String.IsNullOrWhiteSpace
            if (!String.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            //設定頁面參數屬性
            //設定控制項參數: 一頁幾筆資料
            WebUserControl_Page.limit = 5;
            //設定控制項參數: 作用頁面完整網頁名稱
            WebUserControl_Page.targetPage = "WebForm2.aspx";

            //建立計算分頁資料顯示邏輯 (每一頁是從第幾筆開始到第幾筆結束)
            //計算每個分頁的第幾筆到第幾筆
            var floor = (page - 1) * WebUserControl_Page.limit + 1; //每頁的第一筆
            var ceiling = page * WebUserControl_Page.limit; //每頁的最末筆

            //將取得的資料數設定給參數 count
            int count = 36; //總資料數，可修改數字測試分頁功能是否正常

            //設定控制項參數: 總共幾筆資料
            WebUserControl_Page.totalItems = count;

            //渲染分頁控制項
            WebUserControl_Page.showPageControls();

            //設定模擬資料內容
            StringBuilder listHtml = new StringBuilder();
            for (int i = floor; i <= ceiling; i++)
            {
                if (i <= count)
                {
                    listHtml.Append($"<a href=''> --------- 第 {i} 筆資料 --------- </a></li><br /><br />");
                }
            }
            Literal1.Text = listHtml.ToString();
        }
    }
}