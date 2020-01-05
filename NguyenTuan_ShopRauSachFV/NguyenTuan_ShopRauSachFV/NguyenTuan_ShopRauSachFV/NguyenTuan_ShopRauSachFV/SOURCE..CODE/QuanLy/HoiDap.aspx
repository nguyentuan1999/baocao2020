<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="HoiDap.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.HoiDap" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<!--<div class="panel panel-default" style="border: 0px; box-shadow: none; margin-top: -8px; margin-bottom: 6px;">
    <div class="panel-body" style="padding: 0px;">
        <ul class="pagination pagination-sm" style="margin: 0px; float: right">
            <li><a href="#">◄</a></li>
            <li><a href="#">►</a></li>
        </ul>
    </div>
</div>
-->

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">QUẢN LÝ HỎI ĐÁP</h3>
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
                            <td class="title">TIÊU ĐỀ</td>
                            <td class="title">NỘI DUNG</td>
                            <td class="title">NGÀY ĐĂNG</td>
                            <td class="title">THÀNH VIÊN</td>
                            <td class="title">TRẢ LỜI</td>
                            <td class="title">CHIA SẼ</td>
                            <td class="title">ĐÓNG</td>
                            <td class="title" colspan="3">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_hd") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("tieu_de") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("noi_dung") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_hd"),Eval("thang_hd"),Eval("nam_hd")) %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Eval("ho_ten") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 80px">
                            <input type="text" value='<%# Eval("so_cau_tra_loi") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 70px">
                            <input type="checkbox" vmk_id="checkbox_chiase" vmk_value='<%# Eval("id_hd") %>' <%# Xu_Ly_Checked(Eval("chia_se")) %> />
                            <span vmk_id='checkbox_chiase_<%# Eval("id_hd") %>' style="display: none"><asp:CheckBox ID="checkbox_chiase" runat="server" Checked='<%# Eval("chia_se") %>' /></span>
                        </td>
                        <td class="data" style="width: 70px">
                            <input type="checkbox" vmk_id="checkbox_khoa" vmk_value='<%# Eval("id_hd") %>' <%# Xu_Ly_Checked(Eval("close_hd")) %> />
                            <span vmk_id='checkbox_khoa_<%# Eval("id_hd") %>' style="display: none"><asp:CheckBox ID="checkbox_khoa" runat="server" Checked='<%# Eval("close_hd") %>' /></span>
                        </td>
                        <td class="data" style="width: 1px">
                            <span vmk_id='btn_luu_item_<%# Eval("id_hd") %>' style="display: none"><asp:Button ID="btn_luu_item" runat="server" Text="Lưu" CommandName="luu" class="btn btn-primary btn-xs" /></span>
                        </td>
                        <td class="data" style="width: 1px">
                            <a href='HoiDapTraLoi.aspx?cauhoi=<%# Eval("id_hd") %>' class="btn btn-success btn-xs">XEM</a>
                        </td>
                        <td class="data" style="width: 1px">
                            <asp:Button ID="btn_xoa_item" runat="server" Text="XÓA" CommandName="xoa" class="btn btn-warning btn-xs" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>

    <asp:Panel ID="panel_them_sua" runat="server" Visible="false">
        <div class="panel-body">
            <asp:HiddenField ID="trang_thai" runat="server" Value="" />
            <asp:HiddenField ID="id_item_for_edit" runat="server" Value="" />
		    <table style="width: 100%;" class="vmk_table_form">
                <tr>
                    <td class="title">TIÊU ĐỀ</td>
                    <td><asp:TextBox ID="txt_tieude" runat="server" MaxLength="50" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title top">NỘI DUNG</td>
                    <td><asp:TextBox ID="txt_noidung" runat="server" style="width: 100%" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>NGÀY &nbsp;<asp:DropDownList ID="txt_ngayhd" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                        THÁNG &nbsp;<asp:DropDownList ID="txt_thanghd" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                        NĂM &nbsp;<asp:DropDownList ID="txt_namhd" runat="server" style="width: 100px"></asp:DropDownList>&nbsp;&nbsp;ĐĂNG CÂU HỎI
                    </td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        <div class="checkbox">
                            <label><asp:CheckBox ID="checkbox_chiase" runat="server" Checked="true" /> CHIA SẼ</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        <div class="checkbox">
                            <label><asp:CheckBox ID="checkbox_khoa" runat="server" /> ĐÓNG CÂU HỎI NÀY</label>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</div>

<div class="panel panel-default">
    <div class="panel-body">
        <asp:Button ID="btn_them" runat="server" Text="THÊM MỚI" class="btn btn-primary btn-xs" onclick="btn_them_Click"/>
            <asp:Button ID="btn_luu" runat="server" Text="LƯU" Visible="false" class="btn btn-primary btn-xs" onclick="btn_luu_Click"/>
            <asp:HyperLink ID="btn_khongluu" runat="server"  Visible="false" class="btn btn-primary btn-xs" NavigateUrl="">KHÔNG LƯU</asp:HyperLink>
        <asp:Button ID="btn_xoa" runat="server" Text="XÓA MỤC ĐÃ CHỌN" class="btn btn-primary btn-xs" onclick="btn_xoa_Click"/>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("input[vmk_id='checkbox_chiase']").click(function () {
            var id = $(this).attr("vmk_value");
            $("span[vmk_id='checkbox_chiase_" + id + "']").find("input[type='checkbox']").prop("checked", $(this).is(":checked"));
            $("span[vmk_id='btn_luu_item_" + id + "']").find("input[type='submit']").click();
        });
        $("input[vmk_id='checkbox_khoa']").click(function () {
            var id = $(this).attr("vmk_value");
            $("span[vmk_id='checkbox_khoa_" + id + "']").find("input[type='checkbox']").prop("checked", $(this).is(":checked"));
            $("span[vmk_id='btn_luu_item_" + id + "']").find("input[type='submit']").click();
        });
    });
</script>

</asp:Content>
