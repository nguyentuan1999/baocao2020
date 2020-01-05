using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace RAU_SACH_THANH_TRUC
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        string ma_quyen = "";
        string tai_khoan = "";
        string id_thanh_vien = "";

        string ten_cua_hang = "", dia_chi = "", sdt = "", email = "";

        private bool kiem_tra_dang_nhap()
        {
            string ma_quyen = ClassMain.Xu_Ly_Session("GET", "ma_quyen");
            string id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
            if (id_thanh_vien != null && ma_quyen != null) { return true; }
            return false;
        }

        Int64 PageView()
        {
            string FilePageView = Server.MapPath("PageView.ini");

            StreamWriter sSave = default(StreamWriter);
            StreamReader sLoad = default(StreamReader);

            // TẠO FILE LƯU TRỮ MỚI VÀ LƯU GIÁ TRỊ MẶC ĐỊNH NẾU KHÔNG TÌM THẤY //

            if (!File.Exists(FilePageView))
            {
                sSave = File.CreateText(FilePageView);
                sSave.Write("1");
                sSave.Close();
                return 1;
            }

            // ĐỌC LƯỢT XEM TỪ FILE LƯU TRỮ //

            sLoad = File.OpenText(FilePageView);
            
            // KIỂM TRA LƯỢT XEM & CHUYỂN SANG DẠNG SỐ NGUYÊN //

            Int64 value = 0;
            bool check_number = Int64.TryParse(sLoad.ReadToEnd().ToString().Trim(), out value);
            if (check_number == false)
            {
                // NẾU GIÁ TRỊ LƯU TRỮ KHÔNG ĐÚNG DẠNG SỐ NGUYÊN. THÌ ĐÓNG & XÓA FILE //
                sLoad.Close();
                File.Delete(FilePageView);
                return 1;
            }

            // ĐÓNG PHIÊN MỞ FILE //

            sLoad.Close();

            // TĂNG GIÁ TRỊ LƯỢT XEM //

            value = value + 1;

            // GHI LẠI LƯỢT XEM SAU KHI TĂNG GIÁ TRỊ //

            sSave = File.CreateText(FilePageView);
            sSave.Write(value);
            sSave.Close();

            // TRẢ VỀ GIÁ TRỊ LƯỢT XEM MỚI //

            return value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // ĐỌC & GHI PAGE VIEW //

            label_pageview.Text = PageView().ToString();

            // KIỂM TRA ĐĂNG NHẬP //

            if (kiem_tra_dang_nhap() == true)
            {
                panel_box_taikhoan_thoat.Visible = true;
                panel_box_giohang.Visible = true;

                id_thanh_vien = ClassMain.Xu_Ly_Session("GET", "id_thanh_vien");
                if (id_thanh_vien == null) { id_thanh_vien = ""; }
            }
            else
            {
                vmk_box_dangky_dangnhap.Visible = true;
            }

            if (!IsPostBack)
            {
                // LẤY DANH SÁCH DANH MỤC //

                System.Data.DataView vmk_dataview;

                sql_datasource.SelectCommand = "select id_dm, ten_dm from danh_muc where (id_dm_cha is null) and (show_in_menu = 1) order by stt asc";
                sql_datasource.SelectParameters.Clear();
                vmk_dataview = (DataView)sql_datasource.Select(DataSourceSelectArguments.Empty);
                if (vmk_dataview.Count != 0)
                {
                    repeater_list_danhmuc.DataSource = sql_datasource;
                    repeater_list_danhmuc.DataBind();
                }

                // LẤY DỮ LIỆU TRONG CSDL //

                ClassCSDL vmk_csdl = new ClassCSDL();
                vmk_csdl.sql_query = "select ma_cau_hinh, noi_dung from cau_hinh";

                DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();
                DataRow[] BANG_KQ_FILTER;

                if (BANG_KQ.Rows.Count != 0)
                {
                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'ten_cua_hang'");
                    if (BANG_KQ_FILTER.Length != 0) { ten_cua_hang = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'dia_chi'");
                    if (BANG_KQ_FILTER.Length != 0) { dia_chi = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'sdt'");
                    if (BANG_KQ_FILTER.Length != 0) { sdt = BANG_KQ_FILTER[0][1].ToString(); }

                    BANG_KQ_FILTER = BANG_KQ.Select("ma_cau_hinh = 'email'");
                    if (BANG_KQ_FILTER.Length != 0) { email = BANG_KQ_FILTER[0][1].ToString(); }
                }

                // ĐƯA DỮ LIỆU LẤY ĐƯỢC LÊN GIAO DIỆN //

                label_tencuahang.Text = ten_cua_hang.Trim();
                label_diachi.Text = dia_chi.Trim();
                label_sdt.Text = sdt.Trim();
                label_sdt1.Text = sdt.Trim();
                label_email.Text = email.Trim();

                // LƯU TÊN CỬA HÀNG VÀO SESSION ĐỂ SỬ DỤNG CHO CÁC PAGE KHÁC

                ClassMain.Xu_Ly_Session("SET", "ten_cua_hang", ten_cua_hang.Trim());

                // LẤY THÔNG TIN GIỎ HÀNG //

                label_status_giohang_soluong.Text = "0";
                label_status_giohang_thanhtien.Text = "0";

                if (id_thanh_vien != "")
                {
                    ClassCSDL vmk_csdl1 = new ClassCSDL();
                    vmk_csdl1.sql_query = "select COUNT(id_sp) as so_luong_sp, SUM(thanh_tien) as thanh_tien" +
                        " from gio_hang" +
                        " where id_tv = @id_tv"
                    ;

                    DataTable sql_param1 = vmk_csdl1.sql_param;
                    sql_param1.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
                    vmk_csdl1.sql_param = sql_param1;

                    DataTable BANG_KQ1 = vmk_csdl1.VMK_SQL_SELECT();

                    if(BANG_KQ1.Rows.Count != 0){

                        // ĐƯA DỮ LIỆU LÊN GIAO DIỆN //

                        label_status_giohang_soluong.Text = BANG_KQ1.Rows[0][0].ToString();

                        Int64 thanh_tien = 0;
                        bool check_thanh_tien = Int64.TryParse(BANG_KQ1.Rows[0][1].ToString(), out thanh_tien);
                        if (check_thanh_tien == true)
                        {
                            label_status_giohang_thanhtien.Text = String.Format("{0:#,##}", thanh_tien);
                        }
                    }
                }

                // LẤY TÊN TÀI KHOẢN //

                ClassCSDL vmk_csdl2 = new ClassCSDL();
                vmk_csdl2.sql_query = "select ho_ten" +
                    " from thanh_vien" +
                    " where id_tv = @id_tv"
                ;
              
                DataTable sql_param2 = vmk_csdl2.sql_param;
                sql_param2.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
                vmk_csdl2.sql_param = sql_param2;

                DataTable BANG_KQ2 = vmk_csdl2.VMK_SQL_SELECT();
                if (BANG_KQ2.Rows.Count != 0)
                {
                    tai_khoan = BANG_KQ2.Rows[0][0].ToString();
                    label_thongtintaikhoan.InnerText = "XIN CHÀO " + tai_khoan.ToUpper();
                }


                ClassCSDL vmk_csdl3 = new ClassCSDL();
                vmk_csdl3.sql_query = "select ma_quyen" +
                    " from thanh_vien" +
                    " where id_tv = @id_tv"
                ;
                DataTable sql_param3 = vmk_csdl3.sql_param;
                sql_param3.Rows.Add("@id_tv", id_thanh_vien, SqlDbType.Int);
                vmk_csdl3.sql_param = sql_param3;

                DataTable BANG_KQ3 = vmk_csdl3.VMK_SQL_SELECT();
                if (BANG_KQ3.Rows.Count != 0)
                {
                    if (BANG_KQ3.Rows[0][0].ToString() == "Q001" || BANG_KQ3.Rows[0][0].ToString() == "Q002")
                    {
                        label_thongtintaikhoan.HRef = "/QuanLy/DangNhap.aspx";
                    }
                }
            }
        }
    }
}
