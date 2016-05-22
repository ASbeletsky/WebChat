namespace WebChat.Infrastructure.CQRS.QueryHandlers.Application
{
    #region Using

    using WebChat.Domain.Core.Customer;
    using WebChat.Infrastructure.CQRS.Interfaces;
    using WebChat.Infrastructure.CQRS.Queries;
    using WebChat.Infrastructure.CQRS.Queries.Application;
    using WebChat.Infrastructure.Data.Interfaces;
    using WebChat.Infrastructure.Data.Interfaces.Repositories;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public class GetCustomerQueryHandler : BaseQueryHandler, IQueryHandler<GetCustomerQuery, Customer>
    {
        private ICustomerRepository customers;
        public GetCustomerQueryHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.customers = UnitOfWork.Customers;
        }

        public Customer Execute(GetCustomerQuery query)
        {
            return customers.GetById(query.CustomerId);
        }
    }
}
