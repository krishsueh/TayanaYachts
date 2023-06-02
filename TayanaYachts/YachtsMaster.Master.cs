using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class YachtsMaster : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //將型號對應 id 存入 Session 與子頁共用
                getID(); //要放在 Init 不然 Content 頁會去先去抓 Session 而抓不到
            }
        }

        private void getID()
        {
            //取得網址傳值的型號對應 ID
            string yachtsModel = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT TOP 1 model FROM Yachts ORDER BY initDate DESC";
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //如果無網址傳值就用第一筆遊艇型號的 ID
                if (String.IsNullOrEmpty(yachtsModel))
                {
                    yachtsModel = reader["model"].ToString().Trim();
                }
            }
            connection.Close();
            //將 id 存入 Session 供上方列表共用
            Session["yachtsModel"] = yachtsModel;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadGallery(); //讀取並渲染上方相簿輪播
                ShowLeftMenu(); //讀取並渲染左側型號邊欄
                loadTopMenu(); //讀取並渲染型號內容上方標題及分頁列
            }
        }

        private void loadGallery()
        {
            //建立資料表存資料
            DataTable dataTable = new DataTable();
            //新增表格欄位，預設從 1 開始, 設定欄位名稱
            dataTable.Columns.AddRange(new DataColumn[1] { new DataColumn("ImageUrl") });

            //取得 Session 共用 id，Session 物件需轉回字串
            string yachts_model = Session["yachtsModel"].ToString();

            //依 id 取得遊艇輪播圖片資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT yachtsImg.ImgFileName " +
                "FROM yachts INNER JOIN yachtsImg ON yachts.id = yachtsImg.id " +
                "WHERE (yachts.model = @model) AND (yachtsImg.isIndexCover = 'False')";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@model", yachts_model);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataTable.Rows.Add($"../upload/Yachts/{yachts_model}/{reader["ImgFileName"]}");
            }

            connection.Close();
            RepeaterBanner.DataSource = dataTable;
            RepeaterBanner.DataBind();
        }

        private void ShowLeftMenu()
        {

            string urlPathStr = System.IO.Path.GetFileName(Request.PhysicalPath);

            //取得遊艇型號資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts ORDER BY initDate DESC";
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder leftMenuHtml = new StringBuilder();

            while (reader.Read())
            {
                string yachtsModel = reader["model"].ToString();
                string isNewDesign = reader["isNewDesign"].ToString();
                string isNewBuilding = reader["isNewBuilding"].ToString();
                string isNew = "";
                //依是否為新建或新設計加入標註
                if (isNewDesign.Equals("True"))
                {
                    isNew = "(New Design)";
                }
                else if (isNewBuilding.Equals("True"))
                {
                    isNew = "(New Building)";
                }
                leftMenuHtml.Append($"<li><a href='{urlPathStr}?id={yachtsModel}'>{yachtsModel} {isNew}</a></li>");
            }
            connection.Close();

            //渲染左側遊艇型號選單
            LeftMenuHtml.Text = leftMenuHtml.ToString();
        }

        private void loadTopMenu()
        {
            //取得 Session 共用 id，Session 物件需轉回字串
            string yachts_model = Session["yachtsModel"].ToString();

            //依 id 取得遊艇資料

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts WHERE model = @model";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@model", yachts_model);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder topMenuHtmlStr = new StringBuilder();
            if (reader.Read())
            {
                string yachtsModel = reader["model"].ToString();

                //加入渲染型號內容上方分類連結列表
                topMenuHtmlStr.Append($"<li><a class='menu_yli01' href='YachtsOverView.aspx?id={yachtsModel}' >OverView</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli02' href='Yachts_Layout.aspx?id={yachtsModel}' >Layout & deck plan</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli03' href='Yachts_Specification.aspx?id={yachtsModel}' >Specification</a></li>");

                //渲染畫面
                //渲染上方小連結
                LabLink.InnerText = yachtsModel;
                //渲染標題
                LabTitle.InnerText = yachtsModel;
                //渲染型號內容上方分類連結列表
                TopMenuLinkHtml.Text = topMenuHtmlStr.ToString();
            }
            connection.Close();
        }
    }
}