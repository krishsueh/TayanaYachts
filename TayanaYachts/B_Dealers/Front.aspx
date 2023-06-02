<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Front.aspx.cs" Inherits="TayanaYachts.B_Dealers.Front" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex flex-row card p-3 mb-3 border-left-primary">
        <!---新增刪除 dealers--->
        <div>
            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/images/icon/add.png" ImageWidth="25px" NavigateUrl="Add.aspx" ToolTip="新增 Dealers"></asp:HyperLink>
        </div>
        <!---篩選器--->
        <div class="ml-4">
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
        </div>
    </div>

    <!---表格--->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Dealers</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DealersID" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="allSelect" runat="server" AutoPostBack="True" OnCheckedChanged="allSelect_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="dealerSelector" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="圖片" SortExpression="ImgPath">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "Main.aspx?DealersID=" + Eval("DealersID") %>' ImageUrl='<%# "~/upload/Dealers/" + Eval("ImgPath") %>' ImageWidth="100px"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="公司名稱" SortExpression="CompanyName">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Bind("CompanyName") %>' NavigateUrl='<%# "Main.aspx?DealersID=" + Eval("DealersID") %>' Font-Underline="False" ForeColor="Blue"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="對應類別">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Country")+" → "+Eval("Area") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('確定要刪除嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#858796" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
