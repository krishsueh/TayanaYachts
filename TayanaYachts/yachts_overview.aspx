<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="yachts_overview.aspx.cs" Inherits="TayanaYachts.yachts_overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--這一頁是一開始沒有用MasterPage做的-->

    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="371" />
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
                <p><span>YACHTS</span></p>
                <ul>
                    <asp:Repeater ID="ModelRepeater" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="btn_Model" runat="server"
                                    Text='<%# Eval("model")%>' PostBackUrl='<%# "yachts_overview.aspx?id=" + Eval("id") %>' OnClick="btn_Model_Click"></asp:LinkButton>

                                <asp:LinkButton ID="btn_NewDesign" runat="server" Visible="False"
                                    Text='<%# Eval("isNewDesign").ToString() == "True" ? Eval("model") + " (New Design)" : ""%>'
                                    PostBackUrl='<%# "yachts_overview.aspx?id=" + Eval("id") %>'
                                    OnClick="btn_NewDesign_Click"></asp:LinkButton>

                                <asp:LinkButton ID="btn_NewBuilding" runat="server" Visible="False"
                                    Text='<%# Eval("isNewBuilding").ToString() == "True" ? Eval("model") + " (New Building)" : ""%>'
                                    PostBackUrl='<%# "yachts_overview.aspx?id=" + Eval("id") %>'
                                    OnClick="btn_NewBuilding_Click"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> 
            <a href="dealers.aspx">Yachts</a>>> 
            <a href="#"><span class="on1">
                <asp:Literal ID="NavTitle" runat="server"></asp:Literal></span></a>
        </div>

        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>
                        <asp:Literal ID="ModelTitle" runat="server"></asp:Literal></span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <!--次選單-->
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <li><a class="menu_yli01" href="#">Interior</a></li>
                        <li><a class="menu_yli02" href="#">Layout & deck pla</a>n</li>
                        <li><a class="menu_yli03" href="#">Specification</a></li>
                    </ul>
                </div>
                <!--次選單-->

                <div class="box1">
                    <asp:Literal ID="Overview" runat="server"></asp:Literal>
                </div>

                <p class="topbuttom">
                    <img src="images/top.gif" alt="top" />
                </p>

                <!--下載開始-->
                <div class="downloads">
                    <p>
                        <img src="images/downloads.gif" alt="&quot;&quot;" />
                    </p>
                    <ul>
                        <asp:Repeater ID="FileRepeater" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# $"upload/Yachts/{Request.QueryString["id"]}/{Eval("FileName")}"%>' download="">
                                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("FileName")%>'></asp:Literal>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!--下載結束-->



                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
