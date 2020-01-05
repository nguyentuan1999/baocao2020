<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="TinTuc.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.TinTuc_TrangChu" %>

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

        <asp:Panel ID="panel_mot_tintuc" runat="server">
            <asp:HiddenField ID="txt_id_tt" runat="server" />
            <h5 style="margin-bottom: 3px; color: #60A90A"><asp:Label ID="label_tieudebaiviet" runat="server" Text="Không có dữ liệu" style="color: #60A90A"></asp:Label></h5>
            <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <asp:Label ID="label_noidungbaiviet" runat="server">Không có dữ liệu</asp:Label>
            <br /><br />
            <span style="color: blue">Người đăng: <asp:Label ID="label_nguoidangtin" runat="server" Text="Không có dữ liệu"></asp:Label></span>
            <br />
            <span style="color: blue">Đăng ngày: <asp:Label ID="label_ngaythangnam" runat="server" Text="Không có dữ liệu"></asp:Label></span>
            <br />
            <span style="color: #D6492F">Số lần xem: <asp:Label ID="label_solanxem" runat="server" Text="Không có dữ liệu"></asp:Label></span>
            <br /><br />
        </asp:Panel>

        <asp:Panel ID="panel_nhieu_tintuc" runat="server" Visible="false">
            <h5 style="margin-bottom: 3px;"><asp:Label ID="label_tenloai" runat="server" Text="Không có dữ liệu" style="color: #4285F4"></asp:Label></h5>
            <br />
            <asp:Repeater ID="repeater_list_data" runat="server">
                <ItemTemplate>
                    <a href="TinTuc.aspx?id=<%# Eval("id_tt") %>"><h5 style="margin-bottom: 3px; color: #60A90A"><%# Eval("tieu_de") %></h5></a>
                    <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px; width: 730px"></div>
                    <%# Xu_Ly_Noi_Dung_Rut_Gon(Eval("noi_dung"), 500)%>
                    <br /><br />
                    <span style="color: #D6492F">Người đăng: <%# HTML_Encode(Eval("ho_ten")) %></span>
                    - <span style="color: blue">Ngày đăng: <%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_tt"), Eval("thang_tt"), Eval("nam_tt")) %></span>
                    - <span style="color: #D6492F">Số lần xem: <%# Eval("luot_xem") %></span>
                    <br /><br />
                </ItemTemplate>
            </asp:Repeater>
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