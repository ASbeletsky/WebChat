namespace WebChat.Business.DomainModels
{
     #region Using

    using WebChat.Data.Interfaces;
    using Services.Interfaces;

    #endregion

    public abstract class BaseDomainModel
    {
        private IDataStorage storage;
        private IApplicationSettings settings;

        public BaseDomainModel()
        {
            storage = DependencyContainer.Current.GetService<IDataStorage>();
        }
        public BaseDomainModel(IDataStorage storage)
        {
            this.storage = storage;
        }

        protected IDataStorage Storage
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
