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
    public partial class DanhMuc : System.Web.UI.Page
    {
        string PageName = "DanhMuc.aspx";

        private bool Kiem_Tra_Quyen_Han()
        {
            string[] ds_quyen_cho_phep = { "Q002" };
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            if (ma_quyen == null || Array.IndexOf(ds_quyen_cho_phep, ma_quyen.ToUpper()) < 0) { return false; }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "QUẢN LÝ DANH MỤC";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            btn_khongluu.NavigateUrl = PageName;

            label_thongbao.Text = "";

            // KIỂM TRA QUYỀN HẠN //

            if (Kiem_Tra_Quyen_Han() == false) { Response.Redirect("Default.aspx"); return; }

            // BEGIN //

            if (!IsPostBack)
            {
                // TẠO DANH SÁCH DANH MỤC CHA //

                dropdownlist_list_danh_muc_cha.DataSource = sql_datasource_list_danh_muc_cha;
                dropdownlist_list_danh_muc_cha.DataValueField = "id_dm";
                dropdownlist_list_danh_muc_cha.DataTextField = "ten_dm";
                dropdownlist_list_danh_muc_cha.DataBind();
                if (dropdownlist_list_danh_muc_cha.Items.Count != 0) { dropdownlist_list_danh_muc_cha.SelectedIndex = 0; }
                
                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select id_dm, ten_dm, (select ten_dm from danh_muc where (id_dm = vmk_dm.id_dm_cha)) as ten_dm_cha, show_in_menu, stt from danh_muc vmk_dm order by id_dm_cha asc, stt asc";
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
                // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select id_dm " +
                    " from danh_muc " +
                    " where " +
                    " id_dm in (" + id_item.Value + ") " +
                    " and (id_dm in (select id_dm from san_pham))") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. DANH MỤC NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                    return;
                }

                // TIẾN HÀNH XÓA MỘT DỮ LIỆU TRONG CSDL //
                
                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "delete danh_muc where id_dm = @id_dm";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_dm", id_item.Value, SqlDbType.Int);
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
                vmk_csdl.sql_query = "select id_dm_cha, ten_dm, show_in_menu, stt from danh_muc where (id_dm = @id_dm)";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_dm", id_item.Value, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    // NẾU KHÔNG TÌM THẤY DỮ LIỆU CHUYỂN VỀ TRANG XEM //
                    Response.Redirect(PageName);
                    return;
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                dropdownlist_list_danh_muc_cha.SelectedValue = BANG_KQ.Rows[0][0].ToString();
                dropdownlist_list_danh_muc_cha.Items.Remove(dropdownlist_list_danh_muc_cha.Items.FindByValue(id_item.Value));

                txt_tendm.Text = BANG_KQ.Rows[0][1].ToString();

                bool show_in_menu = Convert.ToBoolean(BANG_KQ.Rows[0][2].ToString());
                chk_show_in_menu.Checked = show_in_menu;

                txt_stt_in_menu.Text = BANG_KQ.Rows[0][3].ToString();

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

            txt_tendm.Focus();
        }

        protected void btn_luu_Click(object sender, EventArgs e)
        {
            // LẤY DỮ LIỆU TỪ GIAO DIỆN //

            string tem_dm = txt_tendm.Text.Trim();

            int id_dm_cha = 0;
            string id_dm_cha_temp = dropdownlist_list_danh_muc_cha.SelectedItem.Value.ToString().Trim();
            bool check_id_dm_cha_temp = int.TryParse(id_dm_cha_temp, out id_dm_cha);

            bool show_in_menu = false;
            if (chk_show_in_menu.Checked == true) { show_in_menu = true; }

            int stt_in_menu = 0;
            string stt_in_menu_temp = txt_stt_in_menu.Text.Trim();
            bool check_stt_in_menu = int.TryParse(stt_in_menu_temp, out stt_in_menu);

            // KIỂM TRA DỮ LIỆU //

            if (tem_dm == "")
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU");
                txt_tendm.Focus();
                return;
            }

            // TIẾN HÀNH LƯU DỮ LIỆU VÀO CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();

            if (trang_thai.Value == "them")
            {
                // KIỂM TRA TÊN DANH MỤC TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select ten_dm " +
                    " from danh_muc " + 
                    " where ten_dm = '" + tem_dm + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("DANH MỤC NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // THÊM VÀO CSDL //

                vmk_csdl.sql_query = "insert into danh_muc(id_dm_cha, ten_dm, show_in_menu, stt) values(@id_dm_cha, @ten_dm, @show_in_menu, @stt)";

                DataTable sql_param = vmk_csdl.sql_param;
                if (id_dm_cha <= 0)
                {
                    sql_param.Rows.Add("@id_dm_cha", DBNull.Value, SqlDbType.Int);
                }
                else
                {
                    sql_param.Rows.Add("@id_dm_cha", id_dm_cha, SqlDbType.Int);
                }
                sql_param.Rows.Add("@ten_dm", tem_dm, SqlDbType.NVarChar);
                sql_param.Rows.Add("@show_in_menu", show_in_menu, SqlDbType.Bit);
                sql_param.Rows.Add("@stt", stt_in_menu, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
            }
            else if (trang_thai.Value == "sua")
            {
                // KIỂM TRA TÊN DANH MỤC TRONG CSDL //

                if (ClassMain.SQL_CHECK_EXISTS_V2("select ten_dm " + 
                    " from danh_muc " + 
                    " where ten_dm = '" + tem_dm + "' and id_dm != '" + id_item_for_edit.Value + "'") == true)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("DANH MỤC NÀY ĐƯỢC SỬ DỤNG RỒI");
                    return;
                }

                // TIẾN HÀNH CẬP NHẬT //

                vmk_csdl.sql_query = "update danh_muc set ten_dm = @ten_dm, id_dm_cha = @id_dm_cha, show_in_menu = @show_in_menu, stt = @stt where (id_dm = @id_dm)";

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_dm", id_item_for_edit.Value, SqlDbType.Int);
                sql_param.Rows.Add("@ten_dm", tem_dm, SqlDbType.NVarChar);
                if (id_dm_cha <= 0)
                {
                    sql_param.Rows.Add("@id_dm_cha", DBNull.Value, SqlDbType.Int);
                }
                else
                {
                    sql_param.Rows.Add("@id_dm_cha", id_dm_cha, SqlDbType.Int);
                }
                sql_param.Rows.Add("@show_in_menu", show_in_menu, SqlDbType.Bit);
                sql_param.Rows.Add("@stt", stt_in_menu, SqlDbType.Int);
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

            // KHÔNG CHO XÓA NẾU ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC //

            if (ClassMain.SQL_CHECK_EXISTS_V2("select id_dm "+
                " from danh_muc " + 
                " where " + 
                " id_dm in (" + String.Join(",", list_id_item.ToArray()) + ") " + 
                " and (id_dm in (select id_dm from san_pham))") == true)
            {
                label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ XÓA. DANH MỤC NÀY ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC");
                return;
            }

            // TIẾN HÀNH XÓA NHIỀU DỮ LIỆU TRONG CSDL //

            ClassCSDL vmk_csdl = new ClassCSDL();
            vmk_csdl.sql_query = "delete from danh_muc where id_dm in (" + String.Join(",", list_id_item.ToArray()) + ")";

            int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        public string XuLyShowInMenu(string status)
        {
            bool show_in_menu = Convert.ToBoolean(status);
            if (show_in_menu == true) { return "checked"; }
            return String.Empty;
        }
    }
}
