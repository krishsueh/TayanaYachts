<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="news1.aspx.cs" Inherits="TayanaYachts.news1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <!--遮罩結束-->

    <!--<div id="buttom01"><a href="#"><img src="images/buttom01.gif" alt="next" /></a></div>-->

    <!--小圖開始-->
    <!--<div class="bannerimg">
                <ul>
                    <li><a href="#">
                        <div class="on">
                            <p class="bannerimg_p">
                                <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                        </div>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" width="300" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                </ul>
                <ul>
                    <li><a class="on" href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li>
                        <p class="bannerimg_p"><a href="#">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                    <li><a href="#">
                        <p class="bannerimg_p">
                            <img src="images/pit003.jpg" alt="&quot;&quot;" /></p>
                    </a></li>
                </ul>
            </div>-->
    <!--小圖結束-->

    <!--<div id="buttom02"> <a href="#"><img src="images/buttom02.gif" alt="next" /></a></div>-->

    <!--------------------------------換圖開始---------------------------------------------------->
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="news.aspx">News & Events</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> 
            <a href="news.aspx">News</a>>> 
            <a href="#"><span class="on1">
                <asp:Literal ID="NavTitle" runat="server" Text="News & Events"></asp:Literal></span></a>
        </div>

        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>News & Events</span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    <h4>
                        <asp:Literal ID="title" runat="server"></asp:Literal></h4>
                </div>

                <div style="margin-bottom: 30px">
                    <asp:Literal ID="content" runat="server"></asp:Literal>
                </div>

                <div class="box3" style="display: flex; flex-wrap: wrap; gap: 10px">
                    <asp:Repeater ID="ImgRepeater" runat="server">
                        <ItemTemplate>
                            <p>
                                <asp:Image ID="Image" runat="server" ImageUrl='<%# "upload/News/" + Request.QueryString["id"] + "/" + Eval("ImgFileName") %>' /></p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <!--下載開始-->
                <div class="downloads">
                    <p>
                        <img src="images/downloads.gif" alt="&quot;&quot;" />
                    </p>
                    <ul>
                        <asp:Repeater ID="FileRepeater" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# $"upload/News/{Request.QueryString["id"]}/{Eval("FileName")}"%>' download="">
                                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("FileName")%>'></asp:Literal>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!--下載結束-->

                <div class="buttom001">
                    <a href="news.aspx">
                        <img src="images/back.gif" alt="&quot;&quot;" width="55" height="28" /></a>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
