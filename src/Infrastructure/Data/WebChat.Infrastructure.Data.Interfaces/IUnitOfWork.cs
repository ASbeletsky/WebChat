namespace WebChat.Infrastructure.Data.Interfaces
{   
    #region Using

    using System;
    using Repositories;

    #endregion

    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICustomerAppRepository CustomerApplications { get; }
        IUsersInAppsRepository UsersInApplication { get; }       
        IMessageRepository Messages { get; }
        IDialogRepository Dialogs { get; }
        void Save();
    }
}
