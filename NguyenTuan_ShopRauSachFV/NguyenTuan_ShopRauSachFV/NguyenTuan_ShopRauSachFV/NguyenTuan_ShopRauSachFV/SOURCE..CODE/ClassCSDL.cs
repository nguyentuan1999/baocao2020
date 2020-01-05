/*
 * 
    EXAMPLE SELECT

        ClassCSDL vmk_csdl = new ClassCSDL();
        vmk_csdl.sql_query = "select vmk_value from manman89 where vmk_key=@vmk_key";

        DataTable sql_param = vmk_csdl.sql_param;
        sql_param.Rows.Add("@vmk_key", "12-05-1989", SqlDbType.VarChar);
        vmk_csdl.sql_param = sql_param;

        DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();

    EXAMPLE INSERT

        ClassCSDL vmk_csdl = new ClassCSDL();
        vmk_csdl.sql_query = "insert into manman89 (vmk_key,vmk_value) values (@vmk_key, @vmk_value)";

        DataTable sql_param = vmk_csdl.sql_param;
        sql_param.Rows.Add("@vmk_key", "12-05-1989", SqlDbType.VarChar);
        sql_param.Rows.Add("@vmk_value", "Have a nice day", SqlDbType.NVarChar);
        vmk_csdl.sql_param = sql_param;

        int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
        Response.Write(sql_status);
 
    EXAMPLE INSERT RETURN OUTPUT

        ClassCSDL vmk_csdl = new ClassCSDL();

        // QUERY FOR SQL SERVER 2005+ //

        vmk_csdl.sql_query = "insert into manman89 (vmk_key,vmk_value) output inserted.vmk_key values (@vmk_key, @vmk_value)";

        // QUERY FOR SQL SERVER 2000 //

        // vmk_csdl.sql_query = "insert into manman89 (vmk_key,vmk_value) values (@vmk_key, @vmk_value); select scope_identity()";

        DataTable sql_param = vmk_csdl.sql_param;
        sql_param.Rows.Add("@vmk_key", "12-05-1989", SqlDbType.VarChar);
        sql_param.Rows.Add("@vmk_value", "Have a nice day", SqlDbType.NVarChar);
        vmk_csdl.sql_param = sql_param;

        string sql_output = vmk_csdl.VMK_SQL_INSERT_RETURN_OUTPUT();
        Response.Write(sql_output);

    EXAMPLE DELETE

        ClassCSDL vmk_csdl = new ClassCSDL();
        vmk_csdl.sql_query = "delete from manman89 where vmk_key=@vmk_key";

        DataTable sql_param = vmk_csdl.sql_param;
        sql_param.Rows.Add("@vmk_key", "12-05-1989", SqlDbType.VarChar);
        vmk_csdl.sql_param = sql_param;

        int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
        Response.Write(sql_status);

    EXAMPLE UPDATE

        ClassCSDL vmk_csdl = new ClassCSDL();
        vmk_csdl.sql_query = "update manman89 set vmk_value=@vmk_value where vmk_key=@vmk_key";

        DataTable sql_param = vmk_csdl.sql_param;
        sql_param.Rows.Add("@vmk_key", "12-05-1989", SqlDbType.VarChar);
        sql_param.Rows.Add("@vmk_value", "Good Luck", SqlDbType.NVarChar);
        vmk_csdl.sql_param = sql_param;

        int sql_status = vmk_csdl.VMK_SQL_INSERT_DELETE_UPDATE();
        Response.Write(sql_status);
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace RAU_SACH_THANH_TRUC
{
    public class ClassCSDL
    {
        public String sql_query;
        public DataTable sql_param;

        public DataTable VMK_SQL_PARAM(){
            DataTable sql_param = new DataTable();
            sql_param.Columns.Add("key", typeof(String));
            sql_param.Columns.Add("value", typeof(String));
            sql_param.Columns.Add("kieu_du_lieu", typeof(int));
            return sql_param;
        }

        public ClassCSDL() { sql_param = VMK_SQL_PARAM(); }

        public DataTable VMK_SQL_SELECT()
        {
            // CÂU LỆNH KẾT NỐI CSDL

            String VMK_CONNECTION_STRING = "";

            VMK_CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["VMK_CONNECTION_STRING"].ConnectionString;

            SqlConnection VMK_DATABASE_CONN = new SqlConnection(VMK_CONNECTION_STRING);
            VMK_DATABASE_CONN.Open();

            // CÂU LỆNH TRUY VẤN CSDL

            SqlCommand vmk_query_command = new SqlCommand(sql_query, VMK_DATABASE_CONN);
            vmk_query_command.Parameters.Clear();

            for (int i = 0; i < sql_param.Rows.Count; i++)
            {
                vmk_query_command.Parameters.Add(new SqlParameter(sql_param.Rows[i][0].ToString(), sql_param.Rows[i][2]));
                vmk_query_command.Parameters[sql_param.Rows[i][0].ToString()].Value = sql_param.Rows[i][1];
            }

            SqlDataReader vmk_query_command_reader = vmk_query_command.ExecuteReader();

            // LẤY DỮ LIỆU TRẢ VỀ TỪ CÂU LỆNH TRUY VẤN ĐƯA VÀO ĐỐI TƯỢNG DATA TABLE

            DataTable VMK_DATA_TABLE = new DataTable();
            VMK_DATA_TABLE.Load(vmk_query_command_reader);

            // ĐÓNG KẾT NỐI CSDL

            VMK_DATABASE_CONN.Close();

            return VMK_DATA_TABLE;
        }

        public int VMK_SQL_INSERT_DELETE_UPDATE()
        {
            // CÂU LỆNH KẾT NỐI CSDL

            String VMK_CONNECTION_STRING = "";

            VMK_CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["VMK_CONNECTION_STRING"].ConnectionString;

            //VMK_CONNECTION_STRING = WebConfigurationManager.AppSettings["VMK_CONNECTION_STRING"];

            SqlConnection VMK_DATABASE_CONN = new SqlConnection(VMK_CONNECTION_STRING);
            VMK_DATABASE_CONN.Open();

            // CÂU LỆNH TRUY VẤN CSDL

            SqlCommand vmk_query_command = new SqlCommand(sql_query, VMK_DATABASE_CONN);
            vmk_query_command.Parameters.Clear();

            for (int i = 0; i < sql_param.Rows.Count; i++)
            {
                vmk_query_command.Parameters.Add(new SqlParameter(sql_param.Rows[i][0].ToString(), sql_param.Rows[i][2]));
                vmk_query_command.Parameters[sql_param.Rows[i][0].ToString()].Value = sql_param.Rows[i][1];
            }

            int vmk_query_command_status = vmk_query_command.ExecuteNonQuery();

            // ĐÓNG KẾT NỐI CSDL

            VMK_DATABASE_CONN.Close();

            return vmk_query_command_status;
        }

        public string VMK_SQL_INSERT_RETURN_OUTPUT()
        {
            // CÂU LỆNH KẾT NỐI CSDL

            String VMK_CONNECTION_STRING = "";

            VMK_CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["VMK_CONNECTION_STRING"].ConnectionString;

            //VMK_CONNECTION_STRING = WebConfigurationManager.AppSettings["VMK_CONNECTION_STRING"];

            SqlConnection VMK_DATABASE_CONN = new SqlConnection(VMK_CONNECTION_STRING);
            VMK_DATABASE_CONN.Open();

            // CÂU LỆNH TRUY VẤN CSDL

            SqlCommand vmk_query_command = new SqlCommand(sql_query, VMK_DATABASE_CONN);
            vmk_query_command.Parameters.Clear();

            for (int i = 0; i < sql_param.Rows.Count; i++)
            {
                vmk_query_command.Parameters.Add(new SqlParameter(sql_param.Rows[i][0].ToString(), sql_param.Rows[i][2]));
                vmk_query_command.Parameters[sql_param.Rows[i][0].ToString()].Value = sql_param.Rows[i][1];
            }

            string vmk_query_command_output = Convert.ToString(vmk_query_command.ExecuteScalar());

            // ĐÓNG KẾT NỐI CSDL

            VMK_DATABASE_CONN.Close();

            return vmk_query_command_output;
        }
    }
}