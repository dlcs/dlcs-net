using System.Web;
using System.Web.Mvc;
using DLCS.Mock.ApiApp;

namespace DLCS.Mock
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
