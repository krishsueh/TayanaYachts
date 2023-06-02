<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="CountrySorting.aspx.cs" Inherits="TayanaYachts.B_Dealers.DealerCountry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!---新增國家--->
    <div class="card p-3 mb-3 border-left-primary">
        <div class="d-none d-sm-inline-block form-inline mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
            <div class="input-group">
                <!--輸入欄位-->
                <asp:TextBox ID="tbx_addCountry" runat="server" class="form-control bg-light border-0 small" placeholder="新增國家類別"></asp:TextBox>
                <!--按鈕-->
                <div class="input-group-append">
                    <asp:LinkButton ID="btn_addCountry" runat="server" class="btn btn-primary" OnClick="btn_addCountry_Click" ToolTip="新增國家類別"><i class="fas fa-plus fa-sm"></i></asp:LinkButton>
                </div>
                <div class="ml-3 pt-2">
                    <asp:Label ID="lbl_warning" runat="server" class="badge badge-pill badge-warning text-dark"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!---表格--->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">國家類別</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CountryID" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" Width="70%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('此國家類別的 地區 與 Dealers 資料也會一併刪除，確定要刪除嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="國家名稱" SortExpression="Country">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="地區">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpl_goArea" runat="server" NavigateUrl='<%# "AreaSorting.aspx?country="+Eval("CountryID") %>'>
                                    <input id="btn_goArea" type="button" value="管理" class=" border border-secondary py-0 px-2 rounded" />
                            </asp:HyperLink>
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
