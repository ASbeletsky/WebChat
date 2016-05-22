namespace WebChat.Infrastructure.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using WebChat.Domain.Core.Identity;
    using WebChat.Infrastructure.Data.Interfaces.Repositories;
    using WebChat.Infrastructure.Data.Models.Identity;
    using WebChat.Infrastructure.Data.Storage.Factories;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public class AgentRepository : IAgentRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext context;
        private readonly IEntityConverter converter;
        private readonly UserFactory factory;

        #endregion

        #region Constructors
        public AgentRepository(WebChatDbContext context, IEntityConverter converter, UserFactory factory)
        {
            this.context = context;
            this.converter = converter;
            this.factory = factory;
        }

        #endregion

        public IEnumerable<Agent> All
        {
            get
            {
                IEnumerable<UserModel> models = context.GetUsersInRole((int)Roles.Agent);
                IEnumerable<Agent> agent = factory.RestoreUsersFromModels(models) as IEnumerable<Agent>;
                return agent;
            }
        }

        public void Create(Agent item)
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

        public Agent GetById(long id)
        {
            if (!IsAgent(id))
            {
                throw new Exception(string.Format("User with id {0} is not a agent", id));
            }

            UserModel model = context.Users.Find(id);
            Agent agent = factory.RestoreUserFromModel(model) as Agent;
            return agent;
        }

        public void Update(Agent item)
        {
            if (!IsAgent(item.Id))
            {
                throw new Exception(string.Format("User with id {0} is not a client", item.Id));
            }

            UserModel model = converter.Convert<Agent, UserModel>(item);
            context.Entry(model).State = EntityState.Modified;
        }

        private bool IsAgent(long userId)
        {
            return context.IsUserInRole(userId, (long)Roles.Agent);
        }
    }
}
