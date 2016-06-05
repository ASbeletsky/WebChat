namespace WebChat.Business.DomainModels
{
     #region Using

    using WebChat.Data.Interfaces;
    using Services.Interfaces;

    #endregion

    public abstract class BaseDomainModel
    {
        private IDataStorage storage;
        private IEntityConverter converter;
        private IApplicationSettings settings;

        public BaseDomainModel()
        {
            storage = DependencyContainer.Current.GetService<IDataStorage>();
            converter = DependencyContainer.Current.GetService<IEntityConverter>();
        }
        public BaseDomainModel(IDataStorage storage)
        {
            this.storage = storage;
        }

        public BaseDomainModel(IDataStorage storage, IEntityConverter converter) : this(storage)
        {
            this.converter = converter;
        }

        protected IEntityConverter Converter
        {
            get
            {
                return converter;
            }
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
