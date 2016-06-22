[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(WebChat.WebUI.WebApp.Startup))]
namespace WebChat.WebUI.WebApp
{
    using Microsoft.Owin.Logging;
    #region Using

    using Owin;
    using System.Security.Claims;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using WebChat.WebUI.WebApp.AppStart;
    using System;

    #endregion

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);           
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ConfigureAuth(app);
            app.UseWebApi(WebApiConfig.Register());
            app.MapSignalR();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
