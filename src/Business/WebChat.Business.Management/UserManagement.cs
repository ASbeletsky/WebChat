using System.Collections.Generic;
using System.Threading.Tasks;
using WebChat.Business.Core;
using WebChat.Business.Core.User;
using WebChat.Domain.Core.Identity;
using WebChat.Domain.Data.Managers;
using WebChat.Services.Interfaces;

namespace WebChat.Business.Management
{
    public abstract class UserManagement
    {
        private AppUserManager _domainUserManager;
        private IEntityConverter _entityConverter;

        protected AppUserManager DomainUserManager;
        protected IEntityConverter EntityConverter;

        [Ninject.Inject]
        public UserManagement(AppUserManager domainUserManager, IEntityConverter entityConverter)
        {
            this._domainUserManager = domainUserManager;
            this._entityConverter = entityConverter;
        }

        public async Task<User> CreateUser(User user)
        {
            var userModel = _entityConverter.Convert<User, UserModel>(user);
            var result = await _domainUserManager.CreateAsync(userModel);
            if (result.Succeeded)
            {
                return _entityConverter.Convert<UserModel, Customer>(userModel);                
            }

            var createErrors = GetErrorsByKey(result.Errors, "Create user errors");
            throw new BusinessLogicException(createErrors);
        }

        protected Dictionary<TKey, TValue> GetErrorsByKey<TKey, TValue>(IEnumerable<TValue> errors, TKey key)
        {
            var errorDictionary = new Dictionary<TKey, TValue>();
            foreach (var error in errors)
            {
                errorDictionary.Add(key, error);
            }

            return errorDictionary;
        }
    }
}
