﻿namespace WebChat.Domain.Interfaces
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using WebChat.Domain.Interfaces.Repositories;

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