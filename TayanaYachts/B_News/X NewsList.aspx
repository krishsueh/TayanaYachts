<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="X NewsList.aspx.cs" Inherits="TayanaYachts.B_News.NewsList" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--這一頁是模仿龜人學長做出來的，後來沒有使用，而是用的 Headline.aspx。-->

    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">News 標題管理</h5>
        </div>

        <div class="card-body">
            <p>發文日期 :</p>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#3399FF" ForeColor="White" Font-Bold="True" />
                <TitleStyle BackColor="White" BorderColor="#3399FF" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#3399FF" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
            <hr />
            <p>標題 :</p>
            <asp:RadioButtonList ID="rbl_headline" runat="server" class="my-3" AutoPostBack="True" OnSelectedIndexChanged="rbl_headline_SelectedIndexChanged"></asp:RadioButtonList>
            <asp:Button ID="btn_DeleteCancel" runat="server" Text="取消" type="button" class="btn btn-outline-danger btn-sm" Visible="False" OnClick="btn_DeleteCancel_Click" />
            <asp:Button ID="btn_DeleteNews" runat="server" Text="刪除" type="button" class="btn btn-danger btn-sm" OnClientClick="return confirm('確定要刪除此標題嗎?')" Visible="False" OnClick="btn_DeleteNews_Click" />
            <hr />
            <p>新增標題 :</p>
            <asp:CheckBox ID="ckb_goTop" runat="server" Text="置頂" Width="100%" />
            <asp:TextBox ID="tbx_headline" runat="server" type="text" class="form-control" placeholder="請輸入欲新增之標題名稱" MaxLength="75"></asp:TextBox>
            <asp:Label ID="warning" runat="server" ForeColor="Red" Visible="False" class="badge badge-pill badge-warning text-dark"></asp:Label>
            <asp:Button ID="btn_AddHeadline" runat="server" Text="新增" class="btn btn-outline-primary btn-block mt-3" OnClick="btn_AddHeadline_Click" />

        </div>
    </div>
</asp:Content>
