<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="TayanaYachts.WebForm2" %>

<%@ Register Src="~/WebUserControl_Page.ascx" TagPrefix="uc1" TagName="WebUserControl_Page" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <uc1:WebUserControl_Page runat="server" ID="WebUserControl_Page" />

        </div>
    </form>
</body>
</html>
