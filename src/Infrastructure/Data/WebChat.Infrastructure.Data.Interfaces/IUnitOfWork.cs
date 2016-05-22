namespace WebChat.Infrastructure.Data.Interfaces
{   
    #region Using

    using System;
    using Repositories;

    #endregion

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICustomerRepository Customers { get; }
        IClientRepository Clients { get; }
        IAgentRepository Agents { get; }
        IApplicationRepository Applications { get; }    
        void Save();
    }
}
