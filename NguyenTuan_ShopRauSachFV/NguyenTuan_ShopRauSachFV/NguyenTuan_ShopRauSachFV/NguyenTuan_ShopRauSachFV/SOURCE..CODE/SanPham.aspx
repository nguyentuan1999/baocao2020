<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="SanPham.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.SanPham_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        div#main-menu > ul#menu-box{
            display: none;
        }
        div#row{
            margin-top: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="body">
	<div class="wrap-p clearfix" style="min-height: 300px; margin-top: 10px; word-wrap: break-word;">

        <asp:Panel ID="panel_mot_sanpham" runat="server">
            <h5 style="margin-bottom: 3px; color: #60A90A"><asp:Label ID="label_tensp" runat="server" Text="Không có dữ liệu" style="color: #60A90A"></asp:Label></h5>
            <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <span style="color: red; font-size: 14px">Đơn giá: <asp:Label ID="label_dongia" runat="server" Text="Không có dữ liệu"></asp:Label></span>
            <br />
            <span style="color: blue; font-size: 14px">Đơn vị tính: <asp:Label ID="label_dvt" runat="server" Text="Không có dữ liệu"></asp:Label></span>
            <br />
            <asp:Panel ID="panel_btn_themvaogiohang" runat="server">
                <br />
                <asp:HyperLink ID="hplink_themvaogiohang" runat="server" NavigateUrl="GioHang.aspx?ThemSP=" class="button">THÊM VÀO GIỎ HÀNG</asp:HyperLink>
                <br />
            </asp:Panel>
            <br />
            <asp:Label ID="label_gioithieu" runat="server">Không có dữ liệu</asp:Label>
            <br /><br />
            <span style="color: blue">Danh mục: <asp:HyperLink ID="hplink_danh_muc" runat="server" NavigateUrl="SanPham.aspx?iddm=" style="color: red"><asp:Label ID="label_tendanhmuc" runat="server" Text="Không có dữ liệu"></asp:Label></asp:HyperLink></span>
            <br />
            <span style="color: blue">Đăng ngày: <asp:Label ID="label_ngaythangnam" runat="server" Text="Không có dữ liệu" style="color: green"></asp:Label></span>
            <br />
            <span style="color: #D6492F">Số lần xem: <asp:Label ID="label_solanxem" runat="server" Text="Không có dữ liệu" style="color: green"></asp:Label></span>
            <br /><br />
        </asp:Panel>

        <asp:Panel ID="panel_nhieu_sanpham" runat="server" Visible="false">

            <style type="text/css">
                body, div#body{
	                background: #92CB43;
                }
            </style>

            <h5 style="margin-bottom: 3px; color: white"><asp:Label ID="label_tendm" runat="server" Text="Danh mục này chưa có Sản phẩm nào"></asp:Label></h5>
            <div style="background: #FFFFFF; border-top:2px solid yellow; margin-bottom: 5px;"></div>
            <div id="row">
            <asp:Repeater ID="repeater_list_data" runat="server">
                <ItemTemplate>
			        <div class="post fst-c">
				        <div class="photo-holder">
					        <a href="SanPham.aspx?idsp=<%# Eval("id_sp") %>"><img id="vmk_img_demo_for_product" width="320px" src='<%# Xu_Ly_Tach_Img_From_Noi_Dung(Eval("gioi_thieu")) %>' vmk_default_img='images/product-no-img.png'/></a>
				        </div>
				        <div class="info">
					        <a href="SanPham.aspx?idsp=<%# Eval("id_sp") %>">
						        <h4><%# HTML_Encode(Eval("ten_sp")) %></h4>
						        <table style="width: 98%; margin: 0px;">
							        <tr>
								        <td width="200px">
									        <span class="percent">Giá sản phẩm / <%# HTML_Encode(Eval("ten_dvt")) %></span>
								        </td>
								        <td width="1px" style="text-align: center;"><i class="ico-bought"></i></td>
								        <td></td>
							        </tr>
							        <tr>
								        <td>
									        <span class="price"><%# Xu_Ly_Money(Eval("don_gia")) %><sup>đ</sup></span>
								        </td>
								        <td style="text-align: center;"><%# Eval("luot_xem") %></td>
								        <td><a href="GioHang.aspx?ThemSP=<%# Eval("id_sp") %>"><span class="more-btn"><img src="images/icon_cart.gif" /></span></a></td>
							        </tr>
						        </table>
					        </a>
				        </div>
			        </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
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

            // XỬ LÝ KHI KHÔNG CÓ ẢNH DEMO SẢN PHẨM //

            $('img#vmk_img_demo_for_product').each(function (index) {
                var url = $(this).attr("src");
                var url_default = $(this).attr("vmk_default_img");
                if (url.trim() == "") {
                    $(this).attr("src", url_default);
                }
            });
        });
    </script>
</asp:Content>