using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadBanner();
                loadNews();
            }
        }

        private void loadBanner()
        {
            //連線資料庫取出圖片資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sqlLoad = "SELECT yachts.id, yachts.model, yachts.isNewDesign, yachts.isNewBuilding, yachtsImg.ImgFileName " +
                "FROM yachts INNER JOIN yachtsImg ON yachts.id = yachtsImg.id " +
                "WHERE isIndexCover = 'True' ORDER BY id DESC";
            SqlCommand command = new SqlCommand(sqlLoad, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder bannerHtml = new StringBuilder();
            while (reader.Read())
            {
                //遊艇型號字串用空格切割區分文字及數字
                string[] modelArr = reader["model"].ToString().Split(' ');

                //依新設計或新建造來切換顯示標籤
                string isNewDesignStr = reader["isNewDesign"].ToString();
                string isNewBuildingStr = reader["isNewBuilding"].ToString();
                string newTagStr = ""; //標籤檔名用
                string displayNewStr = "0"; // value 預設為 0 不顯示標籤

                //判斷是否顯示對應標籤
                if (isNewDesignStr.Equals("True"))
                {
                    displayNewStr = "1";
                    newTagStr = "images/new02.png";
                }
                else if (isNewBuildingStr.Equals("True"))
                {
                    displayNewStr = "1";
                    newTagStr = "images/new01.png";
                }

                //加入遊艇型號輪播圖 HTML
                bannerHtml.Append($"<li class='info' style='border-radius: 5px;height: 424px;width: 978px;'><a href='' target='_blank'><img src='upload/Yachts/{reader["model"]}/{reader["ImgFileName"]}' style='width: 978px;height: 424px;border-radius: 5px;'/></a><div class='wordtitle'>{modelArr[0]} <span>{modelArr[1]}</span><br /><p>SPECIFICATION SHEET</p></div><div class='new' style='display: none;overflow: hidden;border-radius:10px;'><img src='{newTagStr}' alt='new' /></div><input type='hidden' value='{displayNewStr}' /></li>");
            }
            connection.Close();
            //渲染畫面
            LitBanner.Text = bannerHtml.ToString();
            LitBannerNum.Text = bannerHtml.ToString(); //不顯示但影響輪播圖片數量計算
        }

        private void loadNews()
        {
            //設定首頁 3 筆新聞的時間範圍
            DateTime nowTime = DateTime.Now;
            string nowDate = nowTime.ToString("yyyy-MM-dd");
            int startDate = -1;  //只顯示往回推算1個月內的新聞
            DateTime limitTime = nowTime.AddMonths(startDate);
            string limitDate = limitTime.ToString("yyyy-MM-dd");

            //計算設定的時間範圍內新聞數量
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT COUNT(id) FROM news WHERE dateTitle >= @limitDate AND dateTitle <= @nowDate";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nowDate", nowDate);
            command.Parameters.AddWithValue("@limitDate", limitDate);
            connection.Open();

            //用 ExecuteScalar() 來算數量
            int newsNum = Convert.ToInt32(command.ExecuteScalar());
            //時間範圍設定持續往前 1 個月，直到取出新聞數量超過 3 筆
            while (newsNum < 3)
            {
                startDate--;
                limitTime = nowTime.AddDays(startDate);
                limitDate = limitTime.ToString("yyyy-MM-dd");
                SqlCommand command2 = new SqlCommand(sql, connection);
                command2.Parameters.AddWithValue("@nowDate", nowDate);
                command2.Parameters.AddWithValue("@limitDate", limitDate);
                newsNum = Convert.ToInt32(command2.ExecuteScalar());
            }
            connection.Close();

            //取出時間範圍內首 3 筆新聞，且 TOP 會放在取出項的最前面
            connection.Open();
            string sql2 = "SELECT TOP 3 * FROM news WHERE dateTitle >= @limitDate AND dateTitle <= @nowDate ORDER BY goTop DESC, initDate DESC";
            SqlCommand command3 = new SqlCommand(sql2, connection);
            command3.Parameters.AddWithValue("@nowDate", nowDate);
            command3.Parameters.AddWithValue("@limitDate", limitDate);
            SqlDataReader reader = command3.ExecuteReader();
            int count = 1; //第幾筆新聞
            while (reader.Read())
            {
                string isTopStr = reader["goTop"].ToString();
                string news_id = reader["id"].ToString();
                if (count == 1)
                {
                    //渲染第1筆新聞圖卡
                    string newsImg = reader["coverImg"].ToString();
                    NewsImg1.Text = $"<img id='thumbnail_Image1' src='upload/News/{reader["id"]}/{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    NewsDate1.Text = dateTimeTitle.ToString("yyyy/M/d");
                    NewsSummary1.Text = reader["summary"].ToString();
                    NewsSummary1.NavigateUrl = $"news1.aspx?id={news_id}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgTop1.Visible = true;
                    }
                }
                else if (count == 2)
                {
                    //渲染第2筆新聞圖卡
                    string newsImg = reader["coverImg"].ToString();
                    NewsImg2.Text = $"<img id='thumbnail_Image1' src='upload/News/{reader["id"]}/{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    NewsDate2.Text = dateTimeTitle.ToString("yyyy/M/d");
                    NewsSummary2.Text = reader["summary"].ToString();
                    NewsSummary2.NavigateUrl = $"news1.aspx?id={news_id}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgTop2.Visible = true;
                    }
                }
                else if (count == 3)
                {
                    //渲染第3筆新聞圖卡
                    string newsImg = reader["coverImg"].ToString();
                    NewsImg3.Text = $"<img id='thumbnail_Image1' src='upload/News/{reader["id"]}/{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    NewsDate3.Text = dateTimeTitle.ToString("yyyy/M/d");
                    NewsSummary3.Text = reader["summary"].ToString();
                    NewsSummary3.NavigateUrl = $"news1.aspx?id={news_id}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgTop1.Visible = true;
                    }
                }
                else break; //超過3筆後停止
                count++;
            }
            connection.Close();
        }

    }
}