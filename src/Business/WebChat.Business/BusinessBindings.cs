namespace WebChat.Business
{
    using DomainModels;
    #region Using

    using Ninject.Modules;
    using System;

    #endregion

    class BusinessBindings : NinjectModule
    {     
        public override void Load()
        {
            Kernel.Bind<ApplicationDomainModel>().ToConstructor(context => new ApplicationDomainModel());
        }
    }
}
