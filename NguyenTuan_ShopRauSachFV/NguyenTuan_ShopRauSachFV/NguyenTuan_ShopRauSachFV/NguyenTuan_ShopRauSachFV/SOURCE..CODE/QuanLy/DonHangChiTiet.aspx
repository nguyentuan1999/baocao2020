<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="DonHangChiTiet.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.DonHangChiTiet" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<asp:Panel ID="panel_lydodong" runat="server" Visible="false">
    <div class="panel panel-primary" style="margin-bottom: 7px;">
        <div class="panel-heading">
            <h3 class="panel-title">ĐƠN HÀNG ĐÃ ĐÓNG VỚI LÝ DO SAU</h3>
        </div>
        <div class="panel-body">
            <asp:Label ID="label_lydodong" runat="server"></asp:Label>
        </div>
    </div>
</asp:Panel>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">THÔNG TIN KHÁCH HÀNG</h3>
	</div>
	<div class="panel-body">
        <table style="width: 100%;" class="vmk_table_list_data">
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">HỌ & TÊN</td>
                <td class="data" style="width: 90%">
                    <asp:TextBox ID="txt_hoten" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">EMAIL</td>
                <td class="data" style="width: 90%">
                    <asp:TextBox ID="txt_email" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">GIỚI TÍNH</td>
                <td class="data" style="width: 90%">
                    
                    <asp:TextBox ID="txt_gioitinh" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">NGÀY SINH</td>
                <td class="data" style="width: 90%">
                    <asp:TextBox ID="txt_ngaysinh" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">SỐ ĐIỆN THOẠI</td>
                <td class="data" style="width: 90%">
                    <asp:TextBox ID="txt_sdt" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="data" style="min-width: 150px; white-space: nowrap">ĐỊA CHỈ</td>
                <td class="data" style="width: 90%">
                    <asp:TextBox ID="txt_diachi" runat="server" ReadOnly="true" onclick="this.select()"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</div>

<div class="panel panel-default" style="margin-bottom: 8px;">
    <div class="panel-body">
        <a href="DonHang.aspx" class="btn btn-primary btn-xs">QUAY LẠI</a>
        <a href="javascript:window.print();" class="btn btn-warning btn-xs navbar-right">IN ĐƠN HÀNG</a>
        <span runat="server" id="label_tongtien" class="label label-danger navbar-right" style="margin-right: 5px; padding: 6px; font-size: 12px; font-weight: normal; background:#178ACC">vnđ</span>
        <span class="label label-success navbar-right" style="margin-right: 5px; padding: 7px; font-size: 11px; font-weight: normal">THÀNH TIỀN</span>
    </div>
</div>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title"><asp:Label ID="label_sodonhang" runat="server">CHI TIẾT ĐƠN HÀNG SỐ #</asp:Label></h3>
	</div>
	<div class="panel-body">

        <asp:Label ID="label_thongbao" runat="server"></asp:Label>

        <asp:Repeater ID="repeater_list_data" runat="server">
            <HeaderTemplate>
	            <table style="width: 100%;" class="vmk_table_list_data">
                    <tr>
                        <td class="title">STT</td>
                        <td class="title">TÊN SẢN PHẨM</td>
                        <td class="title">ĐƠN GIÁ</td>
                        <td class="title">ĐVT</td>
                        <td class="title">SỐ LƯỢNG</td>
                        <td class="title">THÀNH TIỀN</td>
                        <td class="title"></td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="data" style="width: 50px">
                        <input type="text" value='<%# Eval("stt") %>' onclick="this.select()" readonly="readonly" />
                        <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_dhct") %>' />
                    </td>
                    <td class="data">
                        <input type="text" value='<%# Eval("ten_sp") %>' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 100px">
                        <input type="text" value='<%# Xu_Ly_Money(Eval("don_gia")) %> đ' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 80px">
                        <input type="text" value='<%# Eval("ten_dvt") %>' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 100px">
                        <input type="text" value='<%# Eval("so_luong") %>' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 150px">
                        <input type="text" value='<%# Xu_Ly_Money(Eval("thanh_tien")) %> đ' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 1px">
                        <a href='../SanPham.aspx?idsp=<%# Eval("id_sp") %>' target="_blank" class="btn btn-warning btn-xs">XEM</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

	</div>
</div>

</asp:Content>
