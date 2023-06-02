<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="AreaSorting.aspx.cs" Inherits="TayanaYachts.B_Dealers.AreaSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!---新增地區--->
    <div class="card p-3 mb-3 border-left-primary">
        <div class="d-none d-sm-inline-block form-inline mr-auto ml-md-3 my-2 my-md-0 mw-100 navbar-search">
            <div class="input-group">
                <!--輸入欄位-->
                <asp:TextBox ID="tbx_addArea" runat="server" class="form-control bg-light border-0 small" placeholder="新增地區類別"></asp:TextBox>
                <!--按鈕-->
                <div class="input-group-append">
                    <asp:LinkButton ID="btn_addArea" runat="server" class="btn btn-primary" ToolTip="新增地區類別" OnClick="btn_addArea_Click"><i class="fas fa-plus fa-sm"></i></asp:LinkButton>
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
            <h5 class="m-0 font-weight-bold text-primary">地區類別</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="AreaID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" Width="70%">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="刪除" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/icon/trashCan.png" Text="刪除" Width="20px" OnClientClick="return confirm('此地區類別的 Dealers 資料也會一併刪除，確定要刪除嗎?')" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="編號">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="地區名稱" SortExpression="Area">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbx_editArea" runat="server" Text='<%# Bind("Area") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Area") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="編輯" ShowHeader="False">
                        <EditItemTemplate>
                            <asp:ImageButton ID="btn_update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/icon/update.png" Text="更新" Width="20px" />
                            &nbsp;
                            <asp:ImageButton ID="btn_cancel" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/images/icon/cancel.png" Text="取消" Width="20px" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btn_edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/icon/edit.png" Text="編輯" Width="20px" />
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

            <div class="d-flex flex-row-reverse">
                <div>
                    <input id="Button1" type="button" class="btn btn-outline-primary btn-sm" value="回上頁" onclick="self.location.href='CountrySorting.aspx'" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
