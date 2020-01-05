<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="CauHinhPhanMem.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.CauHinhPhanMem" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

 <asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
		<h3 class="panel-title">CẤU HÌNH PHẦN MỀM</h3>
	</div>
	<div class="panel-body">

		<asp:Label ID="label_thongbao" runat="server"></asp:Label>

        <table style="width: 100%;" class="vmk_table_form">
            <tr>
                <td class="title">TÊN CỬA HÀNG</td>
                <td><asp:TextBox ID="txt_ten_cua_hang" runat="server" style="width: 100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title" valign="top">ĐỊA CHỈ</td>
                <td><asp:TextBox ID="txt_dia_chi" runat="server" style="width: 100%" Rows="6" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">EMAIL</td>
                <td><asp:TextBox ID="txt_email" runat="server" style="width: 100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">SỐ ĐIỆN THOẠI</td>
                <td><asp:TextBox ID="txt_sdt" runat="server" style="width: 100%"></asp:TextBox></td>
            </tr>
        </table>
	</div>
</div>

<div class="panel panel-default">
    <div class="panel-body">
        <asp:Button ID="btn_luu" runat="server" Text="LƯU THÔNG TIN" class="btn btn-primary btn-xs" OnClick="btn_luu_Click"/>
    </div>
</div>

</asp:Content>
