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
    public partial class API_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null && Request.QueryString["action"].ToString() != "")
                {
                    string action = Request.QueryString["action"].ToString().Trim();
                    if (action == "get_list_dm")
                    {
                        if (Request.QueryString["id_dm"] != null && Request.QueryString["id_dm"].ToString() != "")
                        {
                            string id_dm_cha = Request.QueryString["id_dm"].ToString().Trim();

                            ClassCSDL vmk_csdl = new ClassCSDL();

                            vmk_csdl.sql_query = "select id_dm, ten_dm from danh_muc where id_dm_cha = @id_dm";

                            DataTable sql_param = vmk_csdl.sql_param;
                            sql_param.Rows.Add("@id_dm", id_dm_cha, SqlDbType.Int);
                            vmk_csdl.sql_param = sql_param;

                            DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

                            if (BANG_KQ.Rows.Count == 0)
                            {
                                Response.Write("DANH MỤC NÀY KHÔNG CÓ DANH MỤC CON");
                                return;
                            }

                            string html = "<ul>";

                            string id_dm = "";
                            string ten_sp = "";
                            int so_luong_break_list = 0;

                            for (int i = 0; i < BANG_KQ.Rows.Count; i++)
                            {
                                id_dm = BANG_KQ.Rows[i][0].ToString().Trim();
                                ten_sp = BANG_KQ.Rows[i][1].ToString().Trim();
                                html += "<li><a href='SanPham.aspx?iddm=" + id_dm + "'>" + ten_sp + "</a></li>";

                                so_luong_break_list += 1;
                                if (so_luong_break_list > 10)
                                {
                                    html += "</ul><ul>";
                                    so_luong_break_list = 1;
                                }
                            }

                            html += "</ul>";
                            Response.Write(html);
                            return;
                        }
                    }
                }
            }
        }
    }
}