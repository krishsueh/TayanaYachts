<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Copy.aspx.cs" Inherits="TayanaYachts.B_News.Copy" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">內文管理</h5>
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
                    <asp:Button ID="btn_UploadSummary" runat="server" Text="存檔" class="btn btn-outline-primary btn-block mt-3" Font-Size="Smaller" OnClick="btn_UploadSummary_Click" />
                </div>
            </div>
            <hr />

            <p>◎NEWS 內文：<asp:Label ID="lbl_Updated" runat="server" ForeColor="Gray" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label></p>
            <ckeditor:ckeditorcontrol id="CKEditorControl1" runat="server" basepath="/Scripts/ckeditor/"
                toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image|Iframe"
                height="200px">
            </ckeditor:ckeditorcontrol>
            <asp:Button ID="btn_UploadNews" runat="server" Text="存檔" class="btn btn-outline-primary btn-block mt-3" OnClick="btn_UploadNews_Click" />

            <hr />

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
            <asp:CheckBoxList ID="CheckBoxList1" runat="server" CellPadding="30" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged"></asp:CheckBoxList>
            <asp:Button ID="btn_ImgDelete" runat="server" Text="刪除圖片" type="button" class="btn btn-danger btn-sm" OnClick="btn_ImgDelete_Click" Visible="False" />

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

            <br />
            <br />

            <asp:Label ID="AttachedFile" runat="server" Text="◎已上傳附件："></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList2" runat="server" CellPadding="30" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList2_SelectedIndexChanged"></asp:CheckBoxList>
            <asp:Button ID="btn_FileDelete" runat="server" Text="刪除附檔" type="button" class="btn btn-danger btn-sm" Visible="False" OnClick="btn_FileDelete_Click" />

            <hr />

            <div class="d-flex flex-row-reverse">
                <div class="ml-2">
                    <input id="Button1" type="button" value="回上頁" class="btn btn-outline-primary btn-sm" onclick="self.location.href='NewsList.aspx'" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
