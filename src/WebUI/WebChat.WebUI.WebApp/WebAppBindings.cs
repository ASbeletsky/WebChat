namespace WebChat.WebUI.WebApp
{
    
    #region Using

    using Microsoft.Owin.Security;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using RealTimeWebService;
    using System.Web;
    using Microsoft.AspNet.SignalR;

    #endregion

    public class WebAppBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            Kernel.Bind<IUserIdProvider>().To<SignalrUserIdProvider>().InRequestScope();
        }
    }
}
