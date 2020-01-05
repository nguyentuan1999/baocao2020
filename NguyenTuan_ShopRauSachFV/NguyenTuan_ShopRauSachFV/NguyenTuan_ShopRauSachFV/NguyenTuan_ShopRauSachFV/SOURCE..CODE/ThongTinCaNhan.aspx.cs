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
    public partial class ThongTinCaNhan_TrangChu : System.Web.UI.Page
    {
        string id_thanh_vien = "";
        string id_tv_from_url = "";
        string id_tv_from_post = "";

        private bool Kiem_Tra_Quyen_Han_Admin()
        {
            string[] ds_quyen_cho_phep = { "Q001", "Q002" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0){return false;}
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "THÔNG TIN CÁ NHÂN";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////
            
            label_thongbao.Text = "";

            // LẤY ID THÀNH VIÊN ĐÃ ĐĂNG NHẬP //

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien == "") { Response.Redirect("DangNhap.aspx"); return; }

            // LẤY ID THÀNH VIÊN TỪ BIẾN TRUYỀN TRONG URL //

            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                id_tv_from_url = Request.QueryString["id"].ToString().ToLower();
            }

            // BEGIN //

            if (!IsPostBack)
            {
                // KIỂM TRA ID THÀNH VIÊN & QUYỀN HẠN //

                if (id_tv_from_url != "")
                {
                    // KIỂM TRA QUYỀN HẠN. NẾU TRUYỀN BIẾN ID THÀNH VIÊN MÀ KHÔNG CÓ QUYỀN QUẢN LÝ SẼ BỊ TỪ CHỐI //

                    if (Kiem_Tra_Quyen_Han_Admin() == false)
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }
                else
                {
                    // NẾU KHÔNG TRUYỀN BIẾN ID THÀNH VIÊN THÌ CHỈ XEM & SỬA ĐƯỢC CHÍNH MÌNH //

                    id_tv_from_url = id_thanh_vien;
                }

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

                // LẤY DỮ LIỆU TỪ CSDL THEO ID THÀNH VIÊN YÊU CẦU //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select account_name,email,ho_ten,gioi_tinh,ngay_sinh,thang_sinh,nam_sinh,dia_chi,sdt" +
                    " from thanh_vien where id_tv = @id_tv"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tv", id_tv_from_url, SqlDbType.VarChar);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                if (BANG_KQ.Rows.Count == 0)
                {
                    label_thongbao.Text = "*** KHÔNG CÓ DỮ LIỆU" + "<br/><br/>";
                    return;
                }

                id_tv_for_edit.Value = id_tv_from_url;
                txt_accountname.Text = BANG_KQ.Rows[0][0].ToString();
                txt_email.Text = BANG_KQ.Rows[0][1].ToString();
                txt_hoten.Text = BANG_KQ.Rows[0][2].ToString();

                bool gioi_tinh = Convert.ToBoolean(BANG_KQ.Rows[0][3]);
                if (gioi_tinh == true) { txt_gioitinh.SelectedValue = "1"; } else { txt_gioitinh.SelectedValue = "0"; }

                txt_ngaysinh.SelectedValue = BANG_KQ.Rows[0][4].ToString();
                txt_thangsinh.SelectedValue = BANG_KQ.Rows[0][5].ToString();
                txt_namsinh.SelectedValue = BANG_KQ.Rows[0][6].ToString();

                txt_diachi.Text = BANG_KQ.Rows[0][7].ToString();
                txt_sdt.Text = BANG_KQ.Rows[0][8].ToString();
            }
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            id_tv_from_post = id_tv_for_edit.Value;
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

            if (account_name == "" || email == "" || ho_ten == "" || ngay_sinh == "" || thang_sinh == "" || nam_sinh == "" || dia_chi == "" || sdt == "")
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
                " where account_name = '" + account_name + "' and id_tv != '" + id_tv_from_post + "'") == true)
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
                " where email = '" + email + "' and id_tv != '" + id_tv_from_post + "'") == true)
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
                " where sdt = '" + sdt + "' and id_tv != '" + id_tv_from_post + "'") == true)
            {
                label_thongbao.Text = "*** SỐ ĐIỆN THOẠI NÀY ĐƯỢC SỬ DỤNG RỒI" + "<br/><br/>";
                return;
            }

            // CẬP NHẬT DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query += "update thanh_vien";
            vmk_csdl.sql_query += " set account_name=@account_name, email=@email,";

            // NẾU MẬT KHẨU RỖNG THÌ KHÔNG CẬP NHẬT //

            if (mat_khau != "")
            {
                vmk_csdl.sql_query += " mat_khau=@mat_khau,";
            }

            vmk_csdl.sql_query += " ho_ten=@ho_ten, gioi_tinh=@gioi_tinh, ngay_sinh=@ngay_sinh, thang_sinh=@thang_sinh, nam_sinh=@nam_sinh,";
            vmk_csdl.sql_query += " dia_chi=@dia_chi, sdt=@sdt";
            vmk_csdl.sql_query += " where id_tv=@id_tv";

            DataTable sql_param = vmk_csdl.sql_param;

            // KIỂM TRA QUYỀN HẠN. NẾU QUẢN LÝ THÌ SỬA AI CŨNG ĐƯỢC. NGƯỢC LẠI CHỈ SỬA ĐƯỢC CHÍNH MÌNH //

            if (Kiem_Tra_Quyen_Han_Admin() == false)
            {
                id_tv_from_post = id_thanh_vien;
            }

            sql_param.Rows.Add("@id_tv", id_tv_from_post, SqlDbType.Int);
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

            // SẼ THOÁT NẾU THAY ĐỔI MẬT KHẨU CỦA MÌNH //

            if (mat_khau != "" && id_tv_from_post == id_thanh_vien)
            {
                Response.Redirect("Thoat.aspx");
                return;
            }

            // TẢI LẠI THÔNG TIN CÁ NHÂN //

            if (id_tv_from_post == id_thanh_vien)
            {
                Response.Redirect("ThongTinCaNhan.aspx");
                return;
            }
            else
            {
                Response.Redirect("ThongTinCaNhan.aspx?id=" + id_tv_from_post);
                return;
            }
        }
    }
}
