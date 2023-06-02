<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="contact1.aspx.cs" Inherits="TayanaYachts.contact1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box1" style="margin: 165px 0">
        <div style="display: flex; justify-content: center">
            <asp:Image ID="Image1" runat="server" ImageUrl="img/icon/check.png" Height="80px" Width="80px" />
        </div>
        <div style="text-align: center; margin: 10px 0px">
            <asp:Label ID="Label1" runat="server" Text="Thank you for contacting us!" Font-Bold="True" Font-Size="Large"></asp:Label>
        </div>
        <div style="text-align: center">
            <asp:Label ID="Label2" runat="server" Text="Label">It's appreciated that you've taken time to write us.<br />We will get back to you soon.</asp:Label>
        </div>
    </div>
</asp:Content>
