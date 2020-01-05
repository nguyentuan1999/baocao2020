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
    public partial class HoiDapTraLoi : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        string PageName1 = "HoiDap.aspx";
        string PageName2 = "HoiDapTraLoi.aspx";

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q002" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "TRẢ LỜI CÂU HỎI";

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

            // LẤY ID THÀNH VIÊN //

            id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien == null) { id_thanh_vien = ""; }

            if (id_thanh_vien == "")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // BEGIN //

            if (!IsPostBack)
            {
                // LẤY ID CÂU HỎI TỪ BIẾN TRUYỀN TRONG URL //

                int id_hd = 0;
                bool check_number = false;

                if (Request.QueryString["cauhoi"] != null)
                {
                    check_number = int.TryParse(Request.QueryString["cauhoi"].ToString(), out id_hd);
                }
                
                if (!check_number || id_hd < 1)
                {
                    Response.Redirect(PageName1);
                    return;
                }

                id_cauhoi.Value = id_hd.ToString();

                // LẤY DỮ LIỆU TỪ CSDL THEO ID CÂU HỎI //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select top(1) hoi_dap.tieu_de, hoi_dap.noi_dung, hoi_dap.ngay_hd, hoi_dap.thang_hd, hoi_dap.nam_hd, hoi_dap.close_hd, ho_ten," +
                    " (select count(*) from hoi_dap_tra_loi where hoi_dap_tra_loi.id_hd = hoi_dap.id_hd) as so_cau_tra_loi" +
                    " from hoi_dap, thanh_vien" +
                    " where hoi_dap.id_tv = thanh_vien.id_tv and hoi_dap.id_hd = @id_hd" +
                    " order by close_hd asc, nam_hd desc, thang_hd desc, ngay_hd desc"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_hd", id_hd, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("CHƯA CÓ DỮ LIỆU", "", false);
                    Response.Redirect(PageName1);
                    return;
                }

                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                label_tieude.Text = HTML_Encode(BANG_KQ.Rows[0][0].ToString());
                label_noidung.Text = HTML_Encode(BANG_KQ.Rows[0][1].ToString(), true);

                string ngay = BANG_KQ.Rows[0][2].ToString();
                string thang = BANG_KQ.Rows[0][3].ToString();
                string nam = BANG_KQ.Rows[0][4].ToString();

                label_ngaythangnam.Text = Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam);

                bool closed_hd = Convert.ToBoolean(BANG_KQ.Rows[0][5]);
                if (closed_hd == true)
                {
                    closed_cauhoi.Value = "1";
                    label_closed.Visible = true;
                    panel_traloi.Visible = false;
                }
                label_hoten.Text = HTML_Encode(BANG_KQ.Rows[0][6].ToString());
                label_so_cau_tra_loi.Text = BANG_KQ.Rows[0][7].ToString();

                // LẤY DỮ LIỆU CÂU TRẢ LỜI //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select hoi_dap_tra_loi.*, ho_ten" +
                    " from hoi_dap_tra_loi, thanh_vien" +
                    " where hoi_dap_tra_loi.id_tv = thanh_vien.id_tv and hoi_dap_tra_loi.id_hd = @id_hd"+
                    " order by nam_traloi desc, thang_traloi desc, ngay_traloi desc, id_traloi desc"
                ;

                sql_datasource.SelectParameters.Clear();
                sql_datasource.SelectParameters.Add("id_hd", DbType.String, id_hd.ToString());
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    repeater_list_traloi_data.DataSource = sql_datasource;
                    repeater_list_traloi_data.DataBind();
                }
            }
        }
        
        protected void repeater_list_traloi_data_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button btn_xoa_traloi = (Button)e.Item.FindControl("btn_xoa_traloi");
            if (closed_cauhoi.Value == "1")
            {
                btn_xoa_traloi.Visible = false;
            }
        }

        protected void repeater_list_traloi_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_cautraloi = (HiddenField)e.Item.FindControl("id_cautraloi");

            if (e.CommandName == "xoa_traloi")
            {
                // KHÔNG CHO XÓA CÂU TRẢ LỜI NẾU CÂU HỎI ĐÃ ĐÓNG //

                if (closed_cauhoi.Value == "1")
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("CÂU HỎI ĐÃ ĐÓNG, KHÔNG THỂ XÓA CÂU TRẢ LỜI");
                    return;
                }

                // XÓA CÂU TRẢ LỜI ĐƯỢC CHỌN //

                ClassCSDL vmk_csdl1 = new ClassCSDL();
                vmk_csdl1.sql_query = "delete hoi_dap_tra_loi where id_traloi = @id_traloi";
                DataTable sql_param1 = vmk_csdl1.sql_param;
                sql_param1.Rows.Add("@id_traloi", id_cautraloi.Value, SqlDbType.Int);
                vmk_csdl1.sql_param = sql_param1;

                int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();

                // CHUYỂN VỀ TRẠNG THÁI XEM //

                Response.Redirect(PageName2 + "?cauhoi=" + id_cauhoi.Value);
            }
        }

        protected void btn_traloi_Click(object sender, EventArgs e)
        {
            // KHÔNG CHO TRẢ LỜI NẾU CÂU HỎI ĐÃ ĐÓNG //

            if (closed_cauhoi.Value == "1")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("CÂU HỎI ĐÃ ĐÓNG, KHÔNG THỂ TRẢ LỜI");
                return;
            }

            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string id_hd, noi_dung, ngay_traloi, thang_traloi, nam_traloi;

            id_hd = id_cauhoi.Value.Trim();
            noi_dung = txt_noidung_traloi.Text.Trim();
            ngay_traloi = DateTime.Today.Day.ToString();
            thang_traloi = DateTime.Today.Month.ToString();
            nam_traloi = DateTime.Today.Year.ToString();

            if (id_hd == "" || noi_dung == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            vmk_csdl.sql_query = "insert hoi_dap_tra_loi(id_hd,id_tv,noi_dung,ngay_traloi,thang_traloi,nam_traloi)" +
                " values (@id_hd,@id_tv,@noi_dung,@ngay_traloi,@thang_traloi,@nam_traloi)"
            ;

            DataTable sql_param = vmk_csdl.sql_param;
            sql_param.Rows.Add("@id_hd", id_hd, SqlDbType.Int);
            sql_param.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
            sql_param.Rows.Add("@noi_dung", noi_dung, SqlDbType.NVarChar);
            sql_param.Rows.Add("@ngay_traloi", ngay_traloi, SqlDbType.TinyInt);
            sql_param.Rows.Add("@thang_traloi", thang_traloi, SqlDbType.TinyInt);
            sql_param.Rows.Add("@nam_traloi", nam_traloi, SqlDbType.SmallInt);
            vmk_csdl.sql_param = sql_param;

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName2 + "?cauhoi=" + id_hd);
        }
    }
}
