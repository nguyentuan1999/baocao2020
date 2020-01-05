<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="DanhMuc.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DanhMuc" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<asp:SqlDataSource ID="sql_datasource_list_danh_muc_cha" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    SelectCommand="select null as id_dm, '' as ten_dm, '0' as stt UNION SELECT id_dm, ten_dm, stt FROM danh_muc where (id_dm_cha is null) order by id_dm asc, stt asc"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">QUẢN LÝ DANH MỤC</h3>
	</div>

    <asp:Label ID="label_thongbao" runat="server"></asp:Label>

    <asp:Panel ID="panel_xem" runat="server">
        <div class="panel-body" style="max-height: 600px; overflow-y: scroll">
            <asp:Repeater ID="repeater_list_data" runat="server" 
                onitemcommand="repeater_list_data_ItemCommand">
                <HeaderTemplate>
		            <table style="width: 100%;" class="vmk_table_list_data">
                        <tr>
                            <td class="title" style="text-align: center"></td>
                            <td class="title">TÊN DANH MỤC</td>
                            <td class="title">DANH MỤC CHA</td>
                            <td class="title">MENU</td>
                            <td class="title">STT</td>
                            <td class="title" colspan="3">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_dm") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("ten_dm") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 30%">
                            <input type="text" value='<%# Eval("ten_dm_cha") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 50px">
                            <input type="checkbox" id="vmk_status_show_in_menu" value='<%# Eval("show_in_menu") %>' onclick="return false;" />
                        </td>
                        <td class="data" style="width: 50px">
                            <input type="text" value='<%# Eval("stt") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 1px">
                            <a href='../SanPham.aspx?iddm=<%# Eval("id_dm") %>' target="_blank" class="btn btn-primary btn-xs">XEM</a>
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
        <script type="text/javascript">
            $(document).ready(function () {
                $("input#vmk_status_show_in_menu").each(function (index) {
                    var status = $(this).attr("value").toLowerCase().trim();
                    if (status == "true") {
                        $(this).prop("checked",true);
                    }
                });
            });
        </script>
    </asp:Panel>

    <asp:Panel ID="panel_them_sua" runat="server" Visible="false">
        <div class="panel-body">
            <asp:HiddenField ID="trang_thai" runat="server" Value="" />
            <asp:HiddenField ID="id_item_for_edit" runat="server" Value="" />
		    <table style="width: 100%;" class="vmk_table_form">
                <tr>
                    <td style="width: 200px; white-space: nowrap">TÊN DANH MỤC</td>
                    <td><asp:TextBox ID="txt_tendm" runat="server" MaxLength="50" Width="98%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 200px; white-space: nowrap">DANH MỤC CHA</td>
                    <td><asp:DropDownList ID="dropdownlist_list_danh_muc_cha" runat="server" Width="98%" Height="24px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 200px; white-space: nowrap">XUẤT HIỆN TRÊN MENU</td>
                    <td><asp:CheckBox ID="chk_show_in_menu" runat="server" Checked /></td>
                </tr>
                <tr>
                    <td style="width: 200px; white-space: nowrap">SỐ THỨ TỰ TRÊN MENU</td>
                    <td><asp:TextBox ID="txt_stt_in_menu" runat="server" MaxLength="5" Width="100px"></asp:TextBox></td>
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

</asp:Content>
