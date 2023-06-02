<%@ Page Title="" Language="C#" MasterPageFile="~/YachtsMaster.Master" AutoEventWireup="true" CodeBehind="YachtsOverView.aspx.cs" Inherits="TayanaYachts.YachtsOverView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box1">
        <asp:Literal ID="Overview_CKeditor" runat="server"></asp:Literal>
    </div>
    <div class="box3">
        <asp:Literal ID="Dimension_CKeditor" runat="server"></asp:Literal>
    </div>

    <p class="topbuttom">
        <img src="images/top.gif" alt="top" style="cursor: pointer"/>
    </p>

    <!--下載開始-->
    <div id="downloadArea" runat="server" class="downloads">
        <p>
            <img src="images/downloads.gif" alt="&quot;&quot;" />
        </p>
        <ul>
            <asp:Repeater ID="FileRepeater" runat="server">
                <ItemTemplate>
                    <li>
                        <a href='<%# $"upload/Yachts/{Eval("model")}/{Eval("FileName")}"%>' download="">
                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("FileName")%>'></asp:Literal>
                        </a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <!--下載結束-->
</asp:Content>
