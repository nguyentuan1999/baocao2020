<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="DatHangThanhCong.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DatHangThanhCong_TrangChu" %>

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
        <div style="text-align: center">
            <h5 style="margin-bottom: 3px; color: #60A90A">ĐẶT HÀNG THÀNH CÔNG</h5>
            <h5 style="margin-bottom: 3px; color: #60A90A">CẢM ƠN BẠN ĐÃ ỦNG HỘ CỬA HÀNG</h5>
            <br />
            <div>CHÚNG TÔI SẼ XỬ LÝ ĐƠN HÀNG VÀ LIÊN HỆ BẠN ĐỂ GIAO HÀNG NHANH NHẤT CÓ THỂ</div>
            <br /><br />
            <a href="Default.aspx" class="button">TIẾP TỤC MUA HÀNG</a>
        </div>
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