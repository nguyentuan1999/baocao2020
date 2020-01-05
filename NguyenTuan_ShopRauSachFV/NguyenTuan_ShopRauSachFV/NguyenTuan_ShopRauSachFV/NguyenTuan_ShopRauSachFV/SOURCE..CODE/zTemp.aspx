<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="zTemp.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.zTemp_TrangChu" %>

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
        <h5 style="margin-bottom: 3px;"><asp:Label ID="label_tieudebaiviet" runat="server" Text="TIÊU ĐỀ"></asp:Label></h5>
        <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
        <asp:Label ID="label_noidungbaiviet" runat="server" Text="NỘI DUNG"></asp:Label>
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