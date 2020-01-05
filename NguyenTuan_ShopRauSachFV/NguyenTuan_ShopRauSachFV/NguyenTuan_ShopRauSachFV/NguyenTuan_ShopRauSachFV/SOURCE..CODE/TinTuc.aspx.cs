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
    public partial class TinTuc_TrangChu : System.Web.UI.Page
    {
        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        public string Xu_Ly_Noi_Dung_Rut_Gon(object data, int max_length = 0)
        {
            string noi_dung = Convert.ToString(data);
            noi_dung = ClassMain.Decode_BBCode(HTML_Encode(noi_dung, true));
            noi_dung = Regex.Replace(noi_dung, "<.*?>", string.Empty);
            if (max_length > 0 && noi_dung.Length > max_length) { noi_dung = noi_dung.Substring(0, max_length) + "....."; }
            return noi_dung;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "THÔNG TIN";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            if (!IsPostBack)
            {
                // LẤY LOẠI TIN TỨC TỪ BIẾN TRUYỀN TRONG URL //

                string loai_tt = "";

                if (Request.QueryString["loai"] != null && Request.QueryString["loai"].ToString() != "")
                {
                    loai_tt = Request.QueryString["loai"].ToString().ToLower();
                }

                // LẤY ID TIN TỨC TỪ BIẾN TRUYỀN TRONG URL //

                int id_tt = 0;

                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    bool check_id_tt = int.TryParse(Request.QueryString["id"].ToString(),out id_tt);
                    if (!check_id_tt) { id_tt = 0; }
                }

                // XỬ LÝ LOẠI TIN TỨC & ID TIN TỨC //

                string cau_lenh_sql = "select * from tin_tuc";

                if (id_tt > 0)
                {
                    cau_lenh_sql = "select top(1) id_tt, tieu_de, noi_dung, luot_xem, ngay_tt, thang_tt, nam_tt, ho_ten" + 
                        " from tin_tuc, thanh_vien" + 
                        " where tin_tuc.id_tv = thanh_vien.id_tv and id_tt = @id_tt and luu_nhap = 0" +
                        " order by nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
                    ;
                }
                else
                {
                    if (loai_tt == "tintuc")
                    {
                        cau_lenh_sql = "select id_tt, tieu_de, noi_dung, luot_xem, ngay_tt, thang_tt, nam_tt, ho_ten" +
                            " from tin_tuc, thanh_vien" +
                            " where tin_tuc.id_tv = thanh_vien.id_tv and ky_thuat = 0 and luu_nhap = 0" +
                            " order by nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
                        ;

                        this.Page.Title = "Tin Tức - " + this.Page.Title;
                        label_tenloai.Text = "<a href='TinTuc.aspx?loai=TinTuc' style='color: #4285F4'>TIN TỨC</a>";
                    }
                    else if (loai_tt == "kythuat")
                    {
                        cau_lenh_sql = "select id_tt, tieu_de, noi_dung, luot_xem, ngay_tt, thang_tt, nam_tt, ho_ten" +
                            " from tin_tuc, thanh_vien" +
                            " where tin_tuc.id_tv = thanh_vien.id_tv and ky_thuat = 1 and luu_nhap = 0" +
                            " order by nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
                        ;

                        this.Page.Title = "Tin Kỹ Thuật - " + this.Page.Title;
                        label_tenloai.Text = "<a href='TinTuc.aspx?loai=KyThuat' style='color: #4285F4'>TIN KỸ THUẬT</a>";
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }

                // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();

                vmk_csdl.sql_query = cau_lenh_sql;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_tt", id_tt, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    return;
                }

                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                if (id_tt > 0)
                {
                    txt_id_tt.Value = HTML_Encode(BANG_KQ.Rows[0][0].ToString());
                    label_tieudebaiviet.Text = HTML_Encode(BANG_KQ.Rows[0][1].ToString());
                    label_noidungbaiviet.Text = ClassMain.Decode_BBCode(HTML_Encode(BANG_KQ.Rows[0][2], true));
                    Int64 luot_xem = Convert.ToInt64(BANG_KQ.Rows[0][3]);

                    string ngay = BANG_KQ.Rows[0][4].ToString();
                    string thang = BANG_KQ.Rows[0][5].ToString();
                    string nam = BANG_KQ.Rows[0][6].ToString();

                    string ho_ten = BANG_KQ.Rows[0][7].ToString();

                    label_ngaythangnam.Text = Xu_Ly_Ngay_Thang_Nam(ngay,thang,nam);
                    label_nguoidangtin.Text = ho_ten;
                    label_solanxem.Text = luot_xem.ToString();

                    // CẬP NHẬT LƯỢT XEM //

                    luot_xem += 1;

                    ClassCSDL vmk_csdl1 = new ClassCSDL();
                    vmk_csdl1.sql_query = "update tin_tuc set luot_xem=@luot_xem where id_tt=@id_tt";

                    DataTable sql_param1 = vmk_csdl1.sql_param;
                    sql_param1.Rows.Add("@luot_xem", luot_xem, SqlDbType.BigInt);
                    sql_param1.Rows.Add("@id_tt", id_tt, SqlDbType.Int);
                    vmk_csdl1.sql_param = sql_param1;

                    int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();
                }
                else
                {
                    panel_nhieu_tintuc.Visible = true;
                    panel_mot_tintuc.Visible = false;

                    repeater_list_data.DataSource = BANG_KQ;
                    repeater_list_data.DataBind();
                }
            }
        }
    }
}
