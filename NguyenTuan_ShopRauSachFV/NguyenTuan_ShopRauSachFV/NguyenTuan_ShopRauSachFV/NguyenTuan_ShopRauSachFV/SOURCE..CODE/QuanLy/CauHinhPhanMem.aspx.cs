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
    public partial class CauHinhPhanMem : System.Web.UI.Page
    {
        string PageName = "CauHinhPhanMem.aspx";

        string ten_cua_hang = "";

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q001" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false) { Response.Redirect("Default.aspx"); return; }

            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "CẤU HÌNH PHẦN MỀM";

            ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            label_thongbao.Text = "";

            if (!IsPostBack)
            {
                string dia_chi = "", sdt = "", email = "";

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

                txt_ten_cua_hang.Text = ten_cua_hang;
                txt_dia_chi.Text = dia_chi;
                txt_sdt.Text = sdt;
                txt_email.Text = email;
            }
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string ten_cua_hang = txt_ten_cua_hang.Text.Trim();
            string dia_chi = txt_dia_chi.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string email = txt_email.Text.Trim();

            if (ten_cua_hang == "" || dia_chi == "" || sdt == "" || email == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query = ""
                + "if not exists (select * from cau_hinh where ma_cau_hinh = 'ten_cua_hang')"
                + "   insert into cau_hinh(ma_cau_hinh, noi_dung) values ('ten_cua_hang', @ten_cua_hang)"
                + " else"
                + "   update cau_hinh set noi_dung = @ten_cua_hang where ma_cau_hinh = 'ten_cua_hang';"

                + "if not exists (select * from cau_hinh where ma_cau_hinh = 'dia_chi')"
                + "   insert into cau_hinh(ma_cau_hinh, noi_dung) values ('dia_chi', @dia_chi)"
                + " else"
                + "   update cau_hinh set noi_dung = @dia_chi where ma_cau_hinh = 'dia_chi';"

                + "if not exists (select * from cau_hinh where ma_cau_hinh = 'sdt')"
                + "   insert into cau_hinh(ma_cau_hinh, noi_dung) values ('sdt', @sdt)"
                + " else"
                + "   update cau_hinh set noi_dung = @sdt where ma_cau_hinh = 'sdt';"

                + "if not exists (select * from cau_hinh where ma_cau_hinh = 'email')"
                + "   insert into cau_hinh(ma_cau_hinh, noi_dung) values ('email', @email)"
                + " else"
                + "   update cau_hinh set noi_dung = @email where ma_cau_hinh = 'email';"
            ;
            
            DataTable sql_param = vmk_csdl.sql_param;
            sql_param.Rows.Add("@ten_cua_hang", ten_cua_hang, SqlDbType.NVarChar);
            sql_param.Rows.Add("@dia_chi", dia_chi, SqlDbType.NVarChar);
            sql_param.Rows.Add("@sdt", sdt, SqlDbType.NVarChar);
            sql_param.Rows.Add("@email", email, SqlDbType.NVarChar);
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            Response.Redirect(PageName);
        }
    }
}
