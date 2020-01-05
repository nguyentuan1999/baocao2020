<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="DonHang.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DonHang" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title" runat="server">QUẢN LÝ ĐƠN HÀNG</h3>
	</div>

    <asp:Label ID="label_thongbao" runat="server"></asp:Label>

    <asp:Panel ID="panel_xem" runat="server">
        <div class="panel-body" style="max-height: 400px; overflow-y: scroll">
            <asp:Repeater ID="repeater_list_data" runat="server" 
                onitemcommand="repeater_list_data_ItemCommand">
                <HeaderTemplate>
		            <table style="width: 100%;" class="vmk_table_list_data">
                        <tr>
                            <td class="title" style="text-align: center"></td>
                            <td class="title">ĐƠN HÀNG SỐ</td>
                            <td class="title">KHÁCH HÀNG</td>
                            <td class="title">NGÀY ĐẶT HÀNG</td>
                            <td class="title">SỐ LƯỢNG HH</td>
                            <td class="title">THÀNH TIỀN</td>
                            <td class="title">ĐÃ ĐÓNG</td>
                            <td class="title" colspan="3">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_dh") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data" style="width: 120px">
                            <input type="text" value='<%# Eval("id_dh") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("ho_ten") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 140px">
                            <input type="text" value='<%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_dh"),Eval("thang_dh"),Eval("nam_dh")) %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 120px">
                            <input type="text" value='<%# Eval("so_luong_hang_hoa") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Xu_Ly_Money(Eval("thanh_tien")) %> đ' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 80px">
                            <asp:CheckBox ID="checkbox_dichvu" runat="server" Checked='<%# Eval("khoa") %>' onClick="return false;" />
                        </td>
                        <td class="data" style="width: 1px">
                            <a href='DonHangChiTiet.aspx?id=<%# Eval("id_dh") %>' class="btn btn-primary btn-xs">XEM</a>
                        </td>
                        <td class="data" style="width: 1px">
                            <span style='display: <%# Xu_Ly_Css_Display(Eval("khoa"),true) %>'>
                                <asp:Button ID="btn_mokhoa_item" runat="server" Text="MỞ" CommandName="mo_khoa" class="btn btn-primary btn-xs" style="width: 50px" />
                            </span>
                            <span style='display: <%# Xu_Ly_Css_Display(Eval("khoa"),false) %>'>
                                <asp:Button ID="btn_khoa_item" runat="server" Text="ĐÓNG" CommandName="khoa" class="btn btn-info btn-xs" style="width: 50px" />
                            </span>
                        </td>
                        <td class="data" style="width: 1px">
                            <!--<asp:Button ID="btn_xoa_item" runat="server" Text="XÓA" CommandName="xoa" class="btn btn-warning btn-xs" />-->
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>

    <asp:Panel ID="panel_sua" runat="server" Visible="false">
        <div class="panel-body">
            <asp:HiddenField ID="id_item_for_edit" runat="server" Value="" />
		    <table style="width: 100%;" class="vmk_table_form">
                <tr>
                    <td class="title top">LÝ DO ĐÓNG</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txt_lydokhoa" runat="server" MaxLength="500" style="width: 100%" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</div>

<div class="panel panel-default">
    <div class="panel-body">
        <!--<asp:Button ID="btn_xoa" runat="server" Text="XÓA MỤC ĐÃ CHỌN" class="btn btn-primary btn-xs" onclick="btn_xoa_Click"/>-->
        <asp:Button ID="btn_luu_lydokhoa" Visible="false" runat="server" Text="LƯU LÝ DO" class="btn btn-primary btn-xs" onclick="btn_luu_lydokhoa_Click"/>
        <asp:HyperLink ID="btn_khongluu" runat="server"  Visible="false" class="btn btn-primary btn-xs" NavigateUrl="">KHÔNG LƯU</asp:HyperLink>
    </div>
</div>

</asp:Content>
