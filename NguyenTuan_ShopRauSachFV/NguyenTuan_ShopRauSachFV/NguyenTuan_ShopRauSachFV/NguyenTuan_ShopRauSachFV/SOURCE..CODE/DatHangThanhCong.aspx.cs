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
    public partial class DatHangThanhCong_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // XỬ LÝ TIÊU ĐỀ CHO PAGE //

            string vmk_title_page = "ĐẶT HÀNG THÀNH CÔNG";

            string ten_cua_hang = ClassMain.Xu_Ly_Session("GET", "ten_cua_hang");
            if (ten_cua_hang != null) { if (ten_cua_hang.Trim() != "") { vmk_title_page += " - " + ten_cua_hang; } }
            ContentPlaceHolder vmk_ContentPlaceHolder_for_title_page = (ContentPlaceHolder)this.Master.FindControl("vmk_ContentPlaceHolder_for_title_page");
            vmk_ContentPlaceHolder_for_title_page.Controls.Add(new LiteralControl(vmk_title_page));
        }
    }
}
