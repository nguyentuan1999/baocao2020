using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RAU_SACH_THANH_TRUC
{
    public static class ClassMain
    {
        public static string Xu_Ly_Session(string YeuCau, string Key = "", string Value = "")
        {
            string SESSION_KEY = "YOUR_SESSION_KEY";

            string Value_TMP;
            Dictionary<string, string> DicTionary_TEMP;

            if (YeuCau.ToUpper().Trim() == "GET")
            {
                if (Key.Trim() == "") { return null; }
                // ĐƯA SESSION VÀO ĐỐI TƯỢNG oSession //
                object oSession = System.Web.HttpContext.Current.Session[SESSION_KEY];
                // NẾU ĐỐI TƯỢNG oSession NULL THÌ TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY. SAU ĐÓ ĐƯA VÀO SESSION RỒI THOÁT HÀM //
                if (oSession == null)
                {
                    System.Web.HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                    System.Web.HttpContext.Current.Session.Timeout = 3600;
                    return null;
                }
                // NẾU ĐỐI TƯỢNG oSession KHÔNG NULL THÌ TIẾN HÀNH GÁN VÀO BIẾN DicTionary_TEMP VÀ KIỂM TRA KIỂU DỮ LIỆU //
                try
                {
                    DicTionary_TEMP = (Dictionary<string, string>)oSession;
                }
                catch (Exception Ex)
                {
                    // NẾU QUÁ TRÌNH GÁN XẢY RA LỖI CHỨNG TỎ BIẾN oSession SAI KIỂU DỮ LIỆU //
                    // THÌ TIẾN HÀNH TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY. SAU ĐÓ ĐƯA VÀO SESSION RỒI THOÁT HÀM //
                    Console.WriteLine(Ex.ToString());
                    System.Web.HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                    System.Web.HttpContext.Current.Session.Timeout = 3600;
                    return null;
                }
                // TIẾN HÀNH LẤY GIÁ TRỊ THEO KEY. NẾU KHÔNG TỒN TẠI THÌ SẼ TRẢ VỀ NULL //
                if (DicTionary_TEMP.TryGetValue(Key, out Value_TMP) == true) { return Value_TMP; }
                return null;
            }

            if (YeuCau.ToUpper().Trim() == "SET")
            {
                if (Key.Trim() == "") { return "Error"; }
                // ĐƯA SESSION VÀO ĐỐI TƯỢNG oSession //
                object oSession = System.Web.HttpContext.Current.Session[SESSION_KEY];
                // NẾU ĐỐI TƯỢNG oSession NULL THÌ TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY //
                if (oSession == null) { oSession = new Dictionary<string, string>(); }
                // NẾU ĐỐI TƯỢNG oSession KHÔNG NULL THÌ TIẾN HÀNH GÁN VÀO BIẾN DicTionary_TEMP VÀ KIỂM TRA KIỂU DỮ LIỆU //
                try
                {
                    DicTionary_TEMP = (Dictionary<string, string>)oSession;
                }
                catch (Exception Ex)
                {
                    // NẾU QUÁ TRÌNH GÁN XẢY RA LỖI CHỨNG TỎ BIẾN oSession SAI KIỂU DỮ LIỆU //
                    // THÌ TIẾN HÀNH TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY //
                    Console.WriteLine(Ex.ToString());
                    DicTionary_TEMP = new Dictionary<string, string>();
                }
                if (DicTionary_TEMP.TryGetValue(Key, out Value_TMP) == true) { DicTionary_TEMP[Key] = Value; } else { DicTionary_TEMP.Add(Key, Value); }
                System.Web.HttpContext.Current.Session[SESSION_KEY] = DicTionary_TEMP;
                System.Web.HttpContext.Current.Session.Timeout = 3600;
                return "Success";
            }

            if (YeuCau.ToUpper().Trim() == "REMOVE")
            {
                if (Key.Trim() == "") { return "Error"; }
                // ĐƯA SESSION VÀO ĐỐI TƯỢNG oSession //
                object oSession = System.Web.HttpContext.Current.Session[SESSION_KEY];
                // NẾU ĐỐI TƯỢNG oSession NULL THÌ TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY. SAU ĐÓ ĐƯA VÀO SESSION RỒI THOÁT HÀM //
                if (oSession == null)
                {
                    System.Web.HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                    System.Web.HttpContext.Current.Session.Timeout = 3600;
                    return "Success";
                }
                // NẾU ĐỐI TƯỢNG oSession KHÔNG NULL THÌ TIẾN HÀNH GÁN VÀO BIẾN DicTionary_TEMP VÀ KIỂM TRA KIỂU DỮ LIỆU //
                try
                {
                    DicTionary_TEMP = (Dictionary<string, string>)oSession;
                }
                catch (Exception Ex)
                {
                    // NẾU QUÁ TRÌNH GÁN XẢY RA LỖI CHỨNG TỎ BIẾN oSession SAI KIỂU DỮ LIỆU //
                    // THÌ TIẾN HÀNH TẠO MỚI VỚI KIỂU DỮ LIỆU LÀ DICTIONARY. SAU ĐÓ ĐƯA VÀO SESSION RỒI THOÁT HÀM //
                    Console.WriteLine(Ex.ToString());
                    System.Web.HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                    System.Web.HttpContext.Current.Session.Timeout = 3600;
                    return "Success";
                }
                // TIẾN HÀNH LẤY GIÁ TRỊ THEO KEY. NẾU TỒN TẠI THÌ SẼ XÓA //
                if (DicTionary_TEMP.TryGetValue(Key, out Value_TMP) == true)
                {
                    DicTionary_TEMP.Remove(Key);
                    System.Web.HttpContext.Current.Session[SESSION_KEY] = DicTionary_TEMP;
                    System.Web.HttpContext.Current.Session.Timeout = 3600;
                }
                return "Success";
            }

            if (YeuCau.ToUpper().Trim() == "CLEAR")
            {
                System.Web.HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                System.Web.HttpContext.Current.Session.Timeout = 3600;
                return "Success";
            }

            return null;
        }

        public static bool SQL_CHECK_EXISTS_V2(string sql_query)
        {
            ClassCSDL vmk_csdl = new ClassCSDL();
            vmk_csdl.sql_query = sql_query;
            DataTable BANG_KQ = vmk_csdl.VMK_SQL_SELECT();
            if (BANG_KQ.Rows.Count != 0) { return true; }
            return false;
        }

        public static string VMK_MAKE_MD5(String DATA_INPUT)
        {
            MD5 MD5_HASH = MD5.Create();
            Byte[] DATA = MD5_HASH.ComputeHash(Encoding.UTF8.GetBytes(DATA_INPUT));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < DATA.Length; i++) { sBuilder.Append(DATA[i].ToString("x2")); }
            return sBuilder.ToString();
        }

        public static bool VMK_CHECK_MD5(String DATA_INPUT, String HASH)
        {
            MD5 MD5_HASH = MD5.Create();
            String HASH_OF_DATA_INPUT = VMK_MAKE_MD5(DATA_INPUT);
            StringComparer Comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == Comparer.Compare(HASH_OF_DATA_INPUT, HASH)) { return true; } else { return false; }
        }

        public static string Xu_Ly_Ngay_Thang_Nam(object ngay_obj, object thang_obj, object nam_obj)
        {
            string ngay, thang, nam;
            ngay = Convert.ToString(ngay_obj);
            thang = Convert.ToString(thang_obj);
            nam = Convert.ToString(nam_obj);
            if (ngay.Length < 2) { ngay = "0" + ngay; }
            if (thang.Length < 2) { thang = "0" + thang; }
            return ngay + "/" + thang + "/" + nam;
        }

        public static string Xu_Ly_Gioi_Tinh(object gioi_tinh_obj)
        {
            bool gioi_tinh = Convert.ToBoolean(gioi_tinh_obj);
            if (gioi_tinh == true) { return "Nữ"; }
            return "Nam";
        }

        public static string HTML_Encode(object data_obj, bool replace_newline = false)
        {
            string data = Convert.ToString(data_obj);
            data = WebUtility.HtmlEncode(data);
            if (replace_newline == true)
            {
                data = data.Replace(System.Environment.NewLine, "<br/>");
            }
            return data;
        }

        public static string TAO_THONG_BAO(string Noi_dung, string Tieu_de = "", bool Show_Button_Close = true)
        {
            if (Noi_dung.Trim() == "") { Noi_dung = "Nội dung"; }

            String kq = "";

            kq = kq + "<div class=\"alert alert-dismissable alert-warning\">";

            if (Show_Button_Close == true)
            {
                kq = kq + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>";
            }

            if (Tieu_de != "")
            {
                kq = kq + "<h4>" + Tieu_de + "</h4>";
            }

            kq = kq + "<p>" + Noi_dung + "</p>";
            kq = kq + "</div>";

            return kq;
        }

        public static string Decode_BBCode(string BBCode)
        {
            Regex regExp;

            regExp = new Regex(@"\[b\](.+?)\[/b\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<strong>$1</strong>");

            regExp = new Regex(@"\[i\](.+?)\[/i\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<i>$1</i>");

            regExp = new Regex(@"\[u\](.+?)\[/u\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<u>$1</u>");

            regExp = new Regex(@"\[left\](.+?)\[/left\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<div align='left'>$1</div>");

            regExp = new Regex(@"\[center\](.+?)\[/center\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<div align='center'>$1</div>");

            regExp = new Regex(@"\[right\](.+?)\[/right\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<div align='right'>$1</div>");

            regExp = new Regex(@"\[justify\](.+?)\[/justify\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<div align='justify'>$1</div>");

            regExp = new Regex(@"\[ul\](.+?)\[/ul\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<ul>$1</ul>");

            regExp = new Regex(@"\[ol\](.+?)\[/ol\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<ol>$1</ol>");

            regExp = new Regex(@"\[li\](.+?)\[/li\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<li>$1</li>");

            regExp = new Regex(@"\[font=(.*?)\](.*?)\[/font\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<font face='$1'>$2</font>");

            regExp = new Regex(@"\[size=(.*?)\](.*?)\[/size\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<div style='font-size: $1px'>$2</div>");

            regExp = new Regex(@"\[color=(.*?)\](.*?)\[/color\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<font color='$1'>$2</font>");

            regExp = new Regex(@"\[img=(.*?)x(.*?)\](.+?)\[/img\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<img width='$1' height='$2' src='$3'>");

            regExp = new Regex(@"\[img\](.+?)\[/img\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<img src='$1'>");

            regExp = new Regex(@"\[email=(.*?)\](.*?)\[/email\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<a target='_blank' href='mailto:$1'>$2</a>");

            regExp = new Regex(@"\[url=(.*?)\](.*?)\[/url\]", RegexOptions.IgnoreCase);
            BBCode = regExp.Replace(BBCode, "<a target='_blank' href='$1'>$2</a>");

            return BBCode;
        }
    }
}