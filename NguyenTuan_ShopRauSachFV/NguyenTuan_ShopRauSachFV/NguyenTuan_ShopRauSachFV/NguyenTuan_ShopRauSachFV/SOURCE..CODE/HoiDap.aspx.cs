using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text.RegularExpressions;

namespace RAU_SACH_THANH_TRUC
{
    public partial class HoiDap_TrangChu : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        public string Xu_Ly_Noi_Dung_Rut_Gon(object data, int max_length = 0)
        {
            string noi_dung = Convert.ToString(data);
            noi_dung = Regex.Replace(noi_dung, "<.*?>", string.Empty);
            if (max_length > 0 && noi_dung.Length > max_length) { noi_dung = noi_dung.Substring(0, max_length) + "....."; }
            return noi_dung;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "HỎI ĐÁP";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (!IsPostBack)
            {
                // LẤY ID CÂU HỎI TỪ BIẾN TRUYỀN TRONG URL //

                int id_hd = 0;

                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    bool check_id_hd = int.TryParse(Request.QueryString["id"].ToString(), out id_hd);
                    if (!check_id_hd) { id_hd = 0; }
                }

                // XỬ LÝ ID CÂU HỎI //
                
                string cau_lenh_sql = "";
                string sql_theo_thanh_vien = "";

                if (id_thanh_vien != "")
                {
                    sql_theo_thanh_vien = " and hoi_dap.id_tv = '" + id_thanh_vien + "' ";
                    panel_show_button_show_panel_guicauhoi.Visible = true;
                }
                else
                {
                    sql_theo_thanh_vien = " and chia_se = 1 ";
                }

                if (id_hd > 0)
                {
                    cau_lenh_sql = "select top(1) id_hd, hoi_dap.id_tv, ho_ten, tieu_de, noi_dung, ngay_hd, thang_hd, nam_hd, close_hd, chia_se," +
                        " (select count(*) from hoi_dap_tra_loi where id_hd=hoi_dap.id_hd) as so_cau_tra_loi" +
                        " from hoi_dap, thanh_vien" +
                        " where hoi_dap.id_tv = thanh_vien.id_tv" +
                        " and id_hd = @id_hd" +
                        sql_theo_thanh_vien +
                        " order by nam_hd desc, thang_hd desc, ngay_hd desc, id_hd desc"
                    ;

                    panel_ds_cauhoi.Visible = false;
                    panel_xem_cauhoi.Visible = true;
                }
                else
                {
                    cau_lenh_sql = "select id_hd, hoi_dap.id_tv, ho_ten, tieu_de, noi_dung, ngay_hd, thang_hd, nam_hd, close_hd, chia_se," +
                        " (select count(*) from hoi_dap_tra_loi where id_hd=hoi_dap.id_hd) as so_cau_tra_loi" +
                        " from hoi_dap, thanh_vien" +
                        " where hoi_dap.id_tv = thanh_vien.id_tv" +
                        sql_theo_thanh_vien +
                        " order by nam_hd desc, thang_hd desc, ngay_hd desc, id_hd desc"
                    ;

                    panel_ds_cauhoi.Visible = true;
                    panel_xem_cauhoi.Visible = false;
                }

                // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();

