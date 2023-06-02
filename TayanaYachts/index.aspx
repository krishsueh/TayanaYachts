﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TayanaYachts.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="//archive.org/includes/analytics.js?v=cf34f82" type="text/javascript"></script>
    <script type="text/javascript">window.addEventListener('DOMContentLoaded', function () { var v = archive_analytics.values; v.service = 'wb'; v.server_name = 'wwwb-app222.us.archive.org'; v.server_ms = 467; archive_analytics.send_pageview({}); });</script>
    <script type="text/javascript" src="/_static/js/bundle-playback.js?v=kjExWF7-" charset="utf-8"></script>
    <script type="text/javascript" src="/_static/js/wombat.js?v=Jjml7g96" charset="utf-8"></script>
    <script type="text/javascript">
        __wm.init("https://web.archive.org/web");
        __wm.wombat("http://www.tayanaworld.com:80/index.aspx", "20170923162940", "https://web.archive.org/", "web", "/_static/",
            "1506184180");
    </script>
    <link rel="stylesheet" type="text/css" href="/_static/css/banner-styles.css?v=S1zqJCYt" />
    <link rel="stylesheet" type="text/css" href="/_static/css/iconochive.css?v=qtvMKcIJ" />
    <!-- End Wayback Rewrite JS Include -->
    <title>Tayana | Tayana Yachts Official Website
    </title>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().eq($(this).index());
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
                if (thisLi.children('input[type=hidden]').val() == 1) {
                    thisLi.children('.new').show();
                }
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

            //相簿輪撥初始值設定
            $('.title ul li:eq(0)').addClass('on');
            var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0) li:eq(0)');
            thisLi.addClass('on');
            if (thisLi.children('input[type=hidden]').val() == 1) {
                thisLi.children('.new').show();
            }

            //最新消息TOP
            $('.newstop').each(function () {
                if ($(this).nextAll('input[type=hidden]').val() == 1) {
                    $(this).show();
                }
            });
        });
    </script>

    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-30943877-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://web.archive.org/web/20170923162940/https://ssl' : 'https://web.archive.org/web/20170923162940/http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>

    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->

    <link rel="shortcut icon" href="/web/20170923162940im_/http://www.tayanaworld.com/favicon.ico" />
    <link href="css/style.css" rel="stylesheet" type="text/css" /> <!--原本的style.css修改的wordtitle的值後還是都抓不到，所以改檔名重抓-->
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contain">
            <div class="sub">
                <p><a href="index.aspx">Home</a></p>
            </div>

            <!--------------------------------選單開始---------------------------------------------------->
            <div class="menu">
                <ul>
                    <li class="menuli01"><a href="YachtsOverview.aspx"></a></li>
                    <li class="menuli02"><a href="news_list.aspx">NEWS</a></li>
                    <li class="menuli03"><a href="company.aspx">COMPANY</a></li>
                    <li class="menuli04"><a href="dealers.aspx">DEALERS</a></li>
                    <li class="menuli05"><a href="contact.aspx">CONTACT</a></li>
                </ul>
            </div>
            <!--------------------------------選單開始結束---------------------------------------------------->


            <!--遮罩-->
            <div class="bannermasks">
                <img src="images/banner00_masks2.png" alt="&quot;&quot;" />
            </div>
            <!--遮罩結束-->


            <!--------------------------------換圖開始---------------------------------------------------->
            <div id="abgne-block-20110111">
                <div class="bd">
                    <div class="banner">
                        <ul>
                            <asp:Literal ID="LitBanner" runat="server"></asp:Literal>
                        </ul>
                        <!--小圖開始-->
                        <div class="bannerimg title" style="display: none">
                            <ul>
                                <asp:Literal ID="LitBannerNum" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <!--小圖結束-->
                    </div>
                </div>
            </div>
            <!--------------------------------換圖結束---------------------------------------------------->


            <!--------------------------------最新消息---------------------------------------------------->
            <div class="news">
                <div class="newstitle">
                    <p class="newstitlep1">
                        <img src="images/news.gif" alt="news" />
                    </p>
                    <p class="newstitlep2"><a href="news_list.aspx">More>></a></p>
                </div>
                <ul>
                    <!--第一則-->
                    <li>
                        <div class="news01">
                            <!--TOP標籤-->
                            <div class="newstop">
                                <asp:Image ID="ImgTop1" runat="server" Visible="false" ImageUrl="images/new_top01.png" />
                            </div>
                            <!--TOP標籤結束-->
                            <div class="news02p1">
                                <p class="news02p1img">
                                    <asp:Literal ID="NewsImg1" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <p class="news02p2">
                                <span>
                                    <asp:Label ID="NewsDate1" runat="server" ForeColor="#02A5B8"></asp:Label></span>
                                <span>
                                    <asp:HyperLink ID="NewsSummary1" runat="server">HyperLink</asp:HyperLink></span>
                            </p>
                        </div>
                    </li>
                    <!--第一則結束-->

                    <!--第二則-->
                    <li>
                        <div class="news01">
                            <!--TOP標籤-->
                            <div class="newstop">
                                <asp:Image ID="ImgTop2" runat="server" Visible="false" ImageUrl="images/new_top01.png" />
                            </div>
                            <!--TOP標籤結束-->
                            <div class="news02p1">
                                <p class="news02p1img">
                                    <asp:Literal ID="NewsImg2" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <p class="news02p2">
                                <span>
                                    <asp:Label ID="NewsDate2" runat="server" ForeColor="#02A5B8"></asp:Label></span>
                                <span>
                                    <asp:HyperLink ID="NewsSummary2" runat="server"></asp:HyperLink></span>
                            </p>
                        </div>
                    </li>
                    <!--第二則結束-->

                    <!--第三則-->
                    <li>
                        <div class="news01">
                            <!--TOP標籤-->
                            <div class="newstop">
                                <asp:Image ID="ImgTop3" runat="server" Visible="false" ImageUrl="images/new_top01.png" />
                            </div>
                            <!--TOP標籤結束-->
                            <div class="news02p1">
                                <p class="news02p1img">
                                    <asp:Literal ID="NewsImg3" runat="server"></asp:Literal>
                                </p>
                            </div>
                            <p class="news02p2">
                                <span>
                                    <asp:Label ID="NewsDate3" runat="server" ForeColor="#02A5B8"></asp:Label></span>
                                <span>
                                    <asp:HyperLink ID="NewsSummary3" runat="server"></asp:HyperLink></span>
                            </p>
                        </div>
                    </li>
                    <!--第三則結束-->
                </ul>
            </div>
            <!--------------------------------最新消息結束---------------------------------------------------->

            <!--------------------------------落款開始---------------------------------------------------->
            <div class="footer">

                <div class="footerp00">
                    <a href="#">
                        <img src="images/tog.jpg" alt="&quot;&quot;" /></a>
                    <p class="footerp001">© 1973-2011 Tayana Yachts, Inc. All Rights Reserved</p>
                </div>
                <div class="footer01">
                    <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
                    <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span><span><a href="mailto:tayangco@ms15.hinet.net">E-mail：tayangco@ms15.hinet.net</a>.</span>
                </div>
            </div>
            <!--------------------------------落款結束---------------------------------------------------->

        </div>
    </form>
</body>
</html>
