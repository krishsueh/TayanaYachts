<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TayanaYachts.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!---篩選器--->
        <asp:DropDownList ID="ddl_CountrySorting" runat="server" DataSourceID="SqlDataSource1" DataTextField="Country" DataValueField="CountryID" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddl_CountrySorting_SelectedIndexChanged">
            <asp:ListItem Value="0">篩選國家類別</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [CountryID], [Country] FROM [dealersCountry] ORDER BY [Country]"></asp:SqlDataSource>

        <asp:DropDownList ID="ddl_AreaSorting" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Area" DataValueField="AreaID" AppendDataBoundItems="True" OnSelectedIndexChanged="ddl_AreaSorting_SelectedIndexChanged" Visible="False">
            <asp:ListItem Value="0">篩選地區類別</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [AreaID], [Area] FROM [dealersArea] WHERE ([CountryID] = @CountryID) ORDER BY [Area]">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddl_CountrySorting" DefaultValue="0" Name="CountryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <hr class="sidebar-divider" />
        <!---新增刪除 dealers--->
        <div>
            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="img/icon/add.png" ImageWidth="25px" NavigateUrl="WebForm3.aspx"></asp:HyperLink>
            <br />
            <br />
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DealersID" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="allSelect" runat="server" AutoPostBack="True" OnCheckedChanged="allSelect_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="dealerSelector" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="圖片" SortExpression="ImgPath">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "WebForm2.aspx?DealersID=" + Eval("DealersID") %>' ImageUrl='<%# "img/Tayana/dealers/" + Eval("ImgPath") %>' ImageWidth="100px"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="公司名稱" SortExpression="CompanyName">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Bind("CompanyName") %>' NavigateUrl='<%# "WebForm2.aspx?DealersID=" + Eval("DealersID") %>' Font-Underline="False" ForeColor="Blue"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="對應類別">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Country")+" → "+Eval("Area") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="刪除">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="img/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('確定要刪除嗎?')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
