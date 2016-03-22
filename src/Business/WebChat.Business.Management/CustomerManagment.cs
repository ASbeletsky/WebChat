namespace WebChat.Business.Management
{
    using Domain.Data.Managers;
    using Services.Interfaces;
    #region Using

    using System.Threading.Tasks;
    using WebChat.Business.Core;
    using WebChat.Business.Core.User;
    using WebChat.Domain.Core.Identity;

    #endregion
    public class CustomerManagement : UserManagement
    {
        public CustomerManagement(AppUserManager storageUserManager, IEntityConverter entityConverter) : base(storageUserManager, entityConverter)
        {

        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer = (Customer) await base.CreateUser(customer);
            var addToRoleResult = await DomainUserManager.AddToRoleAsync(customer.Id, Roles.Customer);
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
