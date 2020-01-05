<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="HoiDap.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.HoiDap_TrangChu" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        div#main-menu > ul#menu-box
        {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div id="body">    
	<div class="wrap-p clearfix" style="min-height: 300px; margin-top: 10px; word-wrap: break-word;">

        <asp:Panel ID="panel_xem_cauhoi" runat="server" Visible="false">

            <h5 style="margin-bottom: 3px; color: #60A90A"><asp:Label ID="label_tieude_cauhoi" runat="server" Text="Không có dữ liệu" style="color: #60A90A"></asp:Label></h5>
            <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <asp:Label ID="label_noidung_cauhoi" runat="server">Không có dữ liệu</asp:Label>

            <br /><br />

            <span style="color: #D6492F">Người hỏi: <asp:Label ID="label_hoten" runat="server">Không có dữ liệu</asp:Label></span>
            - <span style="color: blue">Ngày hỏi: <asp:Label ID="label_ngayhoi" runat="server">Không có dữ liệu</asp:Label></span>
            - <span style="color: #D6492F">Số câu trả lời: <asp:Label ID="label_socautraloi" runat="server">Không có dữ liệu</asp:Label></span>
            
            <br /><br />

            <asp:Repeater ID="repeater_list_data_traloi" runat="server">
                <HeaderTemplate>
                    <h5 style="margin-bottom: 3px; color: #60A90A">Trả lời</h5>
                    <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# HTML_Encode(Eval("noi_dung"),true) %>
                    <br /><br />
                    <span style="color: blue">Người trả lời: <%# HTML_Encode(Eval("ho_ten")) %></span>
                    <br />
                    <span style="color: #D6492F">Ngày trả lời: <%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_traloi"), Eval("thang_traloi"), Eval("nam_traloi")) %></span>
                    <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px; margin-top: 5px;"></div>
                </ItemTemplate>
            </asp:Repeater>

            <br /><br />

            <asp:Panel ID="panel_guitraloi" runat="server" Visible="false">
                <asp:HiddenField ID="id_cauhoi_for_traloi" runat="server" />
                <asp:Label ID="label_thongbao_guitraloi" runat="server" style="color: red; font-size: 15px;"></asp:Label>
                <h5 style="margin-bottom: 3px;"><a href='HoiDap.aspx' style='color: #4285F4'>TRẢ LỜI</a></h5>
                <table style="width: 100%" class="vmk_table_form">
                    <tr>
                        <td>NỘI DUNG TRẢ LỜI</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txt_noidung_traloi" runat="server" TextMode="MultiLine" Rows="6" style="width: 98%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_traloi" runat="server" Text="GỬI TRẢ LỜI" class="button" style="width: 150px" OnClick="btn_guitraloi_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="panel_thongbao_cauhoi_dadong" runat="server" Visible="false">
                <h5 style="margin-bottom: 3px; color: #60A90A">CÂU HỎI ĐÃ ĐÓNG. BẠN KHÔNG THỂ TRẢ LỜI</h5>
                <br /><br />
            </asp:Panel>

        </asp:Panel>

        <asp:Panel ID="panel_ds_cauhoi" runat="server">
            <h5 style="margin-bottom: 3px;"><a href='HoiDap.aspx' style='color: #4285F4'>HỎI ĐÁP</a></h5><br />

            <asp:Panel ID="panel_show_button_show_panel_guicauhoi" runat="server" Visible="false">
                <asp:Button ID="btn_show_panel_guicauhoi" runat="server" class="button" style="width: 150px" Text="GỬI CÂU HỎI" OnClick="btn_show_panel_guicauhoi_Click" />
                <br /><br />
            </asp:Panel>

            <asp:Repeater ID="repeater_list_data_cauhoi" runat="server">
                <ItemTemplate>
                    <a href="HoiDap.aspx?id=<%# Eval("id_hd") %>"><h5 style="margin-bottom: 3px; color: #60A90A"><%# Eval("tieu_de") %></h5></a>
                    <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px; width: 730px"></div>
                    <%# Xu_Ly_Noi_Dung_Rut_Gon(Eval("noi_dung"), 500)%>
                    <br /><br />
                    <span style="color: #D6492F">Người hỏi: <%# HTML_Encode(Eval("ho_ten")) %></span>
                    - <span style="color: blue">Ngày hỏi: <%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_hd"), Eval("thang_hd"), Eval("nam_hd")) %></span>
                    - <span style="color: #D6492F">Số câu trả lời: <%# Eval("so_cau_tra_loi")%></span>
                    <br /><br />
                </ItemTemplate>
            </asp:Repeater>

            <br /><br />

        </asp:Panel>

        <asp:Panel ID="panel_guicauhoi" runat="server" Visible="false">
            <asp:Label ID="label_thongbao_guicauhoi" runat="server" style="color: red; font-size: 15px;"></asp:Label>
            <h5 style="margin-bottom: 3px; color: #60A90A"><asp:Label ID="label_tieudebaiviet" runat="server">GỬI CÂU HỎI</asp:Label></h5>
            <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
            <table style="width: 100%" class="vmk_table_form">
                <tr>
                    <td><asp:TextBox ID="txt_tieude" runat="server" MaxLength="50" style="width: 98%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>NỘI DUNG CÂU HỎI</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txt_noidung_cauhoi" runat="server" TextMode="MultiLine" Rows="6" style="width: 98%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><label style="cursor:pointer;"><asp:CheckBox ID="checkbox_chiase" runat="server"/> CHIA SẼ CÂU HỎI NÀY</label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_guicauhoi" runat="server" Text="GỬI CÂU HỎI" class="button" style="width: 150px" OnClick="btn_guicauhoi_Click" />
                        <a href="HoiDap.aspx" class="button">QUAY LẠI</a>
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