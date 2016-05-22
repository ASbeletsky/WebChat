namespace WebChat.Infrastructure.CQRS.Queries.Application
{
    #region Using

    using WebChat.Infrastructure.CQRS.Interfaces;

    #endregion

    public class GetCustomerQuery : IQuery
    {
        public GetCustomerQuery(long customerId)
        {
            CustomerId = customerId;
        }
        public long CustomerId { get; set; }
    }   
}
