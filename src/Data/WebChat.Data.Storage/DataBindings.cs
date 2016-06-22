namespace WebChat.Data.Storage
{
    #region Using

    using Ninject.Modules;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using Interfaces;

    #endregion

    public class DataBindings : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<DbContext>().ToConstructor(context => new WebChatDbContext()).InRequestScope();
            Kernel.Bind<IDataStorage>().To<EfDataStorage>().InRequestScope();
        }
    }
}
