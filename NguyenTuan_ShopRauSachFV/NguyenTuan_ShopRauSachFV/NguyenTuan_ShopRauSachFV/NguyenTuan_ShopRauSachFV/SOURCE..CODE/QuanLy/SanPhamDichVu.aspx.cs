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
    public partial class SanPhamDichVu : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        string PageName = "SanPhamDichVu.aspx";

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

            string vmk_title_page = "QUẢN LÝ SẢN PHẨM & DỊCH VỤ";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            btn_khongluu.NavigateUrl = PageName;

            label_thongbao.Text = "";

            // LẤY ID THÀNH VIÊN TỪ SESSION //

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien == "")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // BEGIN //

            if (!IsPostBack)
            {
                // TẠO NGÀY THÁNG NĂM //

                for (int i = 1; i < 32; i++)
                {
                    txt_ngaysp.Items.Add(i.ToString());
                    if (i < 13) { txt_thangsp.Items.Add(i.ToString()); }
                }
                for (int i = DateTime.Today.Year - 10; i < DateTime.Today.Year + 10; i++)
                {
                    txt_namsp.Items.Add(i.ToString());
                }

                // CHỌN NGÀY, THÁNG, NĂM MẶC ĐỊNH //

                txt_ngaysp.Items.FindByValue((DateTime.Today.Day).ToString()).Selected = true;
                txt_thangsp.Items.FindByValue((DateTime.Today.Month).ToString()).Selected = true;
                txt_namsp.Items.FindByValue((DateTime.Today.Year).ToString()).Selected = true;

                // TẠO DANH SÁCH DANH MỤC //

                dropdownlist_list_danhmuc.DataSource = sql_datasource_list_danhmuc;
                dropdownlist_list_danhmuc.DataValueField = "id_dm";
                dropdownlist_list_danhmuc.DataTextField = "ten_dm";
                dropdownlist_list_danhmuc.DataBind();
                if (dropdownlist_list_danhmuc.Items.Count != 0) { dropdownlist_list_danhmuc.SelectedIndex = 0; }

                // TẠO DANH SÁCH ĐƠN VỊ TÍNH //

                dropdownlist_list_dvt.DataSource = sql_datasource_list_dvt;
                dropdownlist_list_dvt.DataValueField = "id_dvt";
                dropdownlist_list_dvt.DataTextField = "ten_dvt";
                dropdownlist_list_dvt.DataBind();
                if (dropdownlist_list_dvt.Items.Count != 0) { dropdownlist_list_dvt.SelectedIndex = 0; }

                // LẤY DỮ LIỆU TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select san_pham.*, ten_dm, ten_dvt" +
                    " from san_pham, danh_muc, don_vi_tinh" +
                    " where" +
                    " san_pham.id_dm = danh_muc.id_dm" +
                    " and san_pham.id_dvt = don_vi_tinh.id_dvt" +
                    " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
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

            if (e.CommandName == "xoa")
            {
                // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select id_sp " +
                    " from san_pham " + 
                    " where " +
                    " id_sp in (" + id_item.Value + ") " +
                    " and (id_sp in (select id_sp from gio_hang) " +
                    " or id_sp in (select id_sp from don_hang_chi_tiet))") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. SẢN PHẨM NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                    return;
                }

                // TIẾN HÀNH XÓA MỘT DỮ LIỆU TRONG CSDL //
                
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete san_pham where id_sp = @id_sp";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "sua")
            {
                panel_them_sua.Visible = true;
                panel_xem.Visible = false;

                btn_them.Visible = false;
                btn_xoa.Visible = false;

                btn_luu.Visible = true;
                btn_khongluu.Visible = true;

                trang_thai.Value = "sua";
                id_item_for_edit.Value = id_item.Value;

                // LẤY DỮ LIỆU TRONG CSDL THEO ID //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select ten_sp, gioi_thieu, ngay_sp, thang_sp, nam_sp, don_gia, id_dm, id_dvt" +
                    " from san_pham" +
                    " where id_sp = @id_sp"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    // NẾU KHÔNG TÌM THẤY DỮ LIỆU CHUYỂN VỀ TRANG XEM //
                    Response.Redirect(PageName);
                    return;
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                txt_tensp.Text = BANG_KQ.Rows[0][0].ToString();
                txt_gioithieu.Text = BANG_KQ.Rows[0][1].ToString();
                txt_ngaysp.Text = BANG_KQ.Rows[0][2].ToString();
                txt_thangsp.Text = BANG_KQ.Rows[0][3].ToString();
                txt_namsp.Text = BANG_KQ.Rows[0][4].ToString();
                txt_dongia.Text = BANG_KQ.Rows[0][5].ToString();
                dropdownlist_list_danhmuc.SelectedValue = BANG_KQ.Rows[0][6].ToString();
                dropdownlist_list_dvt.SelectedValue = BANG_KQ.Rows[0][7].ToString();

                return;
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_them_Click(object sender, EventArgs e)
        {
            panel_them_sua.Visible = true;
            panel_xem.Visible = false;

            btn_them.Visible = false;
            btn_xoa.Visible = false;

            btn_luu.Visible = true;
            btn_khongluu.Visible = true;

            trang_thai.Value = "them";

            txt_tensp.Focus();
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string ten_sp = txt_tensp.Text.Trim();
            string gioi_thieu = txt_gioithieu.Text.Trim();
            string id_dm = dropdownlist_list_danhmuc.SelectedItem.Value;
            string id_dvt = dropdownlist_list_dvt.SelectedItem.Value;
            string don_gia = txt_dongia.Text.Trim();

            string ngay_sp = txt_ngaysp.SelectedItem.Value;
            string thang_sp = txt_thangsp.SelectedItem.Value;
            string nam_sp = txt_namsp.SelectedItem.Value;

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (ten_sp == "" || gioi_thieu == "" || id_dm == "" || id_dvt == "" || don_gia == "" || ngay_sp == "" || thang_sp == "" || nam_sp == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // KIỂM TRA ĐƠN GIÁ //

            Int64 number;
            bool check_number = Int64.TryParse(don_gia, out number);
            if (!check_number)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("ĐƠN GIÁ PHẢI LÀ SỐ NGUYÊN");
                return;
            }

            don_gia = number.ToString();

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            if (trang_thai.Value == "them")
            {
                vmk_csdl.sql_query = "insert into san_pham(id_dm,ten_sp,gioi_thieu,ngay_sp,thang_sp,nam_sp,don_gia,id_dvt)" +
                    " values(@id_dm,@ten_sp,@gioi_thieu,@ngay_sp,@thang_sp,@nam_sp,@don_gia,@id_dvt)"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_dm", id_dm, SqlDbType.VarChar);
                sql_param.Rows.Add("@ten_sp", ten_sp, SqlDbType.Int);
                sql_param.Rows.Add("@gioi_thieu", gioi_thieu, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_sp", ngay_sp, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_sp", thang_sp, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_sp", nam_sp, SqlDbType.SmallInt);
                sql_param.Rows.Add("@don_gia", don_gia, SqlDbType.BigInt);
                sql_param.Rows.Add("@id_dvt", id_dvt, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }
            else if (trang_thai.Value == "sua")
            {
                vmk_csdl.sql_query = "update san_pham" +
                    " set id_dm=@id_dm, ten_sp=@ten_sp, gioi_thieu=@gioi_thieu, ngay_sp=@ngay_sp," +
                    " thang_sp=@thang_sp, nam_sp=@nam_sp, don_gia=@don_gia, id_dvt=@id_dvt" +
                    " where id_sp=@id_sp"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item_for_edit.Value, SqlDbType.Int);
                sql_param.Rows.Add("@id_dm", id_dm, SqlDbType.Int);
                sql_param.Rows.Add("@ten_sp", ten_sp, SqlDbType.NVarChar);
                sql_param.Rows.Add("@gioi_thieu", gioi_thieu, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_sp", ngay_sp, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_sp", thang_sp, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_sp", nam_sp, SqlDbType.SmallInt);
                sql_param.Rows.Add("@don_gia", don_gia, SqlDbType.BigInt);
                sql_param.Rows.Add("@id_dvt", id_dvt, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_xoa_Click(object sender, EventArgs e)
        {
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

            // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select id_sp " +
                " from san_pham " +
                " where " +
                " id_sp in (" + String.Join(",", list_id_item.ToArray()) + ") " +
                " and (id_sp in (select id_sp from gio_hang) " +
                " or id_sp in (select id_sp from don_hang_chi_tiet))") == true)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. NHỮNG SẢN PHẨM NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                return;
            }

            // TIẾN HÀNH XÓA NHIỀU DỮ LIỆU TRONG CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();
            vmk_csdl.sql_query = "delete from san_pham where id_sp in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }
    }
}
