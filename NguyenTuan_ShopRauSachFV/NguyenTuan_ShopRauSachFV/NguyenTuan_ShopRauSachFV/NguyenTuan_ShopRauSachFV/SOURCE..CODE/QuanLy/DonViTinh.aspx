<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="DonViTinh.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DonViTinh" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">QUẢN LÝ ĐƠN VỊ TÍNH</h3>
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
                            <td class="title">TÊN ĐƠN VỊ TÍNH</td>
                            <td class="title" colspan="2">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_dvt") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("ten_dvt") %>' onclick="this.select()" readonly="readonly" />
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
                    <td style="width: 120px; white-space: nowrap">TÊN ĐƠN VỊ TÍNH</td>
                    <td><asp:TextBox ID="txt_tendvt" runat="server" MaxLength="50" Width="100%"></asp:TextBox></td>
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
