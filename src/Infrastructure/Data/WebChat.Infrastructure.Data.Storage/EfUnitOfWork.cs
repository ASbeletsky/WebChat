namespace WebChat.Infrastructure.Data.Storage
{
    #region Using

    using Factories;
    using Interfaces;
    using Interfaces.Repositories;
    using Repositories;
    using Services.Interfaces;
    using System;

    #endregion

    public class EfUnitOfWork : IUnitOfWork
    {
        #region Private Members

        private WebChatDbContext context;
        private readonly IEntityConverter converter;

        private IApplicationRepository applications;
        private IUserRepository users;
        private ICustomerRepository customers;
        private IClientRepository clients;
        private IAgentRepository agents;

        private EfUnitOfWork()
        {
            context = WebChatDbContext.GetInstance();
            converter = DependencyResolver.Current.GetService<IEntityConverter>();
        }

        #endregion

        #region Static Members

        private static EfUnitOfWork _instance;
        static EfUnitOfWork()
        {
            _instance = new EfUnitOfWork();
        }
        public static IUnitOfWork GetInstance()
        {
            return _instance;
        }

        #endregion

        #region IUnitOfWork Members

        public IApplicationRepository Applications
        {
            get
            {
                if (applications == null)
                {
                    var appFactory = DependencyResolver.Current.GetService<ApplicationFactory>();
                    applications = new ApplicationRepository(context, converter, appFactory);
                }

                return applications;
            }
        }
      
        public IUserRepository Users
        {
            get
            {
                if (users == null)
                {
                    var userFactory = DependencyResolver.Current.GetService<UserFactory>();
                    users = new UserRepository(context, converter, userFactory);
                }

                return users;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (clients == null)
                {
                    var userFactory = DependencyResolver.Current.GetService<UserFactory>();
                    customers = new CustomerRepository(context, converter, userFactory);
                }
                return customers;
            }
        }

        public IClientRepository Clients
        {
            get
            {
                if (clients == null)
                {
                    var userFactory = DependencyResolver.Current.GetService<UserFactory>();
                    clients = new ClientRepository(context, converter, userFactory);
                }
                return clients;
            }
        }

        public IAgentRepository Agents
        {
            get
            {
                if (agents == null)
                { 
                    var userFactory = DependencyResolver.Current.GetService<UserFactory>();
                    agents = new AgentRepository(context, converter, userFactory);
                }
                return agents;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
