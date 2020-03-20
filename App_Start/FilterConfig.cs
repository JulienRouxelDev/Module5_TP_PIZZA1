using System.Web;
using System.Web.Mvc;

namespace TP_MODULE5_PIZZA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
