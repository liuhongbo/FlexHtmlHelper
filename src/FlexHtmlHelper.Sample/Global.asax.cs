using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FlexHtmlHelper;
namespace FlexHtmlHelperSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            FlexHtmlHelper.FlexRenders.Renders.DefaultRender = FlexHtmlHelper.FlexRenders.Renders["Bootstrap3"];
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
