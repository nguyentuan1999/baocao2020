<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="Default.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.Default_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        body, div#body{
	        background: #92CB43;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="body">
	<div class="wrap-p clearfix">

		<div id="slider">
			<div class="photo-holder">
				<div id="number_slideshow" class="number_slideshow">
					<ul>
                        <asp:Repeater ID="repeater_list_data_slider1" runat="server">
                            <ItemTemplate>
                                <li style="width: 100%; text-align: center">
                                    <div class="title"><%# HTML_Encode(Eval("ten_sp")) %></div>
                                    <a href='SanPham.aspx?idsp=<%# Eval("id_sp") %>'>
                                        <img style="max-height:250px;" id="vmk_img_demo_for_product" src='<%# Xu_Ly_Tach_Img_From_Noi_Dung(Eval("gioi_thieu")) %>' vmk_default_img='images/product-no-img.png'/>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
					</ul>
					<ul class="number_slideshow_nav">
                        <asp:Repeater ID="repeater_list_data_slider2" runat="server">
                            <ItemTemplate>
                                <li><a href="#"><%# Eval("stt") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
					</ul>
					<div style="clear: both"></div>
				</div>
			</div>
			<div class="shadow"></div>                
		</div>

        <asp:Label ID="label_thongbao" runat="server" ForeColor="Red" BackColor="white"></asp:Label>

        <asp:Repeater ID="repeater_list_data" runat="server">
            <HeaderTemplate>
                <div id="row">
            </HeaderTemplate>
            <ItemTemplate>
			        <div class="post fst-c">
				        <div class="photo-holder">
					        <a href="SanPham.aspx?idsp=<%# Eval("id_sp") %>"><img id="vmk_img_demo_for_product" width="320px" height:330px src='<%# Xu_Ly_Tach_Img_From_Noi_Dung(Eval("gioi_thieu")) %>' vmk_default_img='images/product-no-img.png'/></a>
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
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

	</div>
</div>
</asp:Content>

<asp:Content ID="FootContent" runat="server" ContentPlaceHolderID="FootContent">
    <script type="text/javascript">
        $(document).ready(function () {
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#number_slideshow").number_slideshow({
                slideshow_autoplay: 'enable',
                slideshow_time_interval: '3000',
                slideshow_window_background_color: "white",
                slideshow_window_padding: '0',
                slideshow_window_width: 'auto',
                slideshow_window_height: '250',
                slideshow_border_size: '0',
                slideshow_border_color: '0',
                slideshow_show_button: 'enable',
                slideshow_show_title: 'enable',
                slideshow_button_text_color: '#CCC',
                slideshow_button_background_color: '#333',
                slideshow_button_current_background_color: '#666',
                slideshow_button_border_color: '#000',
                slideshow_button_border_size: '1'
            });
        });
    </script>
	<script type="text/javascript" charset="utf-8">
		$(function () {
		    $("img.lazy").lazyload({
		        effect: "fadeIn"
		    });
		});
	</script>
</asp:Content>