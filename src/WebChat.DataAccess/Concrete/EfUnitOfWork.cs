namespace WebChat.DataAccess.Concrete
{

    #region Using

    using WebChat.DataAccess.Concrete.Repositories;
    using WebChat.Models.Entities.Chat;
    using WebChat.Infrastructure.Data;
    using Models.Entities.CustomerApps;
    using Models.Entities.Identity;
    using System.Collections.Generic;

    #endregion

    public class EfUnitOfWork : IDataService
    {
        private WebChatDbContext _context;
        public EfUnitOfWork()
        {
            _context = WebChatDbContext.GetInstance();
        }

        private IRepository<CustomerApplication> _customerApplications;
        private IRepository<AppUser> _users;
        private IRepository<Dialog> _dialogs;
        private IRepository<Message> _messages;

        public IRepository<CustomerApplication> CustomerApplications
        {
            get
            {
                if (_customerApplications == null)
                    _customerApplications = new CustomerAppRepository(_context);
                return _customerApplications;
            }
        }
        public IRepository<AppUser> Users
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(_context);
                return _users;
            }
        }
       
        public IRepository<Dialog> Dialogs
        {
            get
            {
                if (_dialogs == null)
                    _dialogs = new DialogRepository(_context);
                return _dialogs;
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                if (_messages == null)
                    _messages = new MessageRepository(_context);
                return _messages;               
            }
        }

        public static IDataService GetInstance()
        {
            return new EfUnitOfWork();
        }

        public IEnumerable<TResult> ExecuteCollectionQuery<TResult>(string collectionQuery, params object[] parameters)
        {            
            IEnumerable<TResult> queryResults = _context.Database.SqlQuery<TResult>(collectionQuery, parameters);
            return queryResults;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() {  }
    }
}
