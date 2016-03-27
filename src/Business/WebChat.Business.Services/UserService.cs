namespace WebChat.Business.Services
{
    using Core.Customer;
    #region Using

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebChat.Business.Core;
    using WebChat.Business.Core.Identity;
    using WebChat.Domain.Core.Identity;
    using WebChat.Domain.Data.Managers;
    using WebChat.Services.Interfaces;

    #endregion

    public abstract class UserService
    {
        #region Private Members

        private AppUserManager _domainUserManager;
        private IEntityConverter _entityConverter;

        #endregion

        #region Protected Members

        protected AppUserManager DomainUserManager;
        protected IEntityConverter EntityConverter;

        #endregion
        
        public UserService(AppUserManager domainUserManager, IEntityConverter entityConverter)
        {
            this._domainUserManager = domainUserManager;
            this._entityConverter = entityConverter;
        }

        public async Task<User> CreateUserAsync(User user)
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
