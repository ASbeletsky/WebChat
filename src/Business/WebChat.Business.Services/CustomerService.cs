namespace WebChat.Business.Services
{
    #region Using

    using System.Threading.Tasks;
    using WebChat.Business.Core;
    using WebChat.Business.Core.Identity;
    using WebChat.Domain.Core.Identity;
    using Domain.Data.Managers;
    using WebChat.Services.Interfaces;
    using WebChat.Business.Interfaces;
    using Core.Customer;
    #endregion

    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(AppUserManager storageUserManager, IEntityConverter entityConverter) : base(storageUserManager, entityConverter)
        {

        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            customer = (Customer) await base.CreateUserAsync(customer);
            var addToRoleResult = await DomainUserManager.AddToRoleAsync(customer.Id, Roles.Customer.ToString());
            if(!addToRoleResult.Succeeded)
            {
                var addToRoleErrors = GetErrorsByKey(addToRoleResult.Errors, "Add to role Errors");
                throw new BusinessLogicException(addToRoleErrors);
            }
            else
            {
                return customer;
            }          
        }           
    }
}