                vmk_csdl.sql_query = cau_lenh_sql;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_hd", id_hd, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0) { return; }

                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                if (id_hd > 0)
                {
                    id_cauhoi_for_traloi.Value = BANG_KQ.Rows[0][0].ToString();
                    string id_tv = BANG_KQ.Rows[0][1].ToString();
                    string ho_ten = BANG_KQ.Rows[0][2].ToString();
                    string tieu_de = BANG_KQ.Rows[0][3].ToString();
                    string noi_dung = BANG_KQ.Rows[0][4].ToString();

                    string ngay = BANG_KQ.Rows[0][5].ToString();
                    string thang = BANG_KQ.Rows[0][6].ToString();
                    string nam = BANG_KQ.Rows[0][7].ToString();

                    bool close_hd = Convert.ToBoolean(BANG_KQ.Rows[0][8]);
                    bool chia_se = Convert.ToBoolean(BANG_KQ.Rows[0][9]);

                    string so_cau_tra_loi = BANG_KQ.Rows[0][10].ToString();

                    label_tieude_cauhoi.Text = HTML_Encode(tieu_de);
                    label_noidung_cauhoi.Text = HTML_Encode(tieu_de,true);
                    label_hoten.Text = HTML_Encode(ho_ten);
                    label_ngayhoi.Text = Xu_Ly_Ngay_Thang_Nam(ngay,thang,nam);
                    label_socautraloi.Text = so_cau_tra_loi;

                    // LẤY DỮ LIỆU CÂU TRẢ LỜI //

                    System.Data.DataView vmk_dataview;

                    sql_datasource.SelectCommand = "select hoi_dap_tra_loi.*, ho_ten" +
                        " from hoi_dap_tra_loi, thanh_vien" +
                        " where hoi_dap_tra_loi.id_tv = thanh_vien.id_tv and hoi_dap_tra_loi.id_hd = @id_hd" +
                        " order by nam_traloi desc, thang_traloi desc, ngay_traloi desc, id_traloi desc"
                    ;

                    sql_datasource.SelectParameters.Clear();
                    sql_datasource.SelectParameters.Add("id_hd", DbType.String, id_hd.ToString());
                    vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                    if (vmk_dataview.Count != 0)
                    {
                        repeater_list_data_traloi.DataSource = sql_datasource;
                        repeater_list_data_traloi.DataBind();
                    }

                    // NẾU ĐÃ ĐĂNG NHẬP THÌ SẼ THẤY CHỨC NĂNG TRẢ LỜI //

                    if (id_thanh_vien != "" && id_tv == id_thanh_vien)
                    {
                        if (close_hd == false)
                        {
                            panel_guitraloi.Visible = true;
                            panel_thongbao_cauhoi_dadong.Visible = false;
                        }
                        else
                        {
                            panel_guitraloi.Visible = false;
                            panel_thongbao_cauhoi_dadong.Visible = true;
                        }
                        
                    }
                }
                else
                {
                    repeater_list_data_cauhoi.DataSource = BANG_KQ;
                    repeater_list_data_cauhoi.DataBind();
                }
            }
        }

        protected void btn_show_panel_guicauhoi_Click(object sender, EventArgs e)
        {
            panel_ds_cauhoi.Visible = false;
            panel_xem_cauhoi.Visible = false;
            panel_guicauhoi.Visible = true;
            txt_tieude.Focus();
        }

        protected void btn_guicauhoi_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string id_tv = id_thanh_vien;
            string tieu_de = txt_tieude.Text.Trim();
            string noi_dung = txt_noidung_cauhoi.Text.Trim();
            string ngay_hd = DateTime.Today.Day.ToString();
            string thang_hd = DateTime.Today.Month.ToString();
            string nam_hd = DateTime.Today.Year.ToString();
            bool close_hd = false;
            bool chia_se = checkbox_chiase.Checked;

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (id_tv == "" || tieu_de == "" || noi_dung == "" || ngay_hd == "" || thang_hd == "" || nam_hd == "")
            {
                label_thongbao_guicauhoi.Text = "*** BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU" + "<br/><br/>";
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();


            vmk_csdl.sql_query = "insert hoi_dap(id_tv,tieu_de,noi_dung,ngay_hd,thang_hd,nam_hd,close_hd,chia_se)" +
                " values (@id_tv,@tieu_de,@noi_dung,@ngay_hd,@thang_hd,@nam_hd,@close_hd,@chia_se)"
            ;

            DataTable sql_param = vmk_csdl.sql_param;
            sql_param.Rows.Add("@id_tv", id_tv, SqlDbType.Int);
            sql_param.Rows.Add("@tieu_de", tieu_de, SqlDbType.NVarChar);
            sql_param.Rows.Add("@noi_dung", noi_dung, SqlDbType.NVarChar);
            sql_param.Rows.Add("@ngay_hd", ngay_hd, SqlDbType.TinyInt);
            sql_param.Rows.Add("@thang_hd", thang_hd, SqlDbType.TinyInt);
            sql_param.Rows.Add("@nam_hd", nam_hd, SqlDbType.SmallInt);
            sql_param.Rows.Add("@close_hd", close_hd, SqlDbType.Bit);
            sql_param.Rows.Add("@chia_se", chia_se, SqlDbType.Bit);
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect("HoiDap.aspx");
        }

        protected void btn_guitraloi_Click(object sender, EventArgs e)
        {
            if (panel_thongbao_cauhoi_dadong.Visible == true)
            {
                label_thongbao_guitraloi.Text = "*** CÂU HỎI ĐÃ ĐÓNG. BẠN KHÔNG THỂ TRẢ LỜI" + "<br/><br/>";
                return;
            }

            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string id_tv = id_thanh_vien;
            string id_hd = id_cauhoi_for_traloi.Value;
            string noi_dung = txt_noidung_traloi.Text.Trim();
            string ngay_traloi = DateTime.Today.Day.ToString();
            string thang_traloi = DateTime.Today.Month.ToString();
            string nam_traloi = DateTime.Today.Year.ToString();

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (id_tv == "" || id_hd == "" || noi_dung == "" || ngay_traloi == "" || thang_traloi == "" || nam_traloi == "")
            {
                label_thongbao_guitraloi.Text = "*** BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU" + "<br/><br/>";
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query = "insert hoi_dap_tra_loi(id_hd,id_tv,noi_dung,ngay_traloi,thang_traloi,nam_traloi)" +
                " values (@id_hd,@id_tv,@noi_dung,@ngay_traloi,@thang_traloi,@nam_traloi)"
            ;

            DataTable sql_param = vmk_csdl.sql_param;
            sql_param.Rows.Add("@id_hd", id_hd, SqlDbType.Int);
            sql_param.Rows.Add("@id_tv", id_tv, SqlDbType.Int);
            sql_param.Rows.Add("@noi_dung", noi_dung, SqlDbType.NVarChar);
            sql_param.Rows.Add("@ngay_traloi", ngay_traloi, SqlDbType.TinyInt);
            sql_param.Rows.Add("@thang_traloi", thang_traloi, SqlDbType.TinyInt);
            sql_param.Rows.Add("@nam_traloi", nam_traloi, SqlDbType.SmallInt);
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM CÂU HỎI //

            Response.Redirect("HoiDap.aspx?id=" + id_hd);
        }
    }
}
