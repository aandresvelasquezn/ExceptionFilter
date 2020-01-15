using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplicationException.App_Start;

namespace WebApplicationException
{
    public partial class Prueba : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetDebuggingProcess();
        }

        [WebMethod(EnableSession = true)]
        [CustomExceptionFilter]
        public static object GetDebuggingProcess()
        {
            throw new Exception("Falla ASPX");
            return new object();
        }
    }
}