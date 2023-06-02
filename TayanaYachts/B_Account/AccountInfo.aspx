<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="TayanaYachts.B_Account.AccountInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">帳號資料 新增/修改</h5>
        </div>
        <div class="card-body">
            <div>
                <asp:Label ID="Label1" runat="server" Text="職等" Width="100px"></asp:Label>
                <asp:DropDownList ID="ddl_Authority" runat="server" DataSourceID="SqlDataSource1" DataTextField="Authority" DataValueField="Authority" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="warning1" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT DISTINCT [Authority] FROM [users] ORDER BY Authority DESC"></asp:SqlDataSource>

                <hr class="sidebar-divider" />

                <asp:Label ID="Label9" runat="server" Text="到職日" Width="100px"></asp:Label>
                <asp:Label ID="lbl_OnBoardDate" runat="server"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label2" runat="server" Text="姓名" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Name" runat="server" Width="70%"></asp:TextBox>
                <asp:Label ID="warning2" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label3" runat="server" Text="登入帳號" Width="100px"></asp:Label>
                <asp:Label ID="lbl_AccountName" runat="server" Width="70%"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label4" runat="server" Text="密碼" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Password" runat="server" Width="70%" TextMode="Password"></asp:TextBox>
                <asp:Label ID="warning3" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label5" runat="server" Text="E-Mail" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Email" runat="server" Width="70%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label6" runat="server" Text="手機" Width="99px"></asp:Label>
                <asp:TextBox ID="tbx_Mobile" runat="server" Width="70%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label7" runat="server" Text="地址" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Address" runat="server" Width="70%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label8" runat="server" Text="系統權限" Width="100px"></asp:Label>
                <asp:CheckBoxList ID="CheckBoxList_System" runat="server" AutoPostBack="False" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem class="mr-4">Yachts</asp:ListItem>
                    <asp:ListItem class="mr-4">News</asp:ListItem>
                    <asp:ListItem class="mr-4">Company</asp:ListItem>
                    <asp:ListItem class="mr-4">Dealers</asp:ListItem>
                    <asp:ListItem>帳號管理</asp:ListItem>
                </asp:CheckBoxList>
            </div>

            <div class="d-flex flex-row-reverse">
                <div class="ml-2">
                    <input id="Button1" type="button" class="btn btn-outline-primary btn-sm" value="回上頁" onclick="self.location.href='Account.aspx'" />
                </div>
                <div>
                    <asp:Button ID="btn_Save" runat="server" Text="存檔" class="btn btn-outline-primary btn-sm" Width="80px" OnClick="btn_Save_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
