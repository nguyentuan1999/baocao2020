<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="TinTuc.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.TinTuc" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent">
    <link rel="stylesheet" href="sceditor/development/themes/default.css" type="text/css" media="all" />
    <script type="text/javascript" src="sceditor/development/jquery.sceditor.bbcode.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<asp:SqlDataSource ID="sql_datasource" runat="server" 
    ConnectionString="<%$ ConnectionStrings:VMK_CONNECTION_STRING %>" 
    ProviderName="<%$ ConnectionStrings:VMK_CONNECTION_STRING.ProviderName %>"></asp:SqlDataSource>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
        <h3 class="panel-title">QUẢN LÝ TIN TỨC & KỸ THUẬT</h3>
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
                            <td class="title">THÀNH VIÊN</td>
                            <td class="title">NGÀY ĐĂNG</td>
                            <td class="title">LƯỢT XEM</td>
                            <td class="title">TIN KỸ THUẬT</td>
                            <td class="title">NHÁP</td>
                            <td class="title" colspan="3">CHỨC NĂNG</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="data" style="text-align: center; width: 50px;">
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_tt") %>' />
                            <asp:CheckBox ID="checkbox_item_select" runat="server"/>
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("tieu_de") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data">
                            <input type="text" value='<%# Eval("noi_dung") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Eval("ho_ten") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 100px">
                            <input type="text" value='<%# Xu_Ly_Ngay_Thang_Nam(Eval("ngay_tt"),Eval("thang_tt"),Eval("nam_tt")) %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 90px">
                            <input type="text" value='<%# Eval("luot_xem") %>' onclick="this.select()" readonly="readonly" />
                        </td>
                        <td class="data" style="width: 110px">
                            <asp:CheckBox ID="checkbox_tinkythuat" runat="server" Checked='<%# Eval("ky_thuat") %>' onClick="return false;" />
                        </td>
                        <td class="data" style="width: 70px">
                            <asp:CheckBox ID="checkbox_luunhap" runat="server" Checked='<%# Eval("luu_nhap") %>' onClick="return false;" />
                        </td>
                        <td class="data" style="width: 1px">
                            <a href='../TinTuc.aspx?id=<%# Eval("id_tt") %>' target="_blank" class="btn btn-primary btn-xs">XEM</a>
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
            <asp:HiddenField ID="id_tv_for_edit" runat="server" Value="" />
		    <table style="width: 100%;" class="vmk_table_form">
                <tr>
                    <td class="title">TIÊU ĐỀ</td>
                    <td><asp:TextBox ID="txt_tieude" runat="server" MaxLength="50" style="width: 100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="title top">NỘI DUNG</td>
                    <td>
                        <asp:TextBox ID="txt_noidung" class="vmk_jquery_sceditor" runat="server" style="width: 100%" Rows="20" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>NGÀY &nbsp;<asp:DropDownList ID="txt_ngaytt" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                        THÁNG &nbsp;<asp:DropDownList ID="txt_thangtt" runat="server" style="width: 50px"></asp:DropDownList>&nbsp;
                        NĂM &nbsp;<asp:DropDownList ID="txt_namtt" runat="server" style="width: 100px"></asp:DropDownList>&nbsp;&nbsp;ĐĂNG TIN
                    </td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        <div class="checkbox">
                            <label><asp:CheckBox ID="checkbox_tinkythuat" runat="server" /> TIN KỸ THUẬT</label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="title"></td>
                    <td>
                        <div class="checkbox">
                            <label><asp:CheckBox ID="checkbox_luunhap" runat="server" /> LƯU NHÁP</label>
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
        var initEditor = function () {
            $("textarea.vmk_jquery_sceditor").sceditor({
                spellcheck: false,
                plugins: 'bbcode',
                toolbar: "bold,italic,underline|left,center,right,justify|bulletlist,orderedlist|font,size,color,removeformat|image,email,link,unlink|source",
                style: "sceditor/development/themes/default.css"
            });
        };
        initEditor();
    });
</script>

</asp:Content>
