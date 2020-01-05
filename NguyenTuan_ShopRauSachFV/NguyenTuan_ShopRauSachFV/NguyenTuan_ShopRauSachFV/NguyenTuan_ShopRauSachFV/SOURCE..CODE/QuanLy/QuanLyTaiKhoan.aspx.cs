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
    public partial class QuanLyTaiKhoan : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        string PageName = "QuanLyTaiKhoan.aspx";

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q001" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        public string Xu_Ly_Gioi_Tinh(object gioi_tinh) { return ClassMain.Xu_Ly_Gioi_Tinh(gioi_tinh); }

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "QUẢN LÝ NHÂN VIÊN";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            btn_khongluu.NavigateUrl = PageName;

            label_thongbao.Text = "";

            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // LẤY ID THÀNH VIÊN //

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien.Trim() == "")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // BEGIN //

            if (!IsPostBack)
            {
                // TẠO DANH SÁCH QUYỀN HẠN //

                dropdownlist_list_quyenhan.DataSource = sql_datasource_list_quyenhan;
                dropdownlist_list_quyenhan.DataValueField = "ma_quyen";
                dropdownlist_list_quyenhan.DataTextField = "ten_quyen";
                dropdownlist_list_quyenhan.DataBind();
                dropdownlist_list_quyenhan.Items.FindByValue("Q002").Selected = true;

                // TẠO NGÀY THÁNG NĂM //

                for (int i = 1; i < 32; i++)
                {
                    txt_ngaysinh.Items.Add(i.ToString());
                    if (i < 13) { txt_thangsinh.Items.Add(i.ToString()); }
                }
                for (int i = DateTime.Today.Year - 80; i < DateTime.Today.Year - 17; i++)
                {
                    txt_namsinh.Items.Add(i.ToString());
                }
                txt_namsinh.Items.FindByValue((DateTime.Today.Year - 25).ToString()).Selected = true;

                // LẤY DỮ LIỆU TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select thanh_vien.*, ten_quyen from thanh_vien, quyen_han" +
                    " where thanh_vien.ma_quyen = quyen_han.ma_quyen and thanh_vien.ma_quyen in ('Q001','Q002')" + 
                    " order by id_tv asc"
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
                // KHÔNG CHO XÓA TÀI KHOẢN ĐANG ĐĂNG NHẬP //

                if (id_thanh_vien == id_item.Value)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN KHÔNG THỂ XÓA TÀI KHOẢN ĐANG SỬ DỤNG", "", false);
                    return;
                }

                // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select id_tv " +
                    " from thanh_vien " +
                    " where " +
                    " id_tv in (" + id_item.Value + ") " +
                    " and (id_tv in (select id_tv from tin_tuc) " +
                    " or id_tv in (select id_tv from gio_hang) " +
                    " or id_tv in (select id_tv from don_hang) " +
                    " or id_tv in (select id_tv from hoi_dap) " +
                    " or id_tv in (select id_tv from hoi_dap_tra_loi))") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. MÃ TÀI KHOẢN NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                    return;
                }

                // TIẾN HÀNH XÓA MỘT DỮ LIỆU TRONG CSDL //
                
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete thanh_vien where id_tv = @id_tv";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tv", id_item.Value, SqlDbType.Int);
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
                label_thongbao_thaydoi_matkhau.Visible = true;
                id_item_for_edit.Value = id_item.Value;

                // LẤY DỮ LIỆU TRONG CSDL THEO ID //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select ma_quyen,account_name,email,ho_ten,gioi_tinh,ngay_sinh,thang_sinh,nam_sinh,dia_chi,sdt,khoa,ly_do_khoa" +
                    " from thanh_vien where id_tv = @id_tv"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tv", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    // NẾU KHÔNG TÌM THẤY DỮ LIỆU CHUYỂN VỀ TRANG XEM //
                    Response.Redirect(PageName);
                    return;
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                dropdownlist_list_quyenhan.SelectedValue= BANG_KQ.Rows[0][0].ToString();

                txt_accountname.Text = BANG_KQ.Rows[0][1].ToString();
                txt_email.Text = BANG_KQ.Rows[0][2].ToString();

                txt_hoten.Text = BANG_KQ.Rows[0][3].ToString();

                bool gioi_tinh = Convert.ToBoolean(BANG_KQ.Rows[0][4]);
                if (gioi_tinh == true) { txt_gioitinh.SelectedValue = "1"; } else { txt_gioitinh.SelectedValue = "0"; }

                txt_ngaysinh.Text = BANG_KQ.Rows[0][5].ToString();
                txt_thangsinh.Text = BANG_KQ.Rows[0][6].ToString();
                txt_namsinh.Text = BANG_KQ.Rows[0][7].ToString();

                txt_diachi.Text = BANG_KQ.Rows[0][8].ToString();
                txt_sdt.Text = BANG_KQ.Rows[0][9].ToString();

                checkbox_khoa.Checked = Convert.ToBoolean(BANG_KQ.Rows[0][10]);

                txt_lydokhoa.Text = BANG_KQ.Rows[0][11].ToString();

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

            txt_accountname.Focus();
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string ma_quyen = dropdownlist_list_quyenhan.SelectedItem.Value;
            string account_name = txt_accountname.Text.Trim();
            string email = txt_email.Text.Trim();
            string mat_khau = txt_matkhau.Text.Trim();
            string ho_ten = txt_hoten.Text.Trim();

            bool gioi_tinh = false;
            if (txt_gioitinh.SelectedItem.Value == "1") { gioi_tinh = true; }

            string ngay_sinh = txt_ngaysinh.SelectedItem.Value;
            string thang_sinh = txt_thangsinh.SelectedItem.Value;
            string nam_sinh = txt_namsinh.SelectedItem.Value;

            string dia_chi = txt_diachi.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            bool khoa = checkbox_khoa.Checked;
            string ly_do_khoa = txt_lydokhoa.Text.Trim();

            if (khoa == false) { ly_do_khoa = ""; }

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (ma_quyen == "" || account_name == "" || email == "" || ho_ten == "" ||
                ngay_sinh == "" || thang_sinh == "" || nam_sinh == "" || dia_chi == "" ||  sdt == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // KIỂM TRA ACCOUNT NAME //

            if (System.Text.RegularExpressions.Regex.IsMatch(account_name, @"^[a-zA-Z0-9]+$") == false)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("TÀI KHOẢN CHỈ CHẤP NHẬN CHỮ CÁI VÀ SỐ");
                return;
            }

            // KIỂM TRA EMAIL //

            ClassEmailValid EmailValid = new ClassEmailValid();
            if (!EmailValid.IsValid_Email(email))
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("EMAIL KHÔNG ĐÚNG");
                return;
            }

            // KIỂM TRA SỐ ĐIỆN THOẠI //

            Int64 number;
            bool check_number = Int64.TryParse(sdt, out number);
            if (!check_number)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("ĐIỆN THOẠI PHẢI LÀ SỐ NGUYÊN");
                return;
            }

            sdt = number.ToString();

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            if (trang_thai.Value == "them")
            {
                // KIỂM TRA ACCOUNT NAME TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select account_name " + 
                    " from thanh_vien " + 
                    " where account_name = '" + account_name + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("TÀI KHOẢN NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KIỂM TRA EMAIL TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select email " + 
                    " from thanh_vien " + 
                    " where email = '" + email + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("EMAIL NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KIỂM TRA SĐT TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select sdt " + 
                    " from thanh_vien " + 
                    " where sdt = '" + sdt + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("SỐ ĐIỆN THOẠI NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KIỂM TRA MẬT KHẨU

                if (mat_khau == "")
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP MẬT KHẨU");
                    return;
                }

                vmk_csdl.sql_query = "insert into thanh_vien(ma_quyen,account_name,email,mat_khau,ho_ten,gioi_tinh,ngay_sinh,thang_sinh,nam_sinh,dia_chi,sdt,khoa,ly_do_khoa)" +
                    " values (@ma_quyen,@account_name,@email,@mat_khau,@ho_ten,@gioi_tinh, @ngay_sinh,@thang_sinh,@nam_sinh,@dia_chi,@sdt,@khoa,@ly_do_khoa)"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@ma_quyen", ma_quyen, SqlDbType.VarChar);
                sql_param.Rows.Add("@account_name", account_name, SqlDbType.VarChar);
                sql_param.Rows.Add("@email", email, SqlDbType.VarChar);
                sql_param.Rows.Add("@mat_khau", ClassMain.VMK_MAKE_MD5(mat_khau), SqlDbType.VarChar);
                sql_param.Rows.Add("@ho_ten", ho_ten, SqlDbType.NVarChar);
                sql_param.Rows.Add("@gioi_tinh", gioi_tinh, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_sinh", ngay_sinh, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_sinh", thang_sinh, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_sinh", nam_sinh, SqlDbType.SmallInt);
                sql_param.Rows.Add("@dia_chi", dia_chi, SqlDbType.NVarChar);
                sql_param.Rows.Add("@sdt", sdt, SqlDbType.BigInt);
                sql_param.Rows.Add("@khoa", khoa, SqlDbType.Bit);
                sql_param.Rows.Add("@ly_do_khoa", ly_do_khoa, SqlDbType.NVarChar);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }
            else if (trang_thai.Value == "sua")
            {
                // KIỂM TRA ACCOUNT NAME TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select account_name " + 
                    " from thanh_vien " + 
                    " where account_name = '" + account_name + "' and id_tv != '" + id_item_for_edit.Value + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("TÀI KHOẢN NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KIỂM TRA EMAIL TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select email " + 
                    " from thanh_vien " + 
                    " where email = '" + email + "' and id_tv != '" + id_item_for_edit.Value + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("EMAIL NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KIỂM TRA SĐT TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select sdt " + 
                    " from thanh_vien " + 
                    " where sdt = '" + sdt + "' and id_tv != '" + id_item_for_edit.Value + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("SỐ ĐIỆN THOẠI NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // KHÔNG THỂ KHÓA TÀI KHOẢN ĐANG SỬ DỤNG //

                if (id_thanh_vien == id_item_for_edit.Value && khoa == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN KHÔNG THỂ KHÓA TÀI KHOẢN ĐANG SỬ DỤNG", "", false);
                    checkbox_khoa.Checked = false;
                    return;
                }

                // CẬP NHẬT DỮ LIỆU VÀO CSDL //

                vmk_csdl.sql_query += "update thanh_vien";
                vmk_csdl.sql_query += " set ma_quyen=@ma_quyen, account_name=@account_name, email=@email,";

                if (mat_khau != "")
                {
                    vmk_csdl.sql_query += " mat_khau=@mat_khau,";
                }

                vmk_csdl.sql_query += " ho_ten=@ho_ten, gioi_tinh=@gioi_tinh, ngay_sinh=@ngay_sinh, thang_sinh=@thang_sinh, nam_sinh=@nam_sinh,";
                vmk_csdl.sql_query += " dia_chi=@dia_chi, sdt=@sdt, khoa=@khoa, ly_do_khoa=@ly_do_khoa";
                vmk_csdl.sql_query += " where id_tv=@id_tv";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tv", id_item_for_edit.Value, SqlDbType.Int);
                sql_param.Rows.Add("@ma_quyen", ma_quyen, SqlDbType.VarChar);
                sql_param.Rows.Add("@account_name", account_name, SqlDbType.VarChar);
                sql_param.Rows.Add("@email", email, SqlDbType.VarChar);
                sql_param.Rows.Add("@mat_khau", ClassMain.VMK_MAKE_MD5(mat_khau), SqlDbType.VarChar);
                sql_param.Rows.Add("@ho_ten", ho_ten, SqlDbType.NVarChar);
                sql_param.Rows.Add("@gioi_tinh", gioi_tinh, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_sinh", ngay_sinh, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_sinh", thang_sinh, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_sinh", nam_sinh, SqlDbType.SmallInt);
                sql_param.Rows.Add("@dia_chi", dia_chi, SqlDbType.NVarChar);
                sql_param.Rows.Add("@sdt", sdt, SqlDbType.BigInt);
                sql_param.Rows.Add("@khoa", khoa, SqlDbType.Bit);
                sql_param.Rows.Add("@ly_do_khoa", ly_do_khoa, SqlDbType.NVarChar);
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

            // KHÔNG CHO XÓA TÀI KHOẢN ĐANG ĐĂNG NHẬP //

            if (list_id_item.IndexOf(id_thanh_vien) != -1)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN KHÔNG THỂ XÓA TÀI KHOẢN ĐANG SỬ DỤNG", "", false);
                return;
            }

            // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select id_tv " +
                " from thanh_vien " +
                " where " +
                " id_tv in (" + String.Join(",", list_id_item.ToArray()) + ") " +
                " and (id_tv in (select id_tv from tin_tuc) " +
                " or id_tv in (select id_tv from gio_hang) " +
                " or id_tv in (select id_tv from don_hang) " +
                " or id_tv in (select id_tv from hoi_dap) " +
                " or id_tv in (select id_tv from hoi_dap_tra_loi))") == true)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. MÃ TÀI KHOẢN NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                return;
            }

            // TIẾN HÀNH XÓA NHIỀU DỮ LIỆU TRONG CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();
            vmk_csdl.sql_query = "delete from thanh_vien where id_tv in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }
    }
}
