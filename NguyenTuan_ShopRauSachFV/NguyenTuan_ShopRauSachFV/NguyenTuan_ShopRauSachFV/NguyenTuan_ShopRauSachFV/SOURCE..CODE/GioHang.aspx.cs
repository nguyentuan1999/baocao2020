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
    public partial class GioHang_TrangChu : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        private bool Kiem_Tra_Quyen_Han_La_Khach_Hang()
        {
            string[] ds_quyen_cho_phep = { "Q003"};
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        public string Xu_Ly_Money(object money_obj)
        {
            string money_truockhixuly = Convert.ToString(money_obj);
            string money_saukhixuly = "0";
            Int64 money = 0;
            bool check_money = Int64.TryParse(money_truockhixuly, out money);
            if (check_money == true) { money_saukhixuly = String.Format("{0:#,##}", money); }
            return money_saukhixuly;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "GIỎ HÀNG";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            // CHỈ CHẤP NHẬN TÀI KHOẢN THUỘC NHÓM KHÁCH HÀNG MỚI ĐƯỢC ĐẶT HÀNG //

            if (Kiem_Tra_Quyen_Han_La_Khach_Hang() == false)
            {
                label_thongbao.Text = "*** CHỈ CÓ KHÁCH HÀNG MỚI ĐƯỢC ĐẶT HÀNG" + "<br/><br/>";
                return;
            }

            // KIỂM TRA TRẠNG THÁI ĐĂNG NHẬP //

            string id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien == "")
            {
                label_thongbao.Text = "*** BẠN CHƯA ĐĂNG NHẬP ĐỂ THÊM VÀ XEM GIỎ HÀNG" + "<br/><br/>";
                return;
            }

            if(!IsPostBack)
            {
                // LẤY ID SẢN PHẨM TỪ BIẾN TRUYỀN TRONG URL //

                string id_sp = "";

                if (Request.QueryString["ThemSP"] != null && Request.QueryString["ThemSP"].ToString() != "")
                {
                    id_sp = Request.QueryString["ThemSP"].ToString().ToLower();
                }

                // THÊM SẢN PHẨM VÀO GIỎ HÀNG //

                if (id_sp != "")
                {
                    // TIẾN HÀNH NHẬP VÀO CSDL //

                    ClassCSDL vmk_csdl2 = new ClassCSDL();

                    vmk_csdl2.sql_query = "" +
                        " if not exists (select id_sp from gio_hang where id_tv = @id_tv and id_sp = @id_sp)" +
                        "     insert into gio_hang(id_tv, id_sp, don_gia, so_luong, id_dvt)" + 
                        "         values (@id_tv, @id_sp, (select don_gia from san_pham where id_sp = @id_sp), 1, (select id_dvt from san_pham where id_sp = @id_sp))" +
                        " else" +
                        "     update gio_hang set so_luong = so_luong + 1 where id_tv = @id_tv and id_sp = @id_sp"
                    ;

                    DataTable sql_param2 = vmk_csdl2.sql_param;
                    sql_param2.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
                    sql_param2.Rows.Add("@id_sp", id_sp, SqlDbType.Int);
                    vmk_csdl2.sql_param = sql_param2;

                    int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

                    Response.Redirect("GioHang.aspx");
                }

                // LẤY DỮ LIỆU GIỎ HÀNG TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select gio_hang.id_sp, san_pham.ten_sp, gio_hang.don_gia, gio_hang.so_luong, gio_hang.thanh_tien, (select ten_dvt from don_vi_tinh where id_dvt = san_pham.id_dvt) as ten_dvt" +
                    " from gio_hang, san_pham" +
                    " where" + 
                    " gio_hang.id_sp = san_pham.id_sp" + 
                    " and id_tv = @id_tv"
                ;

                sql_datasource.SelectParameters.Clear();
                sql_datasource.SelectParameters.Add("id_tv", DbType.String, id_thanh_vien.ToString());
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    Int64 thanh_tien_gio_hang = 0;

                    foreach (DataRow dr in vmk_dataview.ToTable().Rows)
                    {
                        thanh_tien_gio_hang += Convert.ToInt64(dr[4]);
                    }

                    repeater_list_data.DataSource = sql_datasource;
                    repeater_list_data.DataBind();

                    btn_dathang.Visible = true;
                    label_thanhtiengiohang.Visible = true;
                    label_thanhtiengiohang.Text = Xu_Ly_Money(thanh_tien_gio_hang);
                }
                else
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("GIỎ HÀNG CHƯA CÓ SẢN PHẨM NÀO", "", false);
                    return;
                }
            }
        }

        protected void repeater_list_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_item = (HiddenField)e.Item.FindControl("id_item");
            TextBox txt_soluong = (TextBox)e.Item.FindControl("txt_soluong");

            if (e.CommandName == "xoa")
            {
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete from gio_hang where id_sp = @id_sp";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "edit_soluong")
            {
                int so_luong;
                bool check_soluong = int.TryParse(txt_soluong.Text, out so_luong);
                if (check_soluong == false)
                {
                    so_luong = 1;
                }

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "update gio_hang" +
                    " set so_luong = @so_luong" +
                    " where id_sp = @id_sp"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_item.Value, SqlDbType.Int);
                sql_param.Rows.Add("@so_luong", so_luong, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            Response.Redirect("GioHang.aspx");
        }

        protected void btn_dathang_Click(object sender, EventArgs e)
        {
            // CHỈ CHẤP NHẬN TÀI KHOẢN THUỘC NHÓM KHÁCH HÀNG MỚI ĐƯỢC ĐẶT HÀNG //

            if (Kiem_Tra_Quyen_Han_La_Khach_Hang() == false)
            {
                label_thongbao.Text = "*** CHỈ CÓ KHÁCH HÀNG MỚI ĐƯỢC ĐẶT HÀNG" + "<br/><br/>";
                return;
            }

            // KIỂM TRA TRẠNG THÁI ĐĂNG NHẬP //

            if (id_thanh_vien == "") { Response.Redirect("GioHang.aspx"); }

            // TẠO ĐƠN HÀNG VÀ LẤY ID ĐƠN HÀNG //

            ClassCSDL vmk_csdl1 = new ClassCSDL();

            // QUERY FOR SQL SERVER 2005+ //

            vmk_csdl1.sql_query = "insert into don_hang (id_tv, ngay_dh, thang_dh, nam_dh, thanh_tien)" +
                " output inserted.id_dh" +
                " values (@id_tv, @ngay_dh, @thang_dh, @nam_dh, (select sum(thanh_tien) from gio_hang where id_tv = @id_tv))"
            ;

            DataTable sql_param1 = vmk_csdl1.sql_param;
            sql_param1.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            sql_param1.Rows.Add("@ngay_dh", DateTime.Today.Day, SqlDbType.TinyInt);
            sql_param1.Rows.Add("@thang_dh", DateTime.Today.Month, SqlDbType.TinyInt);
            sql_param1.Rows.Add("@nam_dh", DateTime.Today.Year, SqlDbType.SmallInt);
            vmk_csdl1.sql_param = sql_param1;

            string id_dh = vmk_csdl1.VMK_SQL_INSERT_RETURN_OUTPUT();

            // CHUYỂN GIỎ HÀNG SANG ĐƠN HÀNG CHI TIẾT //

            ClassCSDL vmk_csdl2 = new ClassCSDL();
            vmk_csdl2.sql_query = "insert into don_hang_chi_tiet(id_dh, id_sp, don_gia, so_luong, id_dvt)" +
                " select @id_dh, id_sp, don_gia, so_luong, id_dvt from gio_hang where id_tv = @id_tv"
            ;

            DataTable sql_param2 = vmk_csdl2.sql_param;
            sql_param2.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            sql_param2.Rows.Add("@id_dh", id_dh, SqlDbType.Int);
            vmk_csdl2.sql_param = sql_param2;

            int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

            // XÓA GIỎ HÀNG //

            ClassCSDL vmk_csdl3 = new ClassCSDL();
            vmk_csdl3.sql_query = "delete from gio_hang where id_tv = @id_tv";

            DataTable sql_param3 = vmk_csdl3.sql_param;
            sql_param3.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            vmk_csdl3.sql_param = sql_param3;

            int sql_status3 = vmk_csdl3.VMK_SQL_INSERT_DELETE_UPDATE();

            Response.Redirect("DatHangThanhCong.aspx");
        }
    }
}
