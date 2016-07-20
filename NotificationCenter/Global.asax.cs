using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace NotificationCenter
{
    public class Global : System.Web.HttpApplication
    {
        string connString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Application_Start(object sender, EventArgs e)
        {
            SqlDependency.Start(connString);
            Messages.Instance.IsCheckCertificateExpiration = false;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //Stop SQL dependency
            SqlDependency.Stop(connString);
        }
    }
}