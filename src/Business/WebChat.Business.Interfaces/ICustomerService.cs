namespace WebChat.Business.Interfaces
{
    
    #region Using

    using System.Threading.Tasks;
    using Core.Customer;

    #endregion

    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
