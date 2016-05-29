namespace WebChat.Business.DomainModels
{
     #region Using

    using WebChat.Domain.Interfaces;
    using Services.Interfaces;

    #endregion

    public abstract class BaseDomainModel
    {
        private IUnitOfWork storage;
        private IApplicationSettings settings;

        public BaseDomainModel()
        {
            storage = DependencyContainer.Current.GetService<IUnitOfWork>();
        }
        public BaseDomainModel(IUnitOfWork storage)
        {
            this.storage = storage;
        }

        protected IUnitOfWork Storage
        {
            get
            {
                return storage;
            }
        }

        protected IApplicationSettings Settings
        {
            get
            {
                if(settings == null)
                {
                    settings = DependencyContainer.Current.GetService<IApplicationSettings>();
                }

                return settings;
            }
        }                    
    }
}
