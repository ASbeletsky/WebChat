namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region Using

    using WebChat.Domain.Core.Customer;

    #endregion
    public interface ICustomerRepository : IRepository<Customer, long>
    {
    }
}
