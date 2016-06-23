namespace WebChat.Data.Storage
{
    #region Using

    using Repositories;
    using System;
    using Interfaces.Repositories;
    using WebChat.Data.Interfaces;

    #endregion

    /// <summary>
    /// Represents data storage by entity framework data base ORM
    /// </summary>
    public class EfDataStorage : IDataStorage
    {
        #region Private Members

        /// <summary>
        /// Contains database context
        /// </summary>
        private WebChatDbContext context;

        /// <summary>
        /// Contains message repository
        /// </summary>
        private IMessageRepository messages;

        /// <summary>
        /// Contains dialog repository
        /// </summary>
        private IDialogRepository dialogs; 

        /// <summary>
        /// Contains application reporsitory
        /// </summary>
        private ICustomerAppRepository customerApplications;

        /// <summary>
        /// Contains user reporsitory
        /// </summary>
        private IUserRepository users;

        /// <summary>
        /// Contains users in application
        /// </summary>
        private IUsersInAppsRepository usersInApplication;

        /// <summary>
        /// Contains user dialog
        /// </summary>
        private IUsersInDialogsRepository usersInDialogs;
        private IUsersInRolesRepository usersInRoles;

        #endregion

        /// <summary>
        /// Initialuzes new instance if <see cref="EfDataStorage"/>
        /// </summary>
        /// <param name="context">database context</param>
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

        public IUsersInDialogsRepository UsersInDialogs
        {
            get
            {
                if (usersInDialogs == null)
                    usersInDialogs = new UsersInDialogsRepository(context);
                return usersInDialogs;
            }
        }

        public IUsersInRolesRepository UsersInRoles
        {
            get
            {
                if (usersInRoles == null)
                    usersInRoles = new UsersInRolesRepository(context);
                return usersInRoles;
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
