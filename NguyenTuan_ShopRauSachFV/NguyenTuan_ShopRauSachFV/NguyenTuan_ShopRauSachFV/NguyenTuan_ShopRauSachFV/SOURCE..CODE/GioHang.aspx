<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
 CodeBehind="GioHang.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.GioHang_TrangChu" %>

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
        <h5 style="margin-bottom: 3px; color: #60A90A">GIỎ HÀNG CỦA TÔI</h5>
        <div style="background:#FFFFFF; border-top:2px solid #f3831f; margin-bottom: 5px;"></div>
        <asp:Label ID="label_thongbao" runat="server" ForeColor="Red"></asp:Label>
        <table style="width: 100%" class="vmk_table_form">
            <asp:Repeater ID="repeater_list_data" runat="server" 
                onitemcommand="repeater_list_data_ItemCommand">
                <HeaderTemplate>
                    <tr>
                        <td style="white-space: nowrap"><b>SẢN PHẨM</b></td>
                        <td style="white-space: nowrap"><b>ĐƠN GIÁ</b></td>
                        <td style="white-space: nowrap"><b>ĐVT</b></td>
                        <td style="white-space: nowrap"><b>SỐ LƯỢNG</b></td>
                        <td style="white-space: nowrap"><b>THÀNH TIỀN</b></td>
                        <td style="white-space: nowrap"></td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("id_sp") %>' />
                            <input type="text" value='<%# Eval("ten_sp") %>' onclick="this.select()" readonly="readonly" style="width: 98%" />
                        </td>
                        <td style="width: 100px; white-space: nowrap; padding-right: 5px;">
                            <input type="text" value='<%# Xu_Ly_Money(Eval("don_gia")) %> đ' onclick="this.select()" readonly="readonly" style="width: 95%" />
                        </td>
                        <td style="width: 100px; white-space: nowrap; padding-right: 5px;">
                            <input type="text" value='<%# Eval("ten_dvt") %>' onclick="this.select()" readonly="readonly" style="width: 95%" />
                        </td>
                        <td style="width: 100px; white-space: nowrap">
                            <asp:TextBox ID="txt_soluong" runat="server" Text='<%# Eval("so_luong") %>' vmk_control_id='txt_soluong' MaxLength="3" onclick="this.select()" style="width: 60%"></asp:TextBox>
                            <asp:ImageButton ID="imgbtn_edit_soluong" runat="server" ImageUrl="images/pencil-icon.png" CommandName="edit_soluong" />
                            &nbsp;
                        </td>
                        <td style="width: 100px; white-space: nowrap">
                            <input type="text" value='<%# Xu_Ly_Money(Eval("thanh_tien")) %> đ' onclick="this.select()" readonly="readonly" style="width: 90%" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtn_xoa_item" runat="server" ImageUrl="images/delete.png" CommandName="xoa" style="cursor: pointer" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="float: left; margin-bottom: 20px; color: blue">
            THÀNH TIỀN GIỎ HÀNG: <asp:Label ID="label_thanhtiengiohang" runat="server" style="color: Red;">0</asp:Label>&nbsp;<sup>đ</sup>
        </div>
        <div style="float: right; margin-bottom: 20px;">
            <asp:Button ID="btn_dathang" runat="server" Text="ĐẶT HÀNG" OnClick="btn_dathang_Click" Visible="false" class="button" />
            <a href="Default.aspx" class="button" style="padding: 11px;">TIẾP TỤC MUA HÀNG</a>
        </div>
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

            $("input[type='image']").each(function () {
                var d = new Date();
                $(this).attr("src", $(this).attr("src") + "?" + d.getTime());
                $(this).css("display", "inline");
            });

            $("input[vmk_control_id='txt_soluong']").keypress(function (e) {
                var charCode = (e.which) ? e.which : e.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
            });
        });
    </script>
</asp:Content>