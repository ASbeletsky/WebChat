namespace WebChat.Infrastructure.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces.Repositories;
    using Models.Identity;
    using Services.Interfaces;
    using Domain.Core.Identity;
    using Factories;
    using Managers;
    #endregion

    public class UserRepository : IUserRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext context;
        private readonly IEntityConverter converter;
        private readonly UserFactory factory;

        #endregion

        #region Constructors
        public UserRepository(WebChatDbContext context, IEntityConverter converter, UserFactory factory)
        {
            this.context = context;
            this.converter = converter;
            this.factory = factory;               
        }

        #endregion

        #region IRepository Members

        public User GetById(long id)
        {
            UserModel model = context.Users.Find(id);
            User user = factory.RestoreUserFromModel(model);
            return user;
        }

        public IEnumerable<User> All
        {
            get
            {
                IEnumerable<UserModel> models = context.Users;
                IEnumerable<User> users = factory.RestoreUsersFromModels(models);
                return users;
            }
        }

        public void Create(User item)
        {
            throw new NotImplementedException();
        }

        public void Update(User item)
        {
            UserModel model = converter.Convert<User, UserModel>(item);
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(long id)
        {
            var recordForDelete = context.Users.Find(id);
            if (recordForDelete != null)
            {
                context.Users.Remove(recordForDelete);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
