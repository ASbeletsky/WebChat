using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Business.Core;
using WebChat.Business.Core.User;
using WebChat.Domain.Core.Identity;
using WebChat.Domain.Data.Managers;
using WebChat.Services.Interfaces;

namespace WebChat.Business.Management
{
    #region Using


    #endregion
    public class CustomerManagement
    {
        private AppUserManager _storageUserManager;
        private IEntityConverter _entityConverter;

        [Ninject.Inject]
        public CustomerManagement(AppUserManager storageUserManager, IEntityConverter entityConverter)
        {
            this._storageUserManager = storageUserManager;
            this._entityConverter = entityConverter;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var userModel = _entityConverter.Convert<Customer, UserModel>(customer);            
            var result = await _storageUserManager.CreateAsync(userModel);
            if (result.Succeeded)
            {
                result = await _storageUserManager.AddToRoleAsync(userModel.Id, Roles.Customer);
                if(result.Succeeded)
                {
                    return _entityConverter.Convert<UserModel, Customer>(userModel);
                }

                var addToRoleErrors = GetErrorsByKey(result.Errors, "Create user errors");
                throw new BusinessLogicException(addToRoleErrors);  
            }

            var createErrors = GetErrorsByKey(result.Errors, "Create user errors");
            throw new BusinessLogicException(createErrors);
        } 

        private Dictionary<TKey, TValue> GetErrorsByKey<TKey, TValue>(IEnumerable<TValue> errors, TKey key)
        {
            var errorDictionary = new Dictionary<TKey, TValue>();
            foreach(var error in errors)
            {
                errorDictionary.Add(key, error);
            }

            return errorDictionary;
        }
            
    }
}
