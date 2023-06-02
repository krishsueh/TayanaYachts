<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="dealers.aspx.cs" Inherits="TayanaYachts.dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <p><span>DEALERS</span></p>
                <ul>
                    <asp:Repeater ID="CountryRepeater" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server"
                                    Text='<%# Eval("Country")%>' PostBackUrl='<%# "dealers.aspx?id=" + Eval("CountryID") %>' 
                                    OnClick="ShowDealers_Click"></asp:LinkButton>
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
            <a href="dealers.aspx">Dealers </a> >> 
            <a href="#"><span class="on1"><asp:Literal ID="NavTitle" runat="server"></asp:Literal></span></a>
        </div>

        <div class="right">
            <div class="right1">
                <div class="title">
                    <span><asp:Literal ID="CountryTitle" runat="server"></asp:Literal></span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">
                    <ul>
                        <li>
                            <asp:Repeater ID="DealerRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="list02">
                                        <ul>
                                            <li class="list02li">
                                                <div>
                                                    <p>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "upload/Dealers/" + Eval("ImgPath")%>' />
                                                    </p>
                                                </div>
                                            </li>
                                            <li>
                                                <span>
                                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("Area")%>'></asp:Literal></span>
                                                <br />
                                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Literal>
                                                <br />
                                                <asp:Literal ID="Literal3" runat="server" Text='<%# "Contact：" + Eval("ContactPerson")%>'></asp:Literal>
                                                <br />
                                                <asp:Literal ID="Literal4" runat="server" Text='<%# "Address：" + Eval("Address")%>'></asp:Literal>
                                                <br />
                                                <asp:Literal ID="Literal5" runat="server" Text='<%# "TEL：" + Eval("TEL")%>'></asp:Literal>
                                                <br />
                                                <asp:Literal ID="Literal6" runat="server" Text='<%# "E-mail：" + Eval("Email")%>'></asp:Literal>
                                                <br />
                                                <a href='<%# Eval("Website")%>' target="_blank">
                                                    <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("Website")%>'></asp:Literal></a>
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
