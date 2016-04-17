namespace WebChat.Services.Managment
{

    #region Using

    using WebChat.Services.Interfaces;
    using Infrastructure.Data.Storage.Managers;
    using Infrastructure.Services.Interfaces;

    #endregion

    public class CustomerService : UserService, ICustomerService
    {
        public CustomerService(AppUserManager storageUserManager, IEntityConverter entityConverter) : base(storageUserManager, entityConverter)
        {

        }

        //public async Task<Customer> CreateCustomerAsync(Customer customer)
        //{
        //    customer = (Customer) await base.CreateUserAsync(customer);
        //    var addToRoleResult = await DomainUserManager.AddToRoleAsync(customer.Id, Roles.Customer.ToString());
        //    if(!addToRoleResult.Succeeded)
        //    {
        //        var addToRoleErrors = GetErrorsByKey(addToRoleResult.Errors, "Add to role Errors");
        //        throw new BusinessLogicException(addToRoleErrors);
        //    }
        //    else
        //    {
        //        return customer;
        //    }          
        //}           
    }
}
