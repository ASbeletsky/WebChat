namespace WebChat.WebUI.WebApp
{
    #region Using

    using Microsoft.Owin.Security;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using System.Web;

    #endregion

    public class WebAppBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
        }
    }
}
