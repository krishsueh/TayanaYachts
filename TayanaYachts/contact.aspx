<%@ Page Title="" Language="C#" MasterPageFile="~/F_Master.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="TayanaYachts.contact" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/contact.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------換圖開始---------------------------------------------------->
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>CONTACT</span></p>
                <ul>
                    <li><a href="contact.aspx">Contact</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb">
            <a href="index.aspx">Home</a> >> 
            <a href="#"><span class="on1">
                <asp:Literal ID="NavTitle" runat="server" Text="Contact"></asp:Literal></span></a>
        </div>

        <div class="right">
            <div class="right1">
                <div class="title">
                    <span>Contact</span>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->
                <!--表單-->
                <div class="from01">
                    <p>
                        Please Enter your contact information<span class="span01">*Required</span>
                    </p>
                    <br />
                    <table>
                        <tr>
                            <td class="from01td01">Name :</td>
                            <td>
                                <span>*</span>
                                <asp:TextBox ID="Name" runat="server" type="text"
                                    class="{validate:{required:true, messages:{required:'Required'}}}" Style="width: 250px;" required=""
                                    aria-required="true" oninput="setCustomValidity('');" oninvalid="setCustomValidity('Required!')" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="from01td01">Email :</td>
                            <td>
                                <span>*</span>
                                <asp:TextBox ID="Email" runat="server" type="text"
                                    class="{validate:{required:true, email:true, messages:{required:'Required', email:'Please check the E-mail format is correct'}}}"
                                    Style="width: 250px;" required="" aria-required="true" oninput="setCustomValidity('');"
                                    oninvalid="setCustomValidity('Required!')" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="from01td01">Phone :</td>
                            <td>
                                <span>*</span>
                                <asp:TextBox ID="Phone" runat="server" type="text"
                                    class="{validate:{required:true, messages:{required:'Required'}}}" Style="width: 250px;" required=""
                                    aria-required="true" oninput="setCustomValidity('');" oninvalid="setCustomValidity('Required!')" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="from01td01">Country :</td>
                            <td><span>*</span>
                                <asp:DropDownList ID="Country" runat="server" DataSourceID="SqlDataSource1" DataTextField="Country" DataValueField="CountryID" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [CountryID], [Country] FROM [dealersCountry] ORDER BY [Country]"></asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                        </tr>

                        <tr>
                            <td class="from01td01">&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="Yachts" runat="server" DataSourceID="SqlDataSource2" DataTextField="model" DataValueField="id" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:TayanaYachtsConnectionString %>" SelectCommand="SELECT [id], [model] FROM [yachts] ORDER BY [id] DESC"></asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td class="from01td01">Comments:</td>
                            <td>
                                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" Rows="2" cols="20" Style="height: 150px; width: 330px;" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>

                        <!--Captcha-->
                        <tr>
                            <td class="from01td01" colspan="2">
                                <div style="display: flex; gap: 10px; justify-content: center; align-items: center">
                                    <div>
                                        <asp:Image ID="imgCaptcha" ImageUrl="Captcha.ashx" runat="server" />
                                    </div>
                                    <div>
                                        <div style="display: flex; align-items: center; gap: 10px">
                                            <asp:TextBox ID="txtCaptcha" runat="server" Placeholder="Enter code"></asp:TextBox>
                                            <asp:ImageButton ID="reCaptcha" runat="server" ImageUrl="img/icon/update.png" Width="20px" OnClick="reCaptcha_Click" />
                                        </div>
                                        <div>
                                            <asp:Label ID="Warning" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="f_right from01td01" colspan="2">
                                <asp:ImageButton ID="ContactSubmit" runat="server" type="image" src="images/buttom03.gif" Style="border-width: 0px;" Height="25px" OnClick="ContactSubmit_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <!--表單-->

                <div class="box1">
                    <span class="span02">Contact with us</span><br />
                    Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
                    As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers. 
                    If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
                </div>

                <div class="list03">
                    <p>
                        <span>TAYANA HEAD OFFICE</span><br />
                        NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                        tel. +886(7)641 2422<br />
                        fax. +886(7)642 3193<br />
                        info@tayanaworld.com<br />
                    </p>
                </div>

                <div class="list03">
                    <p>
                        <span>SALES DEPT.</span><br />
                        +886(7)641 2422  ATTEN. Mr.Basil Lin<br />
                        <br />
                    </p>
                </div>

                <div class="box4">
                    <h4>Location</h4>
                    <p>
                        <iframe width="695" height="518" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15175.561893526507!2d120.36929846556066!3d22.50846171363277!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3471e297f292ef73%3A0x99f03ba7afab5cec!2z5aSn5rSL6YGK6ImH5LyB5qWt6IKh5Lu95pyJ6ZmQ5YWs5Y-4!5e0!3m2!1szh-TW!2stw!4v1675130997491!5m2!1szh-TW!2stw"></iframe>
                    </p>
                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
