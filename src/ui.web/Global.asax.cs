﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ui.web.DependencyResolution;

namespace ui.web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DependencyBootstrapper.Bootstrap();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}
