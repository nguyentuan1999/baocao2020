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
    public partial class Admin_SiteMaster : System.Web.UI.MasterPage
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
            // KIỂM TRA TRẠNG THÁI ĐĂNG NHẬP //

            if (kiem_tra_dang_nhap() == false) { Response.Redirect("DangNhap.aspx"); return; }

            // MENU & QUYỀN HẠN //

            /*
                menu_cauhinhhethong
                menu_thongtincanhan
                menu_tintuc
                menu_danhmuc
                menu_donvitinh
                menu_sanphamdichvu
                menu_khachhang
                menu_donhang
                menu_hoidap
                menu_hinhanh
            */

            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null) { ma_quyen = ""; }

            switch (ma_quyen.ToUpper())
            {
                case "Q001":
                    menu_cauhinhphanmem.Visible = true;
                    menu_quanlytaikhoan.Visible = true;
                    menu_thongtincanhan.Visible = true;
                    break;
                case "Q002":
                    menu_thongtincanhan.Visible = true;
                    menu_tintuc.Visible = true;
                    menu_danhmuc.Visible = true;
                    menu_donvitinh.Visible = true;
                    menu_sanphamdichvu.Visible = true;
                    menu_khachhang.Visible = true;
                    menu_donhang.Visible = true;
                    menu_hoidap.Visible = true;
                    menu_hinhanh.Visible = true;
                    break;
                default:
                    Response.Redirect("Thoat.aspx");
                    break;
            }

            // BEGIN //

            if (!IsPostBack)
            {
                string ten_cua_hang = "", dia_chi = "", sdt = "", email = "";

                // LẤY DỮ LIỆU TRONG CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select ma_cau_hinh, noi_dung from cau_hinh";

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();
                DataRow[] BANG_KQ_FILTER;

                if (BANG_KQ.Rows.Count != 0)
                {
                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'ten_cua_hang'");
                    if (BANG_KQ_FILTER.Length != 0) { ten_cua_hang = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'dia_chi'");
                    if (BANG_KQ_FILTER.Length != 0) { dia_chi = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'sdt'");
                    if (BANG_KQ_FILTER.Length != 0) { sdt = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'email'");
                    if (BANG_KQ_FILTER.Length != 0) { email = BANG_KQ_FILTER[0][1].ToString(); }
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                label_tencuahang.Text = ten_cua_hang.Trim();

                // LƯU TÊN CỬA HÀNG VÀO SESSION ĐỂ SỬ DỤNG CHO CÁC PAGE KHÁC

                ClassMain.Xu_Ly_Session("SET", "ten_cua_hang", ten_cua_hang.Trim());
            }
        }
    }
}