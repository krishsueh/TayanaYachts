<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="TayanaYachts.news" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/newbanner.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <!--遮罩結束-->

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
                <div class="box2_list">
                    <ul>
                        <li>
                            <asp:Repeater ID="HeadlineRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="list01">
                                        <ul>
                                            <li style="position: relative">
                                                <div>
                                                    <p>
                                                        <asp:HyperLink ID="HyperLink1" runat="server" ImageWidth="187px" ImageUrl='<%# "upload/News/"+ Eval("id") + "/" + Eval("coverImg")%>' NavigateUrl='<%# "news1.aspx?id=" + Eval("id")%>'></asp:HyperLink>
                                                    </p>
                                                </div>
                                                <div style="border: hidden; position: absolute">
                                                    <asp:Image ID="img_goTop" runat="server" ImageUrl="images/new_top01.png" Visible='<%# Eval("goTop").ToString() == "False" ? false : true %>' />
                                                </div>
                                            </li>
                                            <li>
                                                <span>
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("dateTitle2") %>'></asp:Literal></span>
                                                <br />
                                                <a href='<%# "news1.aspx?id=" + Eval("id")%>' style="color: #34A9D4; text-decoration: none">
                                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("headline") %>'></asp:Literal>

                                                </a>
                                                <br />
                                                <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("summary")%>'></asp:Literal>
                                                <br />
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>


