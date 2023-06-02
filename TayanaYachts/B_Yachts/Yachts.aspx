<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Yachts.aspx.cs" Inherits="TayanaYachts.B_Yachts.Yachts" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遊艇型號-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">遊艇型號</h5>
        </div>
        <div class="card-body">
            <asp:Label ID="Label1" runat="server" Text="◎型號" Width="100px"></asp:Label>
            <asp:Literal ID="Lit_Model" runat="server"></asp:Literal>
            <br />
            <asp:Label ID="Label2" runat="server" Width="100px"></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList_Yachts" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem class="mr-4">New Design</asp:ListItem>
                <asp:ListItem>New Building</asp:ListItem>
            </asp:CheckBoxList>
        </div>
    </div>

    <!--Overview-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Overview</h5>
        </div>
        <div class="card-body">
            <p>◎簡介：</p>
            <CKEditor:CKEditorControl ID="CKEditor_Overview" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize|Table
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="260px">
            </CKEditor:CKEditorControl>

            <hr />

            <p>◎規格：</p>
            <CKEditor:CKEditorControl ID="CKEditor_Dimension" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize|Table
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="400px">
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
            <asp:Button ID="btn_FileDelete" runat="server" Text="刪除附檔" type="button" class="btn btn-danger btn-sm" Visible="False" OnClick="btn_FileDelete_Click" OnClientClick="return confirm('確定要刪除嗎?')" />

        </div>
    </div>

    <!--Layout & Deck Plan-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Layout & Deck Plan</h5>
        </div>
        <div class="card-body">
            <CKEditor:CKEditorControl ID="CKEditor_Layout" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize|Table
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="400px">
            </CKEditor:CKEditorControl>
        </div>
    </div>

    <!--Specification-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Specification</h5>
        </div>
        <div class="card-body">
            <CKEditor:CKEditorControl ID="CKEditor_Spec" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize|Table
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="400px">
            </CKEditor:CKEditorControl>
        </div>
    </div>

    <div class="d-flex flex-row-reverse mb-3">
        <div>
            <input id="Button1" type="button" class="btn btn-outline-primary btn-sm ml-3" value="回上頁" onclick="self.location.href='YachtsList.aspx'" />
        </div>
        <div>
            <asp:Button ID="btn_UpdateYachts" runat="server" Text="更新" class="btn btn-outline-primary btn-sm" Width="80px" OnClick="btn_UpdateYachts_Click" />
        </div>
    </div>

</asp:Content>
