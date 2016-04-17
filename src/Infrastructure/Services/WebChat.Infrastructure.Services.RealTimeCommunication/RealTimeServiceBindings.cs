namespace WebChat.Infrastructure.Services.RealTimeCommunication
{
    #region Using

    using Ninject.Modules;
    using Services.Interfaces;

    #endregion

    public class RealTimeServiceBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IRealTimeWebService>().To<SirnalrRealTimeService>();
        }
    }
}
