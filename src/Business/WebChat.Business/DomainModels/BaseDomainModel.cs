namespace WebChat.Business.DomainModels
{
     #region Using

    using WebChat.Domain.Interfaces;
    using Services.Interfaces;

    #endregion

    public abstract class BaseDomainModel
    {
        private IUnitOfWork storage;

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
    }
}
