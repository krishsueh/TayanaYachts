<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="TayanaYachts.B_News.NewsList1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-row card p-3 mb-3 border-left-primary">
        <!---新增刪除 dealers--->
        <div>
            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/images/icon/add.png" ImageWidth="25px" NavigateUrl="AddNews.aspx" ToolTip="新增新聞標題"></asp:HyperLink>
        </div>
    </div>

    <!---表格--->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">News</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('確定要刪除嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="dateTitle" HeaderText="發佈日期" SortExpression="dateTitle" DataFormatString="{0:yyyy-MM-dd}" >

                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="新聞標題" SortExpression="headline">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("headline") %>' NavigateUrl='<%# "News.aspx?id=" + Eval("id") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="coverImg" HeaderText="coverImg" SortExpression="coverImg" Visible="False" />
                    <asp:BoundField DataField="summary" HeaderText="summary" SortExpression="summary" Visible="False" />

                    <asp:TemplateField HeaderText="置頂" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Select"
                                ImageUrl='<%# Eval("goTop").ToString() == "False" ? "~/images/icon/top.png" : "~/images/icon/check.png" %>'
                                PostBackUrl='<%# "NewsList.aspx?goTop=" + Eval("goTop") %>'
                                Text="選取" Width="25px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#858796" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
