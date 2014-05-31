using CDC.Aplicação.HtmlHelpers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah;

namespace CDC.Aplicação
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandleErrorFilter());
            filters.Add(new HandleErrorAttribute());
        }

        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            if (!(e.Exception.GetBaseException() is HttpException)) return;

            var ex = (HttpException)e.Exception.GetBaseException();

            if (ex.GetHttpCode() == 404)
                e.Dismiss(); // ignora o erro 404
        }
    }
}
