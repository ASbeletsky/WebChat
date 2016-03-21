namespace WebChat.WebUI.WebApp.AppStart
{
    #region Using

    using System.Web.Http;

    #endregion

    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "",
               routeTemplate: "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            return config;
        }
    }
}
