using System.Web;
using System.Web.Mvc;
using WebApplicationException.App_Start;

namespace WebApplicationException
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleExceptionErrorAttribute());
        }
    }
}
