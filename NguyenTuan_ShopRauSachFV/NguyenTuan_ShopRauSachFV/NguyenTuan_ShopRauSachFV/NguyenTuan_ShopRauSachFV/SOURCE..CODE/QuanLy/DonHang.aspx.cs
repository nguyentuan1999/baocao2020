using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace RAU_SACH_THANH_TRUC
{
    public partial class DonHang : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        string PageName = "DonHang.aspx";

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string Xu_Ly_Money(object money_obj)
        {
            string money_truockhixuly = Convert.ToString(money_obj);
            string money_saukhixuly = "0";
            Int64 money = 0;
            bool check_money = Int64.TryParse(money_truockhixuly, out money);
            if (check_money == true) { money_saukhixuly = String.Format("{0:#,##}", money); }
            return money_saukhixuly;
        }

        public string Xu_Ly_Css_Display(object data, bool show_when)
        {
            bool status = Convert.ToBoolean(data);
            if (status == show_when) { return "block"; }
            return "none";
        }

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q002" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "QUẢN LÝ ĐƠN HÀNG";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            btn_khongluu.NavigateUrl = PageName;

            label_thongbao.Text = "";

            // LẤY ID THÀNH VIÊN TỪ SESSION //

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien == "") { Response.Redirect("Default.aspx"); return; }

            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false) { Response.Redirect("Default.aspx"); return; }

            // BEGIN //

            if (!IsPostBack)
            {
                // LẤY DỮ LIỆU TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select don_hang.*, ho_ten," +
                    " (select count(*) from don_hang_chi_tiet where don_hang_chi_tiet.id_dh = don_hang.id_dh) as so_luong_hang_hoa" +
                    " from don_hang, thanh_vien" +
                    " where don_hang.id_tv = thanh_vien.id_tv" +
                    " order by khoa asc, nam_dh desc, thang_dh desc, ngay_dh desc, id_dh desc"
                ;

                sql_datasource.SelectParameters.Clear();
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    repeater_list_data.DataSource = sql_datasource;
                    repeater_list_data.DataBind();
                }
                else
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("CHƯA CÓ DỮ LIỆU","",false);
                    return;
                }
            }
        }

        protected void repeater_list_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_item = (HiddenField)e.Item.FindControl("id_item");

            /*

            // BỎ CHỨC NĂNG XÓA ĐƠN HÀNG

            if (e.CommandName == "xoa")
            {
                // XÓA ĐƠN HÀNG CHI TIẾT TRƯỚC //

                ClassCSDL vmk_csdl1 = new ClassCSDL();
                vmk_csdl1.sql_query = "delete don_hang_chi_tiet where id_dh = @id_dh";

                DataTable sql_param1 = vmk_csdl1.sql_param;
                sql_param1.Rows.Add("@id_dh", id_item.Value, SqlDbType.Int);
                vmk_csdl1.sql_param = sql_param1;

                int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();

                // SAU ĐÓ XÓA ĐƠN HÀNG //

                ClassCSDL vmk_csdl2 = new ClassCSDL();
                vmk_csdl2.sql_query = "delete don_hang where id_dh = @id_dh";

                DataTable sql_param2 = vmk_csdl2.sql_param;
                sql_param2.Rows.Add("@id_dh", id_item.Value, SqlDbType.Int);
                vmk_csdl2.sql_param = sql_param2;

                int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();
            }
            
            */

            if (e.CommandName == "mo_khoa")
            {
                ClassCSDL vmk_csdl = new ClassCSDL();

                vmk_csdl.sql_query = "update don_hang set khoa=@khoa,ly_do_khoa=@ly_do_khoa where id_dh=@id_dh";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_dh", id_item.Value, SqlDbType.Int);
                sql_param.Rows.Add("@khoa", false, SqlDbType.Bit);
                sql_param.Rows.Add("@ly_do_khoa", null, SqlDbType.NVarChar);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "khoa")
            {
                panel_xem.Visible = false;
                panel_sua.Visible = true;

                id_item_for_edit.Value = id_item.Value;

                btn_luu_lydokhoa.Visible = true;
                btn_khongluu.Visible = true;
                btn_xoa.Visible = false;

                txt_lydokhoa.Focus();

                return;
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_luu_lydokhoa_Click(object sender, EventArgs e)
        {
            string lydokhoa = txt_lydokhoa.Text.Trim();
            string id_hd = id_item_for_edit.Value;

            if (lydokhoa == "" || id_hd == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐỦ DỮ LIỆU");
                return;
            }

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query = "update don_hang set khoa=@khoa,ly_do_khoa=@ly_do_khoa where id_dh=@id_dh";

            DataTable sql_param = vmk_csdl.sql_param;
            sql_param.Rows.Add("@id_dh", id_hd, SqlDbType.Int);
            sql_param.Rows.Add("@khoa", true, SqlDbType.Bit);
            sql_param.Rows.Add("@ly_do_khoa", lydokhoa, SqlDbType.NVarChar);
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        

        // BỎ TÍNH NĂNG XÓA ĐƠN HÀNG

        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            /*

            int number;
            int selected_id_item_count = 0;
            ArrayList list_id_item = new ArrayList();

            foreach (RepeaterItem i in repeater_list_data.Items)
            {
                CheckBox cb = (CheckBox)i.FindControl("checkbox_item_select");
                if (cb.Checked)
                {
                    HiddenField id_item = (HiddenField)i.FindControl("id_item");

                    bool check_number = int.TryParse(id_item.Value, out number);
                    if (check_number == true && number > 0)
                    {
                        list_id_item.Add(number.ToString());
                        selected_id_item_count += 1;
                    }
                }
            }

            // KIỂM TRA DỮ LIỆU CÓ ĐƯỢC CHỌN KHÔNG //

            if (selected_id_item_count == 0)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA CHỌN ĐỐI TƯỢNG NÀO");
                return;
            }

            // XÓA ĐƠN HÀNG CHI TIẾT TRƯỚC //

            ClassCSDL vmk_csdl1 = new ClassCSDL();
            vmk_csdl1.sql_query = "delete from don_hang_chi_tiet where id_dh in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();

            // SAU ĐÓ XÓA ĐƠN HÀNG //

            ClassCSDL vmk_csdl2 = new ClassCSDL();
            vmk_csdl2.sql_query = "delete from don_hang where id_dh in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);

            */
        }
    }
}
