namespace WebChat.Infrastructure.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Services.Interfaces;
    using Models.Identity;
    using Factories;
    using Domain.Core.Identity;
    using System.Data.Entity;
    using Interfaces.Repositories;

    #endregion

    public class ClientRepository : IClientRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext context;
        private readonly IEntityConverter converter;
        private readonly UserFactory factory;

        #endregion

        #region Constructors
        public ClientRepository(WebChatDbContext context, IEntityConverter converter, UserFactory factory)
        {
            this.context = context;
            this.converter = converter;
            this.factory = factory;
        }

        #endregion

        public IEnumerable<Client> All
        {
            get
            {
                IEnumerable<UserModel> models = context.GetUsersInRole((int)Roles.Client);
                IEnumerable<Client> clients = factory.RestoreUsersFromModels(models) as IEnumerable<Client>;
                return clients;
            }
        }

        public void Create(Client item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            var recordForDelete = context.Users.Find(id);
            if (recordForDelete != null)
            {
                context.Users.Remove(recordForDelete);
            }
        }

        public Client GetById(long id)
        {
            if (!IsClient(id))
            {
                throw new Exception(string.Format("User with id {0} is not a customer", id));
            }

            UserModel model = context.Users.Find(id);
            Client client = factory.RestoreUserFromModel(model) as Client;
            return client;
        }

        public void Update(Client item)
        {
            if (!IsClient(item.Id))
            {
                throw new Exception(string.Format("User with id {0} is not a client", item.Id));
            }

            UserModel model = converter.Convert<Client, UserModel>(item);
            context.Entry(model).State = EntityState.Modified;
        }

        private bool IsClient(long userId)
        {
            return context.IsUserInRole(userId, (long)Roles.Client);
        }
    }
}
