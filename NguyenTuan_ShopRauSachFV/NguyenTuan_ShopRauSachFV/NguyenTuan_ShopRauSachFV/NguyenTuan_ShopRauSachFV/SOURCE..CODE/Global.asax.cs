using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace RAU_SACH_THANH_TRUC
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Application["Khach_Online"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Khach_Online"] = (int)Application["Khach_Online"] + 1;
            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Khach_Online"] = (int)Application["Khach_Online"] - 1;
            Application.UnLock();
        }
    }
}
