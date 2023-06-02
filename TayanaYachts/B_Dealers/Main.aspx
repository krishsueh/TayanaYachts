<%@ Page Title="" Language="C#" MasterPageFile="~/B_Master.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TayanaYachts.B_Dealers.Main" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card mb-4">
        <div class="card-header">
            <h5 class="m-0 font-weight-bold text-primary">Dealers' Profile</h5>
        </div>
        <div class="card-body">
            <div>
                <asp:Label ID="Country" runat="server" Text="國家" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Country" runat="server" Enabled="False"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Area" runat="server" Text="地區" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Area" runat="server" Enabled="False"></asp:TextBox>
                <hr class="sidebar-divider" />

                <div style="display: flex">
                    <div>
                        <asp:Label ID="Image" runat="server" Text="圖片" Width="100px"></asp:Label>
                    </div>
                    <div>
                        <div class="my-2">
                            <asp:Image ID="ImgPath" runat="server" ImageWidth="100px" />
                        </div>
                        <div>
                            <asp:FileUpload ID="ImageUpload" runat="server" class="mb-2"/>
                            <asp:Button ID="btn_UploadImg" runat="server" Text="上傳" class="btn btn-outline-primary btn-sm" OnClick="btn_UploadImg_Click" />
                        </div>
                    </div>
                </div>
                <div style="margin-left: 100px">
                    <asp:Label ID="img_warning1" runat="server" Font-Size="Smaller" Text="僅支援: jpg、jpeg、png、gif"></asp:Label>
                    <br />
                    <asp:Label ID="img_warning" runat="server" class="badge badge-pill badge-warning text-dark" Font-Size="Smaller"></asp:Label>
                </div>

                <hr class="sidebar-divider" />

                <asp:Label ID="CompanyName" runat="server" Text="公司名稱" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_CompanyName" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning3" runat="server" Text="(必填)" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="ContactPerson" runat="server" Text="聯絡人" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_ContactPerson" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning4" runat="server" Text="(必填)" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Address" runat="server" Text="地址" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Address" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning5" runat="server" Text="(必填)" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="TEL" runat="server" Text="電話" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_TEL" runat="server" Width="80%"></asp:TextBox>
                <asp:Label ID="warning6" runat="server" Text="(必填)" Visible="False" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                <hr class="sidebar-divider" />

                <asp:Label ID="Fax" runat="server" Text="傳真" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Fax" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Email" runat="server" Text="E-mail" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Email" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Website" runat="server" Text="網址" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_Website" runat="server" Width="80%"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label1" runat="server" Text="建檔時間" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_initDate" runat="server" Enabled="False"></asp:TextBox>
                <hr class="sidebar-divider" />

                <asp:Label ID="Label2" runat="server" Text="更新時間" Width="100px"></asp:Label>
                <asp:TextBox ID="tbx_UpdateTime" runat="server" Enabled="False"></asp:TextBox>
                <hr class="sidebar-divider" />
            </div>

            <div class="d-flex flex-row-reverse">
                <div class="ml-2">
                    <input id="Button1" type="button" class="btn btn-outline-primary btn-sm" value="回上頁" onclick="self.location.href='Front.aspx'" />
                </div>
                <div>
                    <asp:Button ID="btn_Save" runat="server" class="btn btn-outline-primary btn-sm" Text="更新存檔" OnClick="btn_Save_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
