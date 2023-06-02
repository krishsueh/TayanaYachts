<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="YachtsList.aspx.cs" Inherits="TayanaYachts.B_Yachts.YachtsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!---新增遊艇型號--->
    <div class="d-flex flex-row card p-3 mb-3 border-left-primary">
        <div>
            <a href="AddYachts.aspx">
                <input id="Link_addYachts" type="button" value="新增遊艇型號" class="btn btn-outline-primary btn-sm" /></a>
        </div>
    </div>

    <!---表格--->
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">型號管理</h5>
        </div>

        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="model" OnRowDeleting="GridView1_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('確定要刪除此型號嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="型號" SortExpression="model">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Model" runat="server" Text='<%# Bind("model") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NewDesign" SortExpression="isNewDesign">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_isNewDesign" runat="server" Enabled="False" Checked='<%# Eval("isNewDesign").ToString() == "False" ? false : true %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NewBuilding" SortExpression="isNewBuilding">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_isNewBuilding" runat="server" Enabled="False" Checked='<%# Eval("isNewBuilding").ToString() == "False" ? false : true %>'></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="橫幅相簿">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpl_goYachtsBanner" runat="server" NavigateUrl='<%# "YachtsBanner.aspx?id=" + Eval("model") %>'>
                                    <input id="btn_goYachtsBanner" type="button" value="管理" class=" border border-secondary py-0 px-2 rounded" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="船型簡介">
                        <ItemTemplate>
                            <asp:HyperLink ID="hpl_goYachts" runat="server" NavigateUrl='<%# "Yachts.aspx?id=" + Eval("model") %>'>
                                    <input id="btn_goYachts" type="button" value="管理" class=" border border-secondary py-0 px-2 rounded" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#858796" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50px"/>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
