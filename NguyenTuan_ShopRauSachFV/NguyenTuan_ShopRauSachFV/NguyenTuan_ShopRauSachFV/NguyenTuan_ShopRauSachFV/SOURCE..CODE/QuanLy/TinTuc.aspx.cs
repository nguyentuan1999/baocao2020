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
    public partial class TinTuc : System.Web.UI.Page
    {
        string id_thanh_vien = "";

        string PageName = "TinTuc.aspx";

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

            string vmk_title_page = "QUẢN LÝ TIN TỨC & KỸ THUẬT";

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

            // BEGIN //

            if (!IsPostBack)
            {
                // TẠO NGÀY THÁNG NĂM //

                for (int i = 1; i < 32; i++)
                {
                    txt_ngaytt.Items.Add(i.ToString());
                    if (i < 13) { txt_thangtt.Items.Add(i.ToString()); }
                }
                for (int i = DateTime.Today.Year - 10; i < DateTime.Today.Year + 10; i++)
                {
                    txt_namtt.Items.Add(i.ToString());
                }

                // CHỌN NGÀY, THÁNG, NĂM MẶC ĐỊNH //

                txt_ngaytt.Items.FindByValue((DateTime.Today.Day).ToString()).Selected = true;
                txt_thangtt.Items.FindByValue((DateTime.Today.Month).ToString()).Selected = true;
                txt_namtt.Items.FindByValue((DateTime.Today.Year).ToString()).Selected = true;

                // LẤY DỮ LIỆU TỪ CSDL //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select tin_tuc.*,ho_ten from tin_tuc, thanh_vien" +
                    " where tin_tuc.id_tv = thanh_vien.id_tv" +
                    " order by luu_nhap desc, nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
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
                // TIẾN HÀNH XÓA MỘT DỮ LIỆU TRONG CSDL //
                
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete tin_tuc where id_tt = @id_tt";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tt", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }

            if (e.CommandName == "sua")
            {
                panel_them_sua.Visible = true;
                panel_xem.Visible = false;

                btn_them.Visible = false;
                btn_xoa.Visible = false;

                btn_luu.Visible = true;
                btn_khongluu.Visible = true;

                trang_thai.Value = "sua";
                id_item_for_edit.Value = id_item.Value;

                // LẤY DỮ LIỆU TRONG CSDL THEO ID //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select id_tv,tieu_de,noi_dung,ngay_tt,thang_tt,nam_tt,ky_thuat,luu_nhap" +
                    " from tin_tuc where id_tt = @id_tt"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tt", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    // NẾU KHÔNG TÌM THẤY DỮ LIỆU CHUYỂN VỀ TRANG XEM //
                    Response.Redirect(PageName);
                    return;
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                id_tv_for_edit.Value = BANG_KQ.Rows[0][0].ToString();
                txt_tieude.Text = BANG_KQ.Rows[0][1].ToString();
                txt_noidung.Text = BANG_KQ.Rows[0][2].ToString();
                txt_ngaytt.Text = BANG_KQ.Rows[0][3].ToString();
                txt_thangtt.Text = BANG_KQ.Rows[0][4].ToString();
                txt_namtt.Text = BANG_KQ.Rows[0][5].ToString();
                checkbox_tinkythuat.Checked = Convert.ToBoolean(BANG_KQ.Rows[0][6]);
                checkbox_luunhap.Checked = Convert.ToBoolean(BANG_KQ.Rows[0][7]);

                return;
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
            string ngay_tt = txt_ngaytt.Text.Trim();
            string thang_tt = txt_thangtt.Text.Trim();
            string nam_tt = txt_namtt.Text.Trim();

            bool ky_thuat = checkbox_tinkythuat.Checked;
            bool luu_nhap = checkbox_luunhap.Checked;

            if (id_tv =="" || tieu_de == "" || noi_dung == "" ||
                ngay_tt =="" || thang_tt == "" || nam_tt == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                return;
            }
            
            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            if (trang_thai.Value == "them")
            {
                vmk_csdl.sql_query = "insert into tin_tuc(id_tv,tieu_de,noi_dung,ngay_tt,thang_tt,nam_tt,ky_thuat,luu_nhap)" +
                    "values(@id_tv,@tieu_de,@noi_dung,@ngay_tt,@thang_tt,@nam_tt,@ky_thuat,@luu_nhap)"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tv", id_tv, SqlDbType.Int);
                sql_param.Rows.Add("@tieu_de", tieu_de, SqlDbType.NVarChar);
                sql_param.Rows.Add("@noi_dung", noi_dung, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_tt", ngay_tt, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_tt", thang_tt, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_tt", nam_tt, SqlDbType.SmallInt);
                sql_param.Rows.Add("@ky_thuat", ky_thuat, SqlDbType.Bit);
                sql_param.Rows.Add("@luu_nhap", luu_nhap, SqlDbType.Bit);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }
            else if (trang_thai.Value == "sua")
            {
                vmk_csdl.sql_query = "update tin_tuc" +
                    " set id_tv=@id_tv, tieu_de=@tieu_de, noi_dung=@noi_dung," +
                    " ngay_tt=@ngay_tt, thang_tt=@thang_tt, nam_tt=@nam_tt, ky_thuat=@ky_thuat, luu_nhap=@luu_nhap" +
                    " where id_tt = @id_tt"
                ;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tt", id_item_for_edit.Value, SqlDbType.Int);
                sql_param.Rows.Add("@id_tv", id_tv_for_edit.Value, SqlDbType.Int);
                sql_param.Rows.Add("@tieu_de", tieu_de, SqlDbType.NVarChar);
                sql_param.Rows.Add("@noi_dung", noi_dung, SqlDbType.NVarChar);
                sql_param.Rows.Add("@ngay_tt", ngay_tt, SqlDbType.TinyInt);
                sql_param.Rows.Add("@thang_tt", thang_tt, SqlDbType.TinyInt);
                sql_param.Rows.Add("@nam_tt", nam_tt, SqlDbType.SmallInt);
                sql_param.Rows.Add("@ky_thuat", ky_thuat, SqlDbType.Bit);
                sql_param.Rows.Add("@luu_nhap", luu_nhap, SqlDbType.Bit);
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

            // TIẾN HÀNH XÓA NHIỀU DỮ LIỆU TRONG CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();
            vmk_csdl.sql_query = "delete from tin_tuc where id_tt in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }
    }
}
