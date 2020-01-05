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
    public partial class TimKiem_TrangChu : System.Web.UI.Page
    {
        public string Xu_Ly_Ngay_Thang_Nam(object ngay, object thang, object nam) { return ClassMain.Xu_Ly_Ngay_Thang_Nam(ngay, thang, nam); }

        public string HTML_Encode(object data, bool replace_newline = false) { return ClassMain.HTML_Encode(data, replace_newline); }

        public string Xu_Ly_Demo(object data, int max_length = 0)
        {
            string noi_dung = Convert.ToString(data);
            noi_dung = Regex.Replace(noi_dung, @"\[img\](.*?)\[/img\]", string.Empty);
            noi_dung = Regex.Replace(noi_dung, @"\[url\](.*?)\[/url\]", string.Empty);
            noi_dung = Regex.Replace(noi_dung, @"\[(.*?)\]", string.Empty);
            noi_dung = Regex.Replace(noi_dung, "<.*?>", string.Empty);
            if (max_length > 0 && noi_dung.Length > max_length)
            {
                noi_dung = noi_dung.Substring(0, max_length) + ".....";
            }
            return noi_dung;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "TÌM KIẾM";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));

            ////

            label_thongbao.Text = "";

            if (!IsPostBack)
            {
                // LẤY KIỂU TÌM KIẾM & TỪ KHÓA TỪ BIẾN TRUYỀN TRONG URL //

                string kieu = "";
                string tu_khoa = "";

                if (Request.QueryString["kieu"] != null && Request.QueryString["kieu"].ToString() != "" && Request.QueryString["tu_khoa"] != null && Request.QueryString["tu_khoa"].ToString() != "")
                {
                    kieu = Request.QueryString["kieu"].ToString().Trim();
                    tu_khoa = Request.QueryString["tu_khoa"].ToString().Trim();
                }

                repeater_list_data.DataSource = null;
                repeater_list_data.DataBind();

                // KIỂM TRA DỮ LIỆU NHẬP //

                if (kieu == "" || tu_khoa == "")
                {
                    label_thongbao.Text = "*** BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU TÌM KIẾM";
                    return;
                }

                keyword.Text = tu_khoa.Replace("'", "\\'");

                // TRUY XUẤT DỮ LIỆU TỪ CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();

                string id_dm_dich_vu = "1";
                string sql_query_show_link = "";

                switch (kieu)
                {
                    case "dich_vu":
                        sql_query_show_link = "(select ('SanPham.aspx?idsp=' + convert(varchar(100),san_pham.id_sp))) as link";
                        vmk_csdl.sql_query = "select *," + sql_query_show_link + ", san_pham.ten_sp as tieu_de, san_pham.gioi_thieu as noi_dung," +
                            " san_pham.ngay_sp as ngay_dang, san_pham.thang_sp as thang_dang, san_pham.nam_sp as nam_dang" +
                            " from san_pham" +
                            " where" +
                            " id_dm = @id_dm_dich_vu" +
                            " and (charindex(@key_word, ten_sp) > 0 or charindex(@key_word, gioi_thieu) > 0)" +
                            " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                        ;
                        break;
                    case "san_pham":
                        sql_query_show_link = "(select ('SanPham.aspx?idsp=' + convert(varchar(100),san_pham.id_sp))) as link";
                        vmk_csdl.sql_query = "select *," + sql_query_show_link + ", san_pham.ten_sp as tieu_de, san_pham.gioi_thieu as noi_dung," +
                            " san_pham.ngay_sp as ngay_dang, san_pham.thang_sp as thang_dang, san_pham.nam_sp as nam_dang" +
                            " from san_pham" +
                            " where" +
                            " id_dm != @id_dm_dich_vu" +
                            " and (charindex(@key_word, ten_sp) > 0 or charindex(@key_word, gioi_thieu) > 0)" +
                            " order by nam_sp desc, thang_sp desc, ngay_sp desc, id_sp desc"
                        ;
                        break;
                    case "tin_tuc":
                        sql_query_show_link = "(select ('TinTuc.aspx?id=' + convert(varchar(100),tin_tuc.id_tt))) as link,";
                        vmk_csdl.sql_query = "select *," + sql_query_show_link +
                            " ngay_tt as ngay_dang, thang_tt as thang_dang, nam_tt as nam_dang" +
                            " from tin_tuc" +
                            " where" +
                            " ky_thuat = 0" +
                            " and (charindex(@key_word, tieu_de) > 0 or charindex(@key_word, noi_dung) > 0)" +
                            " order by nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
                        ;
                        break;
                    case "ky_thuat":
                        sql_query_show_link = "(select ('TinTuc.aspx?id=' + convert(varchar(100),tin_tuc.id_tt))) as link,";
                        vmk_csdl.sql_query = "select *," + sql_query_show_link +
                            " ngay_tt as ngay_dang, thang_tt as thang_dang, nam_tt as nam_dang" +
                            " from tin_tuc" +
                            " where" +
                            " ky_thuat = 1" +
                            " and (charindex(@key_word, tieu_de) > 0 or charindex(@key_word, noi_dung) > 0)" +
                            " order by nam_tt desc, thang_tt desc, ngay_tt desc, id_tt desc"
                        ;
                        break;
                    case "hoi_dap":
                        sql_query_show_link = "(select ('HoiDap.aspx?id=' + convert(varchar(100),hoi_dap.id_hd))) as link,";
                        vmk_csdl.sql_query = "select *," + sql_query_show_link +
                            " ngay_hd as ngay_dang, thang_hd as thang_dang, nam_hd as nam_dang" +
                            " from hoi_dap" +
                            " where" +
                            " chia_se = 1" +
                            " and (charindex(@key_word, tieu_de) > 0 or charindex(@key_word, noi_dung) > 0)" +
                            " order by nam_hd desc, thang_hd desc, ngay_hd desc, id_hd desc"
                        ;
                        break;
                }

                DataTable sql_param = vmk_csdl.sql_param;
                sql_param.Rows.Add("@key_word", tu_khoa, SqlDbType.NVarChar);
                sql_param.Rows.Add("@id_dm_dich_vu", id_dm_dich_vu, SqlDbType.Int);
                vmk_csdl.sql_param = sql_param;

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                if (BANG_KQ.Rows.Count == 0)
                {
                    label_thongbao.Text = "*** KHÔNG TÌM THẤY KẾT QUẢ NÀO";
                    return;
                }

                repeater_list_data.DataSource = BANG_KQ;
                repeater_list_data.DataBind();
            }
        }
    }
}
