<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="HoiDapTraLoi.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.HoiDapTraLoi" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<asp:Label ID="label_thongbao" runat="server"></asp:Label>

<asp:HiddenField ID="id_cauhoi" runat="server" Value="" />

<asp:HiddenField ID="closed_cauhoi" runat="server" Value="" />

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title"><asp:Label ID="label_tieude" runat="server" Text=""></asp:Label></h3>
	</div>
	<div class="panel-body">
        <asp:Label ID="label_noidung" runat="server" Text="" style="word-wrap: break-word;"></asp:Label>
        <br /><br />
        <table style="width: 100%">
            <tr>
                <td style="width: 1px">
                    <asp:Button ID="label_closed" class="btn btn-danger btn-xs disabled" runat="server" Visible="false" Text="ĐÃ ĐÓNG" />
                </td>
                <td>
                    <div class="btn-group" style="float:right">
                        <a href="#" class="btn btn-info btn-xs dropdown-toggle" data-toggle="dropdown">THÔNG TIN &nbsp;<span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Trả lời: <asp:Label ID="label_so_cau_tra_loi" runat="server" Text=""></asp:Label></a></li>
                            <li><a href="#">Đăng bởi: <asp:Label ID="label_hoten" runat="server" Text=""></asp:Label></a></li>
                            <li><a href="#">Ngày đăng: <asp:Label ID="label_ngaythangnam" runat="server" Text=""></asp:Label></a></li>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>

<asp:Repeater ID="repeater_list_traloi_data" runat="server" 
    onitemcommand="repeater_list_traloi_data_ItemCommand" 
    onitemdatabound="repeater_list_traloi_data_ItemDataBound">
    <ItemTemplate>
        <div class="panel panel-default" style="margin-bottom: 7px;">
            <div class="panel-body">
                <asp:HiddenField ID="id_cautraloi" runat="server" Value='<%# Eval("id_traloi") %>' />
                <p style="word-wrap: break-word;"><%# HTML_Encode(Eval("noi_dung"), true) %></p>
                <div class="btn-group" style="float:right">
                    <div class="btn-group">
                        <a href="#" class="btn btn-info btn-xs dropdown-toggle" data-toggle="dropdown">THÔNG TIN &nbsp;<span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <li><a href="#">Đăng bởi: <%# HTML_Encode(Eval("ho_ten")) %></a></li>
                            <li><a href="#">Ngày đăng: <%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_traloi"), Eval("thang_traloi"), Eval("nam_traloi")) %></a></li>
                        </ul>
                    </div>
                    <asp:Button ID="btn_xoa_traloi" CommandName="xoa_traloi" runat="server" Text="XÓA" class="btn btn-warning btn-xs" style="margin-left: 4px" />
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

<asp:Panel ID="panel_traloi" runat="server">
    <div class="panel panel-primary">
	    <div class="panel-heading">
            <h3 class="panel-title">TRẢ LỜI CÂU HỎI</h3>
	    </div>
	    <div class="panel-body">
            <table style="width: 100%">
                <tr>
                    <td><asp:TextBox ID="txt_noidung_traloi" runat="server" style="width: 100%" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="padding-top: 5px"><asp:Button ID="btn_traloi" runat="server" Text="TRẢ LỜI" class="btn btn-warning btn-xs" onclick="btn_traloi_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Panel>

</asp:Content>
