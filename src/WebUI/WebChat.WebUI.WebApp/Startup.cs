[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(WebChat.WebUI.WebApp.Startup))]
namespace WebChat.WebUI.WebApp
{
    #region Using

    using Owin;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using WebChat.WebUI.WebApp.AppStart;

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
        }
    }
}
