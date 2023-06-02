<%@ Page Title="" Language="C#" MasterPageFile="~/YachtsMaster.Master" AutoEventWireup="true" CodeBehind="Yachts_Layout.aspx.cs" Inherits="TayanaYachts.Yachts_Layout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box6">
        <p>Layout & deck plan</p>
        <asp:Literal ID="Layout_CKeditor" runat="server"></asp:Literal>
    </div>
</asp:Content>
