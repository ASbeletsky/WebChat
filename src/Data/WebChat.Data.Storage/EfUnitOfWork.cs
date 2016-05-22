namespace WebChat.Data.Storage
{
    using Repositories;
    #region Using

    using System;
    using WebChat.Domain.Interfaces;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion
    public class EfUnitOfWork : IUnitOfWork
    {
        #region Private Members

        private WebChatDbContext _context;
        private IMessageRepository _messages;
        private IDialogRepository _dialogs;
        private ICustomerAppRepository _customerApplications;
        private IUserRepository _users;
        private IUsersInAppsRepository usersInApplication;

        private EfUnitOfWork()
        {
            _context = WebChatDbContext.GetInstance();
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

        public ICustomerAppRepository CustomerApplications
        {
            get
            {
                if (_customerApplications == null)
                    _customerApplications = new CustomerAppRepository(_context);
                return _customerApplications;
            }
        }

        public IDialogRepository Dialogs
        {
            get
            {
                if (_dialogs == null)
                    _dialogs = new DialogRepository(_context);
                return _dialogs;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessageRepository(_context);
                return _messages;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(_context);
                return _users;
            }
        }

        public IUsersInAppsRepository UsersInApplication
        {
            get
            {
                if (usersInApplication == null)
                    usersInApplication = new UsersInAppsRepository(_context);
                return usersInApplication;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
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
                    _context.Dispose();
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
