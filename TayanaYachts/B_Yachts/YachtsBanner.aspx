<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="YachtsBanner.aspx.cs" Inherits="TayanaYachts.B_Yachts.YachtsBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Yachts Banner-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">橫幅輪播相簿</h5>
        </div>
        <div class="card-body">
            <p>◎圖片上傳：</p>
            <div style="display: flex">
                <asp:FileUpload ID="ImgUpload" runat="server" AllowMultiple="True" />
                &nbsp;&nbsp;
                <asp:Button ID="btn_ImgUpload" runat="server" Text="上傳" type="button" class="btn btn-outline-primary btn-sm" OnClick="btn_ImgUpload_Click" />
            </div>
            <asp:Label ID="img_warning1" runat="server" Font-Size="Smaller" Text="僅支援: jpg、jpeg、png、gif"></asp:Label>
            <br />
            <asp:Label ID="img_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>

            <br />
            <br />

            <asp:Label ID="AttachedImg" runat="server" Text="◎已上傳圖片："></asp:Label>
                <br />
            <asp:Label ID="isCover" runat="server" Text="框線圖將設為首頁封面" class="badge badge-pill badge-primary text-white" Font-Size="Smaller"></asp:Label>
            <asp:Label ID="bannerImg_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" CellPadding="30" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" ></asp:CheckBoxList>
            <div style="margin-left: 40px">
                <asp:Button ID="btn_setIndexCover" runat="server" style="margin-right: 10px" Text="設為首頁封面" type="button" class="btn btn-primary btn-sm" OnClick="btn_setIndexCover_Click" Visible="False" />
                <asp:Button ID="btn_ImgDelete" runat="server" Text="刪除圖片" type="button" class="btn btn-danger btn-sm" OnClick="btn_ImgDelete_Click" Visible="False" OnClientClick="return confirm('確定要刪除嗎?')"/>
            </div>
        </div>
    </div>
    <div class="d-flex flex-row-reverse">
        <div class="ml-2">
            <input id="Button1" type="button" value="回上頁" class="btn btn-outline-primary btn-sm mb-3" onclick="self.location.href='YachtsList.aspx'" />
        </div>
    </div>
</asp:Content>
