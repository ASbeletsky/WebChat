namespace WebChat.Infrastructure.CQRS.Queries.CustomerApp
{
    #region Using

    using WebChat.Infrastructure.CQRS.Interfaces;

    #endregion

    public class EditCustomerAppQuery : IQuery
    {
        public int AppId { get; set; }
    }
}
