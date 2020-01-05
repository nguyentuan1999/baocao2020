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
    public partial class SanPham_TrangChu : System.Web.UI.Page
    {
        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        public string Xu_Ly_Money(object money_obj)
        {
            string money_truockhixuly = Convert.ToString(money_obj);
            string money_saukhixuly = "0";
            Int64 money = 0;
            bool check_money = Int64.TryParse(money_truockhixuly, out money);
            if (check_money == true) { money_saukhixuly = String.Format("{0:#,##}", money); }
            return money_saukhixuly;
        }

        public string Xu_Ly_Tach_Img_From_Noi_Dung(object data)
        {
            string data_source = Convert.ToString(data);
            string url_of_img = String.Empty;
            try
            {
                Regex regExp = new Regex(@"\[img(.*?)\](.+?)\[/img\]", RegexOptions.IgnoreCase);
                Match regExp_Url_Img = regExp.Match(data_source);
                url_of_img = regExp_Url_Img.Groups[2].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                url_of_img = String.Empty;
            }
            return url_of_img;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "SẢN PHẨM";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            if (!IsPostBack)
            {
                // LẤY ID DANH MỤC TỪ BIẾN TRUYỀN TRONG URL //

                int id_dm = 0;

                if (Request.QueryString["iddm"] != null && Request.QueryString["iddm"].ToString() != "")
                {
                    bool check_id_dm = int.TryParse(Request.QueryString["iddm"].ToString(), out id_dm);
                    if (!check_id_dm) { id_dm = 0; }
                }

                // LẤY ID SẢN PHẨM TỪ BIẾN TRUYỀN TRONG URL //

                int id_sp = 0;

                if (Request.QueryString["idsp"] != null && Request.QueryString["idsp"].ToString() != "")
                {
                    bool check_id_sp = int.TryParse(Request.QueryString["idsp"].ToString(), out id_sp);
                    if (!check_id_sp) { id_sp = 0; }
                }

                // XỬ LÝ DANH MỤC & ID SẢN PHẨM //

                string cau_lenh_sql = "select * from san_pham";

                if (id_sp > 0)
                {
                    cau_lenh_sql = "select top(1) id_sp, san_pham.id_dm, ten_dm, ten_sp, gioi_thieu, ngay_sp, thang_sp, nam_sp, don_gia, luot_xem, (select ten_dvt from don_vi_tinh where id_dvt = san_pham.id_dvt) as ten_dvt" + 
                        " from san_pham, danh_muc" +
                        " where san_pham.id_dm = danh_muc.id_dm and id_sp = @id_sp" +
                        " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                    ;
                }
                else
                {
                    if (id_dm > 0)
                    {
                        cau_lenh_sql = "select id_sp, san_pham.id_dm, ten_dm, ten_sp, gioi_thieu, ngay_sp, thang_sp, nam_sp, don_gia, luot_xem, (select ten_dvt from don_vi_tinh where id_dvt = san_pham.id_dvt) as ten_dvt" +
                            " from san_pham, danh_muc" +
                            " where (san_pham.id_dm = danh_muc.id_dm) and (san_pham.id_dm in (select id_dm from danh_muc where (id_dm = @id_dm) or (id_dm_cha = @id_dm)))" +
                            " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                        ;
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }

                    panel_nhieu_sanpham.Visible = true;
                    panel_mot_sanpham.Visible = false;
                }

                // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();

                vmk_csdl.sql_query = cau_lenh_sql;

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@id_sp", id_sp, SqlDbType.Int);
                sql_param.Rows.Add("@id_dm", id_dm, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    // KHÔNG CÓ DỮ LIỆU
                    return;
                }

                // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                if (id_sp != 0)
                {
                    hplink_themvaogiohang.NavigateUrl = hplink_themvaogiohang.NavigateUrl + BANG_KQ.Rows[0][0].ToString();
                    hplink_danh_muc.NavigateUrl = hplink_danh_muc.NavigateUrl + BANG_KQ.Rows[0][1].ToString();

                    label_tendanhmuc.Text = HTML_Encode(BANG_KQ.Rows[0][2].ToString());
                    label_tensp.Text = HTML_Encode(BANG_KQ.Rows[0][3].ToString());
                    label_gioithieu.Text = ClassMain.Decode_BBCode(HTML_Encode(BANG_KQ.Rows[0][4], true));

                    string ngay = BANG_KQ.Rows[0][5].ToString();
                    string thang = BANG_KQ.Rows[0][6].ToString();
                    string nam = BANG_KQ.Rows[0][7].ToString();

                    string don_gia = String.Format("{0:#,## vnđ}", BANG_KQ.Rows[0][8]);
                    label_dongia.Text = don_gia;

                    label_ngaythangnam.Text = Xu_Ly_Ngay_Thang_Nam(ngay,thang,nam);

                    Int64 luot_xem = Convert.ToInt64(BANG_KQ.Rows[0][9]);
                    label_solanxem.Text = luot_xem.ToString();

                    label_dvt.Text = HTML_Encode(BANG_KQ.Rows[0][10].ToString());

                    // CẬP NHẬT LƯỢT XEM //

                    luot_xem += 1;

                    ClassCSDL vmk_csdl1 = new ClassCSDL();
                    vmk_csdl1.sql_query = "update san_pham set luot_xem=@luot_xem where id_sp=@id_sp";

                    DataTable sql_param1 = vmk_csdl1.sql_param;
                    sql_param1.Rows.Add("@luot_xem", luot_xem, SqlDbType.BigInt);
                    sql_param1.Rows.Add("@id_sp", id_sp, SqlDbType.Int);
                    vmk_csdl1.sql_param = sql_param1;

                    int sql_status1 = vmk_csdl1.VMK_SQL_INSERT_DELETE_UPDATE();
                }
                else
                {
                    label_tendm.Text = HTML_Encode(BANG_KQ.Rows[0][2].ToString());
                    this.Page.Title = label_tendm.Text + " - " + this.Page.Title;

                    repeater_list_data.DataSource = BANG_KQ;
                    repeater_list_data.DataBind();
                }
            }
        }
    }
}
