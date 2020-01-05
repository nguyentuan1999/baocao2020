using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace RAU_SACH_THANH_TRUC
{
    public partial class Captcha_TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(60, 24);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            objGraphics.Clear(Color.Green);

            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            Font objFont = new Font("Arial", 9, FontStyle.Bold);
            string randomStr = "";
            int[] myIntArray = new int[5];
            int x;

            Random autoRand = new Random();

            for (x = 0; x < 5; x++)
            {
                myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                randomStr += (myIntArray[x].ToString());
            }

            ClassMain.Xu_Ly_Session("SET", "captcha", randomStr);

            objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 3);

            Response.ContentType = "image/GIF";
            objBMP.Save(Response.OutputStream, ImageFormat.Gif);

            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
        }
    }
}
