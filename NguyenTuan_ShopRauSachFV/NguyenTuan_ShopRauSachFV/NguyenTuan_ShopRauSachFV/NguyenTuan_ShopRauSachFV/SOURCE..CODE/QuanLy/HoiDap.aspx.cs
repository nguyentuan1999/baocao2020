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
    public partial class HoiDap : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        Int16 page_num_current = 1;
        Int16 page_num_menu = 5;
        Int16 row_per_page = 5;

        string PageName = "HoiDap.aspx";

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q002" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string Xu_Ly_Checked(object data)
        {
            bool status = Convert.ToBoolean(data);
            if (status == true) { return "checked"; }
            return "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "QUẢN LÝ HỎI ĐÁP";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            btn_khongluu.NavigateUrl = PageName;

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

            // PHÂN TRANG //

            if (row_per_page < 1)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("CẤU HÌNH ROW_PER_PAGE KHÔNG ĐƯỢC < 1", "", false);
                return;
            }
            if (page_num_current < 1)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("CẤU HÌNH PAGE_NUM_CURRENT KHÔNG ĐƯỢC < 1", "", false);
                return;
            }
            if (page_num_menu < 1)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("CẤU HÌNH PAGE_NUM_MENU KHÔNG ĐƯỢC < 1", "", false);
                return;
            }

            // LẤY PAGE CURRENT TỪ URL //

            bool check_number = false;
            if (Request.QueryString["page"] != null)
            {
                check_number = Int16.TryParse(Request.QueryString["page"].ToString(), out page_num_current);
            }
            if (!check_number || page_num_current < 1) { page_num_current = 1; }

            // BEGIN //

            if (!IsPostBack)
            {
                // TẠO NGÀY THÁNG NĂM //

                for (int i = 1; i < 32; i++)
                {
                    txt_ngayhd.Items.Add(i.ToString());
                    if (i < 13) { txt_thanghd.Items.Add(i.ToString()); }
                }
                for (int i = DateTime.Today.Year - 10; i < DateTime.Today.Year + 10; i++)
                {
                    txt_namhd.Items.Add(i.ToString());
                }

                // CHỌN NGÀY, THÁNG, NĂM MẶC ĐỊNH //

                txt_ngayhd.Items.FindByValue((DateTime.Today.Day).ToString()).Selected = true;
                txt_thanghd.Items.FindByValue((DateTime.Today.Month).ToString()).Selected = true;
                txt_namhd.Items.FindByValue((DateTime.Today.Year).ToString()).Selected = true;

                // LẤY DỮ LIỆU TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select hoi_dap.*, ho_ten," +
                    " (select count(*) from hoi_dap_tra_loi where hoi_dap_tra_loi.id_hd = hoi_dap.id_hd) as so_cau_tra_loi" +
                    " from hoi_dap, thanh_vien" +
                    " where hoi_dap.id_tv = thanh_vien.id_tv" +
                    " order by close_hd asc, nam_hd desc, thang_hd desc, ngay_hd desc, id_hd desc"
                ;

                sql_datasource.SelectParameters.Clear();
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    repeater_list_data.DataSource = sql_datasource;
                    repeater_list_data.DataBind();
                }
                else
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("CHƯA CÓ DỮ LIỆU","",false);
                    return;
                }
            }
        }

        protected void repeater_list_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_item = (HiddenField)e.Item.FindControl("id_item");

            if (e.CommandName == "xoa")
            {
                // XÓA TẤT CẢ CÂU TRẢ LỜI CỦA CÂU HỎI TRƯỚC //

                ClassCSDL vmk_csdl1 = new ClassCSDL();
                vmk_csdl1.sql_query = "delete hoi_dap_tra_loi where id_hd = @id_hd";
                DataTable sql_param1 = vmk_csdl1.sql_param;
                sql_param1.Rows.Add("@id_hd", id_item.Value, SqlDbType.Int);
                vmk_csdl1.sql_param = sql_param1;

                int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();

                // XÓA CÂU HỎI SAU //

                ClassCSDL vmk_csdl2 = new ClassCSDL();
                vmk_csdl2.sql_query = "delete hoi_dap where id_hd = @id_hd";
                DataTable sql_param2 = vmk_csdl2.sql_param;
                sql_param2.Rows.Add("@id_hd", id_item.Value, SqlDbType.Int);
                vmk_csdl2.sql_param = sql_param2;

                int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "luu")
            {
                CheckBox checkbox_khoa = (CheckBox)e.Item.FindControl("checkbox_khoa");
                CheckBox checkbox_chiase = (CheckBox)e.Item.FindControl("checkbox_chiase");

                bool close_hd = checkbox_khoa.Checked;
                bool chia_se = checkbox_chiase.Checked;

                ClassCSDL vmk_csdl = new ClassCSDL();

                vmk_csdl.sql_query = "update hoi_dap set close_hd=@close_hd, chia_se=@chia_se  where id_hd=@id_hd";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_hd", id_item.Value, SqlDbType.Int);
                sql_param.Rows.Add("@close_hd", close_hd, SqlDbType.Bit);
                sql_param.Rows.Add("@chia_se", chia_se, SqlDbType.Bit);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_them_Click(object sender, EventArgs e)
        {
            panel_them_sua.Visible = true;
            panel_xem.Visible = false;

            btn_them.Visible = false;
            btn_xoa.Visible = false;

            btn_luu.Visible = true;
            btn_khongluu.Visible = true;

            trang_thai.Value = "them";

            txt_tieude.Focus();
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // KIỂM TRA DỮ LIỆU NHẬP VÀO //

            string id_tv = id_thanh_vien;
            string tieu_de = txt_tieude.Text.Trim();
            string noi_dung = txt_noidung.Text.Trim();
            string ngay_hd = txt_ngayhd.Text.Trim();
            string thang_hd = txt_thanghd.Text.Trim();
            string nam_hd = txt_namhd.Text.Trim();
            bool close_hd = checkbox_khoa.Checked;
            bool chia_se = checkbox_chiase.Checked;

            // KIỂM TRA DỮ LIỆU NHẬP //

            if (id_tv == "" || tieu_de == "" || noi_dung == "" || ngay_hd == "" || thang_hd == "" || nam_hd == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            if (trang_thai.Value == "them")
            {
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
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_xoa_Click(object sender, EventArgs e)
        {
            int number;
            int selected_id_item_count = 0;
            ArrayList list_id_item = new ArrayList();

            foreach (RepeaterItem i in repeater_list_data.Items)
            {
                CheckBox cb = (CheckBox)i.FindControl("checkbox_item_select");
                if (cb.Checked)
                {
                    HiddenField id_item = (HiddenField)i.FindControl("id_item");

                    bool check_number = int.TryParse(id_item.Value, out number);
                    if (check_number == true && number > 0)
                    {
                        list_id_item.Add(number.ToString());
                        selected_id_item_count += 1;
                    }
                }
            }

            // KIỂM TRA DỮ LIỆU CÓ ĐƯỢC CHỌN KHÔNG //

            if (selected_id_item_count == 0)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA CHỌN ĐỐI TƯỢNG NÀO");
                return;
            }

            // XÓA CÂU TRẢ LỜI CỦA CÂU HỎI TRƯỚC //

            ClassCSDL vmk_csdl1 = new ClassCSDL();
            vmk_csdl1.sql_query = "delete from hoi_dap_tra_loi where id_hd in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();

            // XÓA CÂU HỎI SAU //

            ClassCSDL vmk_csdl2 = new ClassCSDL();
            vmk_csdl2.sql_query = "delete from hoi_dap where id_hd in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status2 = vmk_csdl2.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }
    }
}
