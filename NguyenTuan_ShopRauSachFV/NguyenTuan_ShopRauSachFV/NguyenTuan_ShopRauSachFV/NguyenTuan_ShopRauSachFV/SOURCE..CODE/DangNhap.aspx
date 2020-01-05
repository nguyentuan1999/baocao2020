<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="DangNhap.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DangNhap_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        div#main-menu > ul#menu-box
        {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="body">
	<div class="wrap-p clearfix" style="min-height: 300px; margin-top: 10px; word-wrap: break-word;">
        <asp:Label ID="label_thongbao" runat="server" style="color: red; font-size: 15px;"></asp:Label>
        <asp:Panel ID="panel_form_dangnhap" runat="server" DefaultButton="btn_dangnhap">
            <h5 style="margin-bottom: 3px; color: #4285F4">ĐĂNG NHẬP</h5>
            <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <table style="width: 100%;" class="vmk_table_form">
                <tr>
                    <td class="title">TÀI KHOẢN HOẶC EMAIL</td>
                    <td style="white-space: nowrap">
                        <asp:TextBox ID="txt_taikhoan" runat="server" MaxLength="50" style="width: 80%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validator_taikhoan1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_taikhoan" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="title">MẬT KHẨU</td>
                    <td style="white-space: nowrap">
                        <asp:TextBox ID="txt_matkhau" runat="server" TextMode="Password" style="width: 80%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validator_matkhau1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_matkhau" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="title">MÃ XÁC NHẬN</td>
                    <td style="white-space: nowrap">
                        <img src="Captcha.aspx" style="margin-top: -2px" />&nbsp;<asp:TextBox ID="txt_captcha" runat="server" Width="200px" Height="19px" autocomplete='off'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="validator_captcha1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_captcha" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="title top"></td>
                    <td>
                        <asp:Button ID="btn_dangnhap" runat="server" Text="ĐĂNG NHẬP" class="button" onclick="btn_dangnhap_Click" />
                        <a href='javascript:alert("TÍNH NĂNG NÀY ĐANG ĐƯỢC CẬP NHẬT.\r\nXIN VUI LÒNG GỌI ĐẾN SĐT CỬA HÀNG\r\nĐỂ ĐƯỢC HỔ TRỢ CẤP MẬT KHẨU MỚI.\r\nXIN CẢM ƠN");' class="button">QUÊN MẬT KHẨU</a>
                        <asp:ValidationSummary ID="validation_total" runat="server" Visible="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</div>
</asp:Content>

<asp:Content ID="FootContent" runat="server" ContentPlaceHolderID="FootContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#main-menu').hover(function () {
                $('#menu-box', this).show();
            }, function () {
                $('#menu-box', this).hide();
            });
        });
    </script>
</asp:Content>