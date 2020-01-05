<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="TimKiem.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.TimKiem_TrangChu" %>

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

        <asp:Label ID="label_thongbao" runat="server" style="color: Red;"></asp:Label>

        <asp:Repeater ID="repeater_list_data" runat="server">
            <ItemTemplate>
                <a href='<%# Eval("link") %>'><h5 style="margin-bottom: 3px; color: #60A90A"><%# Eval("tieu_de") %></h5></a>
                <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px; width: 730px"></div>
                <%# Xu_Ly_Demo(Eval("noi_dung"),500) %>
                <br /><br />
                <span style="color: Blue">Ngày đăng: <%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_dang"), Eval("thang_dang"), Eval("nam_dang")) %></span>
                <br /><br />
            </ItemTemplate>
        </asp:Repeater>

        <br /><br />
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

            // HIGHLIGHT KEYWORD //

            var keyword = '<asp:Literal runat="server" id="keyword"></asp:Literal>';
            if (keyword != "") {
                $('div#body').find("div.wrap-p").highlight(keyword);
            }
        });
    </script>
    <script type="text/javascript" src="js/jquery.highlight-4.js"></script>
</asp:Content>