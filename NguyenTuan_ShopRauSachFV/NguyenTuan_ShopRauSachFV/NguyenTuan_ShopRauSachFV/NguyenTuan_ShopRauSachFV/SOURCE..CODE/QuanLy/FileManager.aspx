<%@ Page Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
 CodeBehind="FileManager.aspx.cs" Inherits="RAU_SACH_THANH_TRUC.FileManager" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Admin_HeadContent"></asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="Admin_MainContent">

<div class="panel panel-default" style="margin-bottom: 7px;">
    <div class="panel-body">
        <table style="width: 100%" class="vmk_table_form">
            <tr>
                <td style="width: 180px; white-space: nowrap"><span style="color: Red;">CHỌN HÌNH ẢNH (JPG)</span> &nbsp;</td>
            </tr>
            <tr>
                <td style="white-space: nowrap; overflow: hidden"><asp:FileUpload ID="vmk_file_uploader" runat="server" style="width: 100%; text-transform: uppercase" /></td>
            </tr>
            <tr>
                <td><asp:Button ID="btn_upload" runat="server" Text="TẢI HÌNH ẢNH LÊN" onclick="btn_upload_Click" class="btn btn-warning btn-xs" /></td>
            </tr>
        </table>
    </div>
</div>

<div class="panel panel-primary" style="margin-bottom: 7px;">
	<div class="panel-heading">
		<h3 class="panel-title">QUẢN LÝ HÌNH ẢNH</h3>
	</div>
	<div class="panel-body" style="max-height: 500px; overflow-y: scroll">

		<asp:Label ID="label_thongbao" runat="server"></asp:Label>

        <asp:Repeater ID="repeater_list_data" runat="server" 
            onitemcommand="repeater_list_data_ItemCommand">
            <HeaderTemplate>
		        <table style="width: 100%;" class="vmk_table_list_data">
                    <tr>
                        <td class="title">FILE NAME</td>
                        <td class="title">SIZE (BYTES)</td>
                        <td class="title">LIÊN KẾT CỦA HÌNH ẢNH</td>
                        <td class="title" colspan="2">CHỨC NĂNG</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="data" style="width: 200px">
                        <asp:HiddenField ID="id_item" runat="server" Value='<%# Eval("name") %>' />
                        <input type="text" value='<%# Eval("name") %>' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 110px">
                        <input type="text" value='<%# Eval("size") %>' onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data">
                        <input type="text" id="txt_url" vmk_file_name='<%# Eval("link") %>' value="" onclick="this.select()" readonly="readonly" />
                    </td>
                    <td class="data" style="width: 1px">
                        <a id="hyperlink_url" vmk_file_name='<%# Eval("link") %>' href="" target="_blank" class="btn btn-primary btn-xs">XEM ẢNH</a>
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
</div>

<script type="text/javascript">
    $(document).ready(function () {
        var UrlOfImgProduct = "";

        var Regex = /(.*?)\/\/(.*?)\//g;
        var Domain_With_Port = Regex.exec(window.location.href);
        if (Domain_With_Port.length == 3) {
            UrlOfImgProduct = Domain_With_Port[1] + "//" + Domain_With_Port[2] + "/images_products/";
        }

        $("input#txt_url").each(function () {
            var regex = /(.*?)\/\/(.*?)\//g;
            var Domain_With_Port = regex.exec(window.location.href);
            if (Domain_With_Port.length == 3) {
                $(this).val(UrlOfImgProduct + $(this).attr("vmk_file_name"));
            }
        });

        $("a#hyperlink_url").each(function () {
            $(this).prop("href", UrlOfImgProduct + $(this).attr("vmk_file_name"));
        });

    });
</script>

</asp:Content>
