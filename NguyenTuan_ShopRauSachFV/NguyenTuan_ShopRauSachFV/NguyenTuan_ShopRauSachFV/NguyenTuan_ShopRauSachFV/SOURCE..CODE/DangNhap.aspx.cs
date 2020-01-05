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
    public partial class DangNhap_TrangChu : System.Web.UI.Page
    {
        private bool kiem_tra_dang_nhap()
        {
            string id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (id_thanh_vien != null && ma_quyen != null) { return true; }
            return false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (kiem_tra_dang_nhap() == true) { Response.Redirect("Default.aspx"); return; }

            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "ĐĂNG NHẬP";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            label_thongbao.Text = "";

            if (!IsPostBack) { txt_taikhoan.Focus(); }
        }

        protected void btn_dangnhap_Click(object sender, EventArgs e)
        {
            string id_thanh_vien = "";
            string ma_quyen = "";
            string mat_khau_from_csdl = "";
            bool khoa = false;

            string tai_khoan = txt_taikhoan.Text.Trim();
            string mat_khau_from_client = txt_matkhau.Text.Trim();
            string captcha_from_client = txt_captcha.Text.Trim();

            // TẠO MỚI CLASS CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            if (tai_khoan == "" || mat_khau_from_client == "" || captcha_from_client == "")
            {
                label_thongbao.Text = "*** BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU" + "<br/><br/>";
                txt_taikhoan.Focus();
                return;
            }

            // KIỂM TRA CAPTCHA //

            string captcha = ClassMain.Xu_Ly_Session("GET", "captcha");
            if (captcha == null) { captcha = ""; }

            if (captcha.Trim() == "")
            {
                label_thongbao.Text = "*** KHÔNG KIỂM TRA ĐƯỢC CAPTCHA";
                txt_captcha.Text = "";
                return;
            }

            if (captcha_from_client != captcha)
            {
                label_thongbao.Text = "*** MÃ XÁC NHẬN KHÔNG ĐÚNG";
                txt_captcha.Text = "";
                return;
            }

            ClassMain.Xu_Ly_Session("REMOVE", "captcha");

            // LẤY THÔNG TIN TỪ CSDL //

            vmk_csdl.sql_query = "select top(1) mat_khau, id_tv, ma_quyen, khoa from thanh_vien where email = @email or account_name = @account_name";

            DataTable sql_param = vmk_csdl.VMK_SQL_PARAM();
            sql_param.Rows.Add("@email", tai_khoan, SqlDbType.VarChar);
            sql_param.Rows.Add("@account_name", tai_khoan, SqlDbType.VarChar);
            vmk_csdl.sql_param = sql_param;

            DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

            if (BANG_KQ.Rows.Count != 0)
            {
                mat_khau_from_csdl = BANG_KQ.Rows[0][0].ToString();
                id_thanh_vien = BANG_KQ.Rows[0][1].ToString();
                ma_quyen = BANG_KQ.Rows[0][2].ToString();
                khoa = Convert.ToBoolean(BANG_KQ.Rows[0][3]);
            }

            if (khoa == true)
            {
                label_thongbao.Text = "*** TÀI KHOẢN NÀY ĐÃ BỊ KHÓA" + "<br/><br/>";
                txt_captcha.Text = "";
                txt_taikhoan.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU //

            if (ClassMain.VMK_CHECK_MD5(mat_khau_from_client, mat_khau_from_csdl) == true && id_thanh_vien != "")
            {
                ClassMain.Xu_Ly_Session("SET", "id_thanh_vien", id_thanh_vien);
                ClassMain.Xu_Ly_Session("SET", "ma_quyen", ma_quyen);
                Response.Redirect("Default.aspx");
                return;
            }

            txt_captcha.Text = "";
            label_thongbao.Text = "*** TÀI KHOẢN HOẶC MẬT KHẨU KHÔNG ĐÚNG" + "<br/><br/>";
            txt_taikhoan.Focus();
        }
    }
}
