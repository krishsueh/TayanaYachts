<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="TayanaYachts.B_Account.Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex flex-row card p-3 mb-3 border-left-primary">
        <!---新增員工--->
        <div>
            <a href="AddAccount.aspx">
                <input id="Link_addAccount" type="button" value="新增員工" class="btn btn-outline-primary btn-sm" /></a>
        </div>
    </div>

    <!---表格--->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">帳號列表</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="5">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('確定要刪除此帳號嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="權限" SortExpression="Authority">
                        <ItemTemplate>
                            <asp:Label ID="tbx_Authority" runat="server" Text='<%# Bind("Authority") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="姓名" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="帳號" SortExpression="AccountName">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("AccountName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="系統權限">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Yachts" runat="server" Checked='<%# Eval("Yachts_R").ToString() == "True" ? true : false %>' Enabled="False" Text=" Yachts" />
                            &nbsp; &nbsp;<asp:CheckBox ID="chk_News" runat="server" Checked='<%# Eval("News_R").ToString() == "True" ? true : false %>' Enabled="False" Text=" News" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chk_Company" runat="server" Checked='<%# Eval("Company_R").ToString() == "True" ? true : false %>' Enabled="False" Text=" Company" />
                            <asp:CheckBox ID="chk_Dealers" runat="server" Checked='<%# Eval("Dealers_R").ToString() == "True" ? true : false %>' Enabled="False" Text=" Dealers" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chk_Account" runat="server" Checked='<%# Eval("Account_M").ToString() == "True" ? true : false %>' Enabled="False" Text=" Account" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="編輯" ShowHeader="False">
                        <ItemTemplate>
                            <a href='<%# "AccountInfo.aspx?id=" + Eval("UserID") %>'>
                                <asp:Image ImageUrl="~/images/icon/edit.png" runat="server" Width="20px" />
                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#858796" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

        </div>
    </div>
</asp:Content>
