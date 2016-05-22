namespace WebChat.Infrastructure.Data.Storage.Factories
{
    #region Using

    using WebChat.Domain.Core.Identity;
    using WebChat.Infrastructure.Data.Models.Identity;
    using WebChat.Infrastructure.Services.Interfaces;
    using System.Collections.Generic;

    #endregion

    public class UserFactory : FactoryBase
    {
        #region Constructors

        public UserFactory() : base()
        {            
        }

        public UserFactory(IEntityConverter converter) : base(converter)
        {
        }
        #endregion

        public virtual User RestoreUserFromModel(UserModel userModel)
        {
            User user = Converter.Convert<UserModel, User>(userModel);
            return user;
        }

        public virtual IEnumerable<User> RestoreUsersFromModels(IEnumerable<UserModel> userModels)
        {
            IEnumerable<User> users = Converter.Convert<IEnumerable<UserModel>, IEnumerable<User>>(userModels);
            return users;
        }
    }
}
