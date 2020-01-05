<%@ Page Language="C#" AutoEventWireup="true"
 CodeBehind="DangNhap.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DangNhap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head runat="server">

    <title>ĐĂNG NHẬP</title>

    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
	<meta name="description" content="Rau sạch và Dịch vụ rau sạch"/>
	<meta name="author" content="ManMan89"/>
	<link rel="icon" href="images/favicon.ico"/>

	<link href="css/bootstrap.css" rel="stylesheet"/>
	<link href="css/style.css" rel="stylesheet"/>

	<!-- JUST FOR DEBUGGING PURPOSES. DON'T ACTUALLY COPY THESE 2 LINES! -->
	<!--[if lt IE 9]><script src="js/ie8-responsive-file-warning.js"></script><![endif]-->

	<script type="text/javascript" src="js/ie-emulation-modes-warning.js"></script>

	<!-- HTML5 SHIM AND RESPOND.JS IE8 SUPPORT OF HTML5 ELEMENTS AND MEDIA QUERIES -->
	<!--[if lt IE 9]>
		<script src="js/html5shiv.min.js"></script>
		<script src="js/respond.min.js"></script>
	<![endif]-->

</head>
<body>
    <form id="Form1" runat="server">

		<div class="container" style="margin-top: 71px;">
            <div class="panel panel-primary">
	            <div class="panel-heading">
		            <h3 class="panel-title">ĐĂNG NHẬP</h3>
	            </div>
	            <div class="panel-body">

                    <asp:Label ID="label_thongbao" runat="server"></asp:Label>

		            <table style="width: 100%;" class="vmk_table_form">
                        <tr>
                            <td class="title">TÀI KHOẢN HOẶC EMAIL</td>
                            <td><asp:TextBox ID="txt_tai_khoan" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="title">MẬT KHẨU</td>
                            <td><asp:TextBox ID="txt_mat_khau" runat="server" Width="100%" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="title top">MÃ XÁC NHẬN</td>
                            <td>
                                <img src="../Captcha.aspx" style="margin-top: -2px" />&nbsp;<asp:TextBox ID="txt_captcha" runat="server" Width="200px" Height="24px" autocomplete='off'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btn_dang_nhap" runat="server" Text="ĐĂNG NHẬP" class="btn btn-primary btn-sm" onclick="btn_dang_nhap_Click"/>
                                <a href="../Default.aspx" class="btn btn-primary btn-sm" target="_blank">TRANG CHỦ</a>
                            </td>
                        </tr>
                    </table>
	            </div>
            </div>
		</div>
		
		<script src="js/jquery/1.11.1/jquery.min.js"></script>
		<script src="js/bootstrap.min.js"></script>
		
		<!-- IE10 VIEWPORT HACK FOR SURFACE/DESKTOP WINDOWS 8 BUG -->
		<script src="js/ie10-viewport-bug-workaround.js"></script>

    </form>
</body>
</html>