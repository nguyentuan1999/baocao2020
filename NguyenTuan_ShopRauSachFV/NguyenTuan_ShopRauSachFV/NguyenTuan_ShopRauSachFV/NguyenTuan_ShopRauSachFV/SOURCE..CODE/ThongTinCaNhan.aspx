<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="ThongTinCaNhan.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.ThongTinCaNhan_TrangChu" %>

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
        <asp:HiddenField ID="id_tv_for_edit" runat="server" />
        <h5 style="margin-bottom: 3px; color: #60A90A">THÔNG TIN CÁ NHÂN</h5>
        <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 10px;"></div>
        <asp:Label ID="label_thongbao" runat="server" style="color: Red;"></asp:Label>
        <table style="width: 100%;" class="vmk_table_form">
            <tr>
                <td class="title">TÀI KHOẢN</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_accountname" runat="server" MaxLength="50" style="width: 70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_taikhoan1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_accountname" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="validator_taikhoan2" runat="server"
                        ErrorMessage="* CHỈ CHẤP NHẬN SỐ VÀ CHỮ CÁI" ValidationExpression="^[a-zA-Z0-9]*$" ControlToValidate="txt_accountname" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="title">EMAIL</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_email" runat="server" MaxLength="50" style="width: 70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_email1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_email" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="validator_email2" runat="server"
                        ErrorMessage="* EMAIL KHÔNG ĐÚNG" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ControlToValidate="txt_email" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="title">MẬT KHẨU</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_matkhau" runat="server" TextMode="Password" style="width: 40%"></asp:TextBox>
                    <span style="color: red">* NẾU KHÔNG CẦN THAY ĐỔI MẬT KHẨU THÌ ĐỂ TRỐNG</span>
                </td>
            </tr>
            <tr>
                <td class="title">HỌ & TÊN</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_hoten" runat="server" MaxLength="50" style="width: 70%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_hoten1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_hoten" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="title"></td>
                <td>GIỚI TÍNH &nbsp;<asp:DropDownList ID="txt_gioitinh" runat="server">
                        <asp:ListItem Selected="true" Value="0">Nam</asp:ListItem>
                        <asp:ListItem Value="1">Nữ</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;
                    NGÀY &nbsp;<asp:DropDownList ID="txt_ngaysinh" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                    THÁNG &nbsp;<asp:DropDownList ID="txt_thangsinh" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                    NĂM &nbsp;<asp:DropDownList ID="txt_namsinh" runat="server" style="width: 100px"></asp:DropDownList>&nbsp;&nbsp;SINH
                </td>
            </tr>
            <tr>
                <td class="title">SỐ ĐIỆN THOẠI</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_sdt" runat="server" style="width: 70%" MaxLength="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_sdt1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_sdt" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="validator_sdt2" runat="server"
                        ErrorMessage="* CHỈ CHẤP NHẬN SỐ" ValidationExpression="^[0-9]+$" ControlToValidate="txt_sdt" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="title top">ĐỊA CHỈ</td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txt_diachi" runat="server" style="width: 70%" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="validator_diachi1" runat="server" ErrorMessage="* KHÔNG ĐƯỢC RỖNG" ControlToValidate="txt_diachi" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btn_luu" runat="server" Text="LƯU THÔNG TIN" class="button" OnClick="btn_luu_Click" />
                    <a href="Default.aspx" class="button">KHÔNG LƯU</a>
                    <asp:ValidationSummary ID="validation_total" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
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

            $("input[vmk_control_id='txt_sdt']").keypress(function (e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>