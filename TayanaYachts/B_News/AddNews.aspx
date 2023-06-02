<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="TayanaYachts.B_News.AddNews" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--新增新聞-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">新增新聞標題</h5>
        </div>
        <div class="card-body">
            <div>
                <asp:Label ID="Label3" runat="server" Text="◎發佈日期" Width="92px"></asp:Label><span style="color: red;">*</span>
                <input id="ReleasedDate" runat="server" type="date" />
                <asp:Label ID="date_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="◎標題" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_Headline" runat="server" Width="60%"></asp:TextBox>
                <asp:Button ID="btn_addHeadline" runat="server" Text="新增" class="btn btn-outline-primary btn-sm mx-3" Width="80px" OnClick="btn_addHeadline_Click" />
                <asp:Label ID="headline_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Width="100px"></asp:Label>
                <asp:CheckBoxList ID="CheckBoxList_Yachts" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem>置頂</asp:ListItem>
                </asp:CheckBoxList>
            </div>

        </div>
    </div>
    <div id="NewsArea" class="card mb-4" runat="server">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">內文</h5>
        </div>
        <div class="card-body">
            <div style="display: flex; flex-wrap: wrap">

                <div style="width: 35%">
                    <p>◎標題縮圖：</p>
                    <div style="display: flex">
                        <div>
                            <div class="my-2">
                                <asp:Image ID="CoverPath" runat="server" Width="100%" />
                            </div>
                            <div>
                                <asp:FileUpload ID="CoverUpload" runat="server" Width="80%" class="mb-2" />
                                <asp:Button ID="btn_UploadCover" runat="server" Text="上傳" class="btn btn-outline-primary btn-sm" OnClick="btn_UploadCover_Click" />
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="cover_warning1" runat="server" Font-Size="Smaller" Text="僅支援: jpg、jpeg、png、gif"></asp:Label>
                    <br />
                    <asp:Label ID="cover_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
                </div>

                <div style="margin-left: 10%; width: 50%">
                    <p>◎標題大綱：<asp:Label ID="lbl_UpdatedSummary" runat="server" ForeColor="Gray" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label></p>
                    <asp:TextBox ID="tbx_Summary" runat="server" Height="183px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
            </div>
            <hr />

            <p>◎NEWS 內文：<asp:Label ID="lbl_Updated" runat="server" ForeColor="Gray" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label></p>
            <CKEditor:CKEditorControl ID="CKEditor_Content" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="200px">
            </CKEditor:CKEditorControl>

            <hr />

            <p>◎附件上傳：</p>
            <div style="display: flex">
                <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="True" />
                &nbsp;&nbsp;
                <asp:Button ID="btn_FileUpload" runat="server" Text="上傳" type="button" class="btn btn-outline-primary btn-sm" OnClick="btn_FileUpload_Click" />
            </div>
            <asp:Label ID="file_warning1" runat="server" Font-Size="Smaller" Text="僅支援: pdf、word、txt、rar"></asp:Label>
            <br />
            <asp:Label ID="file_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList_File" runat="server" CellPadding="30" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList_File_SelectedIndexChanged"></asp:CheckBoxList>
            <asp:Button ID="btn_FileDelete" runat="server" Text="刪除附檔" type="button" class="btn btn-danger btn-sm" Visible="False" OnClick="btn_FileDelete_Click" />
        </div>
    </div>
    <asp:Button ID="btn_AddNews" runat="server" Text="存檔" class="btn btn-outline-primary btn-block my-3" OnClick="btn_AddNews_Click" />

</asp:Content>
