<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="TayanaYachts.B_Dealers.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">新增 Dealers</h5>
        </div>
        <div class="card-body">
            <div>
                <asp:Label ID="Label1" runat="server" Text="國家" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:DropDownList ID="ddl_country" runat="server" DataSourceID="SqlDataSource1" DataTextField="Country" DataValueField="CountryID" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddl_country_SelectedIndexChanged">
                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="warning1" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [CountryID], [Country] FROM [dealersCountry] ORDER BY [Country]"></asp:SqlDataSource>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label2" runat="server" Text="地區" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:DropDownList ID="ddl_area" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Area" DataValueField="AreaID" AppendDataBoundItems="True">
                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="warning2" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [AreaID], [Area] FROM [dealersArea] WHERE ([CountryID] = @CountryID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddl_country" DefaultValue="0" Name="CountryID" PropertyName="SelectedValue" Type="string" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <hr class="sidebar-divider" />

                <div style="display: flex">
                    <div>
                        <div class="my-2">
                            <asp:Image ID="img_dealer" runat="server" Width="100px" />
                        </div>
                        <div>
                            <asp:FileUpload ID="ImageUpload" runat="server" Width="80%" class="mb-2" />
                            <asp:Button ID="btn_UploadImg" runat="server" Text="上傳" class="btn btn-outline-primary btn-sm" OnClick="btn_UploadImg_Click" />
                        </div>
                    </div>
                </div>
                <asp:Label ID="img_warning1" runat="server" Font-Size="Smaller" Text="僅支援: jpg、jpeg、png、gif"></asp:Label>
                <br />
                <asp:Label ID="img_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>

                <hr class="sidebar-divider" />

                <asp:Label ID="Label4" runat="server" Text="公司名稱" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_CompanyName" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning3" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label5" runat="server" Text="聯絡人" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_ContactPerson" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning4" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label6" runat="server" Text="地址" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_Address" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning5" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label7" runat="server" Text="TEL" Width="92px"></asp:Label><span style="color: red;">*</span>
                <asp:TextBox ID="tbx_TEL" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning6" runat="server" Text="必填" class="badge badge-pill badge-warning text-dark" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label8" runat="server" Text="Fax" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Fax" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label9" runat="server" Text="E-mail" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Email" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label10" runat="server" Text="網址" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Website" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />
            </div>

            <div class="d-flex flex-row-reverse">
                <div>
                    <input id="Button1" type="button" class="btn btn-outline-primary btn-sm" value="回上頁" onclick="self.location.href='Front.aspx'" />
                </div>
                <div class="mx-2">
                    <input id="Reset1" type="reset" class="btn btn-outline-primary btn-sm" value="重新填寫" />
                </div>
                <div>
                    <asp:Button ID="btn_addDealer" runat="server" Text="新增" class="btn btn-outline-primary btn-sm" OnClick="btn_addDealer_Click" Width="80px" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
