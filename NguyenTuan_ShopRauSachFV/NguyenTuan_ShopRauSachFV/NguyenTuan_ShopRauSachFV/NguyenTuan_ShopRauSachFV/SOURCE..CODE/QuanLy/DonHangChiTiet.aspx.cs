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
    public partial class DonHangChiTiet : System.Web.UI.Page
    {
        string PageName1 = "DonHang.aspx";

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

        public string Xu_Ly_Gioi_Tinh(object gioi_tinh) { return ClassMain.Xu_Ly_Gioi_Tinh(gioi_tinh); }

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "XEM CHI TIẾT ĐƠN HÀNG";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            label_thongbao.Text = "";

            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // BEGIN //

            if (!IsPostBack)
            {
                // LẤY ID ĐƠN HÀNG TỪ BIẾN TRUYỀN TRONG URL //

                int id_dh = 0;
                bool check_number = false;

                if (Request.QueryString["id"] != null)
                {
                    check_number = int.TryParse(Request.QueryString["id"].ToString(), out id_dh);
                }

                if (!check_number || id_dh < 1)
                {
                    Response.Redirect(PageName1);
                    return;
                }

                label_sodonhang.Text = label_sodonhang.Text + id_dh.ToString();

                // KIỂM TRA TRẠNG THÁI ĐƠN HÀNG //

                ClassCSDL vmk_csdl1 = new ClassCSDL();
                vmk_csdl1.sql_query = "select khoa, ly_do_khoa" +
                    " from don_hang" +
                    " where" +
                    " id_dh = @id_dh"
                ;

                DataTable sql_param1 = vmk_csdl1.sql_param;
                sql_param1.Rows.Add("@id_dh", id_dh, SqlDbType.Int);
                vmk_csdl1.sql_param = sql_param1;

                DataTable BANG_KQ1 = vmk_csdl1.VMK_SQL_SELECT();

                if (BANG_KQ1.Rows.Count == 0)
                {
                    Response.Redirect(PageName1);
                    return;
                }

                bool khoa = Convert.ToBoolean(BANG_KQ1.Rows[0][0]);
                if (khoa == true)
                {
                    panel_lydodong.Visible = true;
                    label_lydodong.Text = HTML_Encode(BANG_KQ1.Rows[0][1].ToString());
                }

                // LẤY THÔNG TIN KHÁCH HÀNG THEO ID ĐƠN HÀNG //

                ClassCSDL vmk_csdl2 = new ClassCSDL();
                vmk_csdl2.sql_query = "select thanh_vien.ho_ten, thanh_vien.email, thanh_vien.gioi_tinh, thanh_vien.ngay_sinh, thanh_vien.thang_sinh, thanh_vien.nam_sinh, thanh_vien.sdt, thanh_vien.dia_chi" +
                    " from thanh_vien" +
                    " where" +
                    " id_tv = (select id_tv from don_hang where id_dh = @id_dh)"
                ;

                DataTable sql_param2 = vmk_csdl2.sql_param;
                sql_param2.Rows.Add("@id_dh", id_dh, SqlDbType.Int);
                vmk_csdl2.sql_param = sql_param2;

                DataTable BANG_KQ2 = vmk_csdl2.VMK_SQL_SELECT();

                if (BANG_KQ2.Rows.Count == 0)
                {
                    Response.Redirect(PageName1);
                    return;
                }

                txt_hoten.Text = BANG_KQ2.Rows[0][0].ToString();
                txt_email.Text = BANG_KQ2.Rows[0][1].ToString();
                txt_gioitinh.Text = Xu_Ly_Gioi_Tinh(BANG_KQ2.Rows[0][2]);
                txt_ngaysinh.Text = Xu_Ly_Ngay_Thang_Nam(BANG_KQ2.Rows[0][3],BANG_KQ2.Rows[0][4],BANG_KQ2.Rows[0][5]);
                txt_sdt.Text = BANG_KQ2.Rows[0][6].ToString();
                txt_diachi.Text = BANG_KQ2.Rows[0][7].ToString();

                // LẤY DỮ LIỆU TỪ CSDL THEO ID ĐƠN HÀNG //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select row_number() over (order by id_dhct asc) as stt, id_dhct, id_sp," +
                    " (select ten_sp from san_pham where id_sp = don_hang_chi_tiet.id_sp) as ten_sp," +
                    " don_gia, so_luong, thanh_tien, (select ten_dvt from don_vi_tinh where id_dvt = don_hang_chi_tiet.id_dvt) as ten_dvt" +
                    " from don_hang_chi_tiet" + 
                    " where id_dh = @id_dh"
                ;

                sql_datasource.SelectParameters.Clear();
                sql_datasource.SelectParameters.Add("id_dh", DbType.String, id_dh.ToString());
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    // TÍNH TỔNG TIỀN //

                    Int64 tong_tien = 0;
                    DataTable dt = vmk_dataview.ToTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Int64 thanh_tien = 0;
                        bool check_money = Int64.TryParse(dr[6].ToString(), out thanh_tien);
                        if (check_money == true)
                        {
                            tong_tien += thanh_tien;
                        }
                    }
                    label_tongtien.InnerText = Xu_Ly_Money(tong_tien) + " đ";

                    // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                    repeater_list_data.DataSource = sql_datasource;
                    repeater_list_data.DataBind();
                }
                else
                {
                    Response.Redirect(PageName1);
                    return;
                }
            }
        }
    }
}
