namespace WebChat.BusinessLogic.DomainModels
{
    #region Using

    using WebChat.Infrastructure.Data;

    #endregion

    public class CustomerAppDomainModel : BaseDomainModel
    {
        public CustomerAppDomainModel(IDataService storage) : base(storage)
        {

        }
    }
}
