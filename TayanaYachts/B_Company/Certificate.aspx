<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Certificate.aspx.cs" Inherits="TayanaYachts.B_Company.Certificate" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Certificate</h5>
        </div>
        <div class="card-body">
            <CKEditor:CKEditorControl ID="CKEditor_Certificate" runat="server" BasePath="/Scripts/ckeditor/"
                Toolbar="Source
                    Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image|Iframe"
                Height="400px"></CKEditor:CKEditorControl>
            <asp:Button ID="btn_TextUpload" runat="server" Text="存檔" class="btn btn-outline-primary btn-block mt-3" OnClick="btn_TextUpload_Click" />
            <hr />
            <asp:Label ID="lbl_LastUpdated" runat="server" ForeColor="Gray" class="d-flex justify-content-center" Font-Size="Smaller"></asp:Label>
        </div>
    </div>
</asp:Content>
