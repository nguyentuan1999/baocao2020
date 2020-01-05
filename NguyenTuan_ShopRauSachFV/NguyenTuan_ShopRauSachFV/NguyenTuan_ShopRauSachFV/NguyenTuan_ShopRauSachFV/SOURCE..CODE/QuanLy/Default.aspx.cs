using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RAU_SACH_THANH_TRUC
{
    public partial class Admin_Default : System.Web.UI.Page
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

            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "QUẢN LÝ";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));
        }
    }
}
