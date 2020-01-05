<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="KhachHang.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.KhachHang" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">QUẢN LÝ KHÁCH HÀNG</h3>
	</div>

    <asp:Label ID="label_thongbao" runat="server"></asp:Label>

    <asp:Panel ID="panel_xem" runat="server">
        <div class="panel-body" style="max-height: 500px; overflow-y: scroll">
            <asp:Repeater ID="repeater_list_data" runat="server" 
                onitemcommand="repeater_list_data_ItemCommand">
                <HeaderTemplate>
		            <table style="width: 100%;" class="vmk_table_list_data">
                        <tr>
                            <td class="title" style="text-align: center"></td>
                            <td class="title">TÀI KHOẢN</td>
                            <td class="title">EMAIL</td>
                            <td class="title">HỌ & TÊN</td>
                            <td class="title">NĂM SINH</td>
                            <td class="title">G.TÍNH</td>
                            <td class="title">SĐT</td>
                            <td class="title">KHÓA</td>
                            <td class="title" colspan="3">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_tv") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("account_name") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("email") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("ho_ten") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 90px">
                            <input type="text" value='<%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_sinh"),Eval("thang_sinh"),Eval("nam_sinh")) %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 60px">
                            <input type="text" value='<%# Xu_Ly_Gioi_Tinh(Eval("gioi_tinh")) %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Eval("sdt") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 70px">
                            <asp:CheckBox ID="checkbox_khoa" runat="server" Checked='<%# Eval("khoa") %>' onClick="return false;" />
                        </td>
                        <td class="data" style="width: 1px">
                            <a href='../ThongTinCaNhan.aspx?id=<%# Eval("id_tv") %>' target="_blank" class="btn btn-primary btn-xs">XEM</a>
                        </td>
                        <td class="data" style="width: 1px">
                            <asp:Button ID="btn_sua_item" runat="server" Text="SỬA" CommandName="sua" class="btn btn-info btn-xs" />
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
                    <td class="title">TÀI KHOẢN</td>
                    <td><asp:TextBox ID="txt_accountname" runat="server" MaxLength="50" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title">EMAIL</td>
                    <td><asp:TextBox ID="txt_email" runat="server" MaxLength="50" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title">MẬT KHẨU</td>
                    <td style="white-space: nowrap"><asp:TextBox ID="txt_matkhau" runat="server" TextMode="Password" style="width: 60%"></asp:TextBox><label id="label_thongbao_thaydoi_matkhau" runat="server" visible="false" style="color: red">&nbsp;&nbsp;* NẾU KHÔNG CẦN THAY ĐỔI MẬT KHẨU THÌ ĐỂ TRỐNG</label></td>
                </tr>
                <tr>
                    <td class="title">HỌ & TÊN</td>
                    <td><asp:TextBox ID="txt_hoten" runat="server" MaxLength="50" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        GIỚI TÍNH &nbsp;
                        <asp:DropDownList ID="txt_gioitinh" runat="server">
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
                    <td><asp:TextBox ID="txt_sdt" runat="server" vmk_control_id="txt_sdt" MaxLength="15" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title top">ĐỊA CHỈ</td>
                    <td><asp:TextBox ID="txt_diachi" runat="server" style="width: 100%" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        <div class="checkbox">
                            <label><asp:CheckBox ID="checkbox_khoa" runat="server" /> KHÓA TÀI KHOẢN NÀY</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="title top">LÝ DO KHÓA</td>
                    <td><asp:TextBox ID="txt_lydokhoa" runat="server" MaxLength="500" style="width: 100%" Rows="1" TextMode="MultiLine"></asp:TextBox></td>
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
    $(document).ready(function(){
        $("input[vmk_control_id='txt_sdt']").keypress(function (e) {
            var charCode = (e.which) ? e.which : e.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        });
    });
</script>

</asp:Content>
