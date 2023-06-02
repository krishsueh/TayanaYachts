<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TayanaYachts.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>Tayana Yachts</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet" />

    <link href="css/jquery.loading.css" rel="stylesheet" />

</head>
<body id="loading" class="bg-gradient-primary">

    <form id="form1" runat="server">
        <div class="container">
            <!-- Outer Row -->
            <div class="row justify-content-center">

                <div class="col-xl-10 col-lg-12 col-md-9">

                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row">
                                <!--<div class="col-lg-6 d-none d-lg-block bg-login-image"></div>-->

                                <img alt="" class="col-lg-6 d-none d-lg-block" src="images/TayanaYachts.png" style="background-position: center; background-size: cover" />

                                <div class="col-lg-6">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Tayana Yachts</h1>
                                        </div>
                                        <div class="user">
                                            <div class="form-group">
                                                <asp:TextBox ID="tbx_AccountName" runat="server" CssClass="form-control form-control-user"
                                                    placeholder="Username"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:TextBox ID="tbx_Password" runat="server" TextMode="Password"
                                                    CssClass="form-control form-control-user" placeholder="Password"></asp:TextBox>
                                            </div>
                                            <asp:Button ID="btn_Login" runat="server" Text="登入" CssClass="btn btn-primary btn-user btn-block" OnClick="btn_Login_Click" />
                                            <br />
                                            <asp:Label ID="lbl_Error" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            <br />
                                        </div>
                                        <hr />
                                        <div class="text-center">
                                            <a class="small" href="forgot-password.html">忘記密碼?</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </div>

        <!-- Bootstrap core JavaScript-->
        <script src="vendor/jquery/jquery.min.js"></script>
        <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

        <!-- Core plugin JavaScript-->
        <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

        <!-- Custom scripts for all pages-->
        <script src="js/sb-admin-2.min.js"></script>

        <!-- Loading Function-->
        <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
        <script src="Scripts/jquery.loading.js"></script>
        <script type="text/javascript">
            $("#btn_Login").click(function () {
                $("#loading").loading({theme: 'dark'});
            });
        </script>

    </form>
</body>
</html>
