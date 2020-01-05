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
    public partial class FileManager : System.Web.UI.Page
    {
        string path_folder_images = @"..\images_products";
        string ext_allow = ".jpg";
        int size_allow = 5000000;

        string PageName = "FileManager.aspx";

        string Xu_Ly_File_Name(string Path_File)
        {
            int LastX = Path_File.LastIndexOf("\\");
            string Name_File = Path_File.Substring(LastX + 1);
            return Name_File;
        }

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

            string vmk_title_page = "QUẢN LÝ HÌNH ẢNH";

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

            // BEGIN //

            if (!IsPostBack)
            {
                DataTable DTFiles = new DataTable();
                DTFiles.Columns.Add("Name", typeof(String));
                DTFiles.Columns.Add("Size", typeof(String));
                DTFiles.Columns.Add("Link", typeof(String));
                DTFiles.Columns.Add("DateTimeModified", typeof(double));

                string[] Files_Hinh_Anh = System.IO.Directory.GetFiles(Server.MapPath(path_folder_images), "*.jpg");
                foreach (string File_Name in Files_Hinh_Anh)
                {
                    System.IO.FileInfo FileInFo = new System.IO.FileInfo(File_Name);
                    DTFiles.Rows.Add(Xu_Ly_File_Name(File_Name), FileInFo.Length, Xu_Ly_File_Name(File_Name),FileInFo.LastWriteTime.ToString("yyyyMMddHHmmss"));
                }

                if (DTFiles.Rows.Count == 0)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG TÌM THẤY FILE ẢNH NÀO", "", false);
                    return;
                }

                // SẮP XẾP THEO THỜI GIAN //

                DataView dv = DTFiles.DefaultView;
                dv.Sort = "DateTimeModified DESC";

                DTFiles = dv.ToTable();

                repeater_list_data.DataSource = DTFiles;
                repeater_list_data.DataBind();
            }
        }

        protected void repeater_list_data_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField id_item = (HiddenField)e.Item.FindControl("id_item");

            if (e.CommandName == "xoa")
            {
                System.IO.File.Delete(Server.MapPath(path_folder_images) + "\\" + id_item.Value);
            }

            // CHUYỂN VỀ TRẠNG THÁI XEM //

            Response.Redirect(PageName);
        }

        protected void btn_upload_Click(object sender, EventArgs e)
        {
            if (vmk_file_uploader.HasFile)
            {
                string ext = System.IO.Path.GetExtension(vmk_file_uploader.FileName);
                if (ext.ToLower() != ext_allow)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("CHỈ CHẤP NHẬN FILE ẢNH JPG");
                    return;
                }

                if (vmk_file_uploader.FileContent.Length > size_allow)
                {
                    label_thongbao.Text = ClassMain.TAO_THONG_BAO("KHÔNG THỂ TẢI LÊN FILE LỚN HƠN " + size_allow.ToString());
                    return;
                }

                string FileName = System.IO.Path.Combine(Server.MapPath(path_folder_images), vmk_file_uploader.FileName);

                vmk_file_uploader.SaveAs(FileName);

                Response.Redirect(PageName);
            }
        }
    }
}
