namespace WebChat.RealTimeWebService
{
    #region Using

    using Ninject.Modules;
    using WebChat.Infrastructure;

    #endregion

    public class RealTimeServiceBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IRealTimeWebService>().To<SirnalrRealTimeService>();
        }
    }
}
