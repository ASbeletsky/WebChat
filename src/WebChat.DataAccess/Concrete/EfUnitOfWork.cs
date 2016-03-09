using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.Entities.Chat;
using WebChat.DataAccess.Concrete.Entities.Customer_apps;
using WebChat.DataAccess.Concrete.Repositories;
using WebChat.Models.Entities.Chat;

namespace WebChat.DataAccess.Concrete
{
    public class EfUnitOfWork : IDataService
    {
        private WebChatDbContext _context;
        private IAmountOfDialogs amountOfDialogs;
        public EfUnitOfWork()
        {
            _context = WebChatDbContext.GetInstance();
        }

        private CustomerAppRepository _customerApplications;
        private UserRepository _users;
        private IRepository<Dialog> _dialogs;
        private MessageRepository _messages;

        public ICustomerAppRepository CustomerApplications
        {
            get
            {
                if (_customerApplications == null)
                    _customerApplications = new CustomerAppRepository(_context);
                return _customerApplications;
            }
        }
        public UserRepository Users
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

        public void Dispose() {  }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
