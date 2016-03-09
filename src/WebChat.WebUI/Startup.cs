using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Owin;
using WebChat.ApplicationBootstrap;


[assembly: OwinStartupAttribute(typeof(WebChat.WebUI.Startup))]
namespace WebChat.WebUI
{
    public partial class Startup
    {
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ApplicationBootstraper.DependencyInjector.RegisterBindings();
            ApplicationBootstraper.RealTimeWebService.RegisterService(app);
        }
    }
}
