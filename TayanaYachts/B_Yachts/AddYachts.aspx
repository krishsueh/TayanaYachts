<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="AddYachts.aspx.cs" Inherits="TayanaYachts.B_Yachts.AddYachts" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--新增型號-->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">新增遊艇型號</h5>
        </div>
        <div class="card-body">
            <div>
                <asp:Label ID="Label1" runat="server" Text="◎型號" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_Model" runat="server" PlaceHolder="例：Tayana"></asp:TextBox>
                <asp:TextBox ID="tbx_Length" runat="server" PlaceHolder="例：37"></asp:TextBox>
                <asp:Button ID="btn_addModel" runat="server" Text="新增" class="btn btn-outline-primary btn-sm mx-3" Width="80px" OnClick="btn_addModel_Click" />
                <asp:Label ID="model_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Width="100px"></asp:Label>
                <asp:CheckBoxList ID="CheckBoxList_Yachts" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem class="mr-4">New Design</asp:ListItem>
                    <asp:ListItem>New Building</asp:ListItem>
                </asp:CheckBoxList>
            </div>
        </div>
    </div>

    <!--Overview-->
    <div id="overviewCard" runat="server" class="card mb-4">
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

            <br />

            <asp:CheckBoxList ID="CheckBoxList_File" runat="server" CellPadding="30" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="CheckBoxList_File_SelectedIndexChanged"></asp:CheckBoxList>
            <asp:Button ID="btn_FileDelete" runat="server" Text="刪除附檔" type="button" class="btn btn-danger btn-sm" Visible="False" OnClientClick="return confirm('確定要刪除嗎?')" OnClick="btn_FileDelete_Click" />
        </div>
    </div>

    <!--Layout & Deck Plan-->
    <div id="layoutCard" runat="server" class="card mb-4">
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
    <div id="specCard" runat="server" class="card mb-4">
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
    <asp:Button ID="btn_AddYachts" runat="server" Text="存檔" class="btn btn-outline-primary btn-block my-3" visible="false" OnClick="btn_AddYachts_Click"/>
</asp:Content>
