namespace WebChat.BusinessLogic.DomainModels
{
    #region Using

    using WebChat.Infrastructure.Data;

    #endregion

    //TO DO: Перенести вызов хранимок из репозиторие в доменки
    public class BaseDomainModel
    {
        private IDataService _storage;
        public BaseDomainModel(IDataService storage)
        {
            _storage = storage;
        }

        protected IDataService Storage
        {
            get
            {
                return _storage;
            }
        }

    }
}
