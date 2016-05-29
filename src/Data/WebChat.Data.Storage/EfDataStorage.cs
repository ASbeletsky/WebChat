﻿namespace WebChat.Data.Storage
{
    #region Using

    using Repositories;
    using System;
    using WebChat.Domain.Interfaces;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion

    public class EfDataStorage : IDataStorage
    {
        #region Private Members

        private WebChatDbContext context;
        private IMessageRepository messages;
        private IDialogRepository dialogs;
        private ICustomerAppRepository customerApplications;
        private IUserRepository users;
        private IUsersInAppsRepository usersInApplication;

        #endregion

        public EfDataStorage(WebChatDbContext context)
        {
            this.context = context;
        }

        
        #region IDataStorage Members

        public ICustomerAppRepository Applications
        {
            get
            {
                if (customerApplications == null)
                    customerApplications = new CustomerAppRepository(context);
                return customerApplications;
            }
        }

        public IDialogRepository Dialogs
        {
            get
            {
                if (dialogs == null)
                    dialogs = new DialogRepository(context);
                return dialogs;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                if (messages == null)
                    messages = new MessageRepository(context);
                return messages;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (users == null)
                    users = new UserRepository(context);
                return users;
            }
        }

        public IUsersInAppsRepository UsersInApplication
        {
            get
            {
                if (usersInApplication == null)
                    usersInApplication = new UsersInAppsRepository(context);
                return usersInApplication;
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