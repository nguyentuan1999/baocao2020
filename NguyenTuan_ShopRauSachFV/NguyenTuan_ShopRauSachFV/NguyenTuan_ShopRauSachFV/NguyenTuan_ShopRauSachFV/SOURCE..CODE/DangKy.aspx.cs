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
    public partial class DangKy_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "ĐĂNG KÝ";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            label_thongbao.Text = "";

            if (!IsPostBack)
            {
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

                // ĐƯA CON TRỎ VỀ TEXTBOX FOCUS //

                txt_accountname.Focus();
            }
        }

        protected void btn_dangky_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string ma_quyen = "Q003";
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

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (ma_quyen == "" || account_name == "" || email == "" || mat_khau == "" || ho_ten == "" ||
                ngay_sinh == "" || thang_sinh == "" || nam_sinh == "" || dia_chi == "" || sdt == "")
            {
                label_thongbao.Text = "*** BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU" + "<br/><br/>";
                return;
            }

            // KIỂM TRA ACCOUNT NAME //

            if (System.Text.RegularExpressions.Regex.IsMatch(account_name, @"^[a-zA-Z0-9]+$") == false)
            {
                label_thongbao.Text = "*** TÀI KHOẢN CHỈ CHẤP NHẬN CHỮ CÁI VÀ SỐ" + "<br/><br/>";
                return;
            }

            // KIỂM TRA ACCOUNT NAME TRONG CSDL //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select account_name " +
                " from thanh_vien " + 
                " where account_name = '" + account_name + "'") == true)
            {
                label_thongbao.Text = "*** TÀI KHOẢN NÀY ĐƯỢC SỬ DỤNG RỒI" + "<br/><br/>";
                return;
            }

            // KIỂM TRA EMAIL //

            ClassEmailValid EmailValid = new ClassEmailValid();
            if (!EmailValid.IsValid_Email(email))
            {
                label_thongbao.Text = "*** EMAIL KHÔNG ĐÚNG" + "<br/><br/>";
                return;
            }

            // KIỂM TRA EMAIL TRONG CSDL //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select email " +
                " from thanh_vien " + 
                " where email = '" + email + "'") == true)
            {
                label_thongbao.Text = "*** EMAIL NÀY ĐƯỢC SỬ DỤNG RỒI" + "<br/><br/>";
                return;
            }

            // KIỂM TRA SỐ ĐIỆN THOẠI //

            Int64 number;
            bool check_number = Int64.TryParse(sdt, out number);
            if (!check_number)
            {
                label_thongbao.Text = "*** ĐIỆN THOẠI PHẢI LÀ SỐ NGUYÊN" + "<br/><br/>";
                return;
            }

            sdt = number.ToString();

            // KIỂM TRA SĐT TRONG CSDL //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select sdt " +
                " from thanh_vien " + 
                " where sdt = '" + sdt + "'") == true)
            {
                label_thongbao.Text = "*** SỐ ĐIỆN THOẠI NÀY ĐƯỢC SỬ DỤNG RỒI" + "<br/><br/>";
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query = "insert into thanh_vien(ma_quyen,account_name,email,mat_khau,ho_ten,gioi_tinh,ngay_sinh,thang_sinh,nam_sinh,dia_chi,sdt)" +
                " values (@ma_quyen,@account_name,@email,@mat_khau,@ho_ten,@gioi_tinh, @ngay_sinh,@thang_sinh,@nam_sinh,@dia_chi,@sdt)"
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
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // THÔNG BÁO ĐĂNG KÝ THÀNH CÔNG //

            label_thongbao.Text = "*** ĐĂNG KÝ THÀNH CÔNG" + "<br/><br/>";
            panel_form_dangnhap.Visible = true;
            panel_form_dangky.Visible = false;
        }
    }
}
