namespace WebChat.Data.Storage.Repositories
{
    #region Using

    using Models.Chat;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion

    public class MessageRepository : IMessageRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public MessageRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public MessageModel GetById(long id)
        {
            return _context.Messages.Find(id);
        }

        public MessageModel Find(Func<MessageModel, bool> predicate)
        {
            return _context.Messages.FirstOrDefault(predicate);
        }

        public IEnumerable<MessageModel> All
        {
            get
            {
                return _context.Messages;
            }
        }

        public void Create(MessageModel item)
        {
            _context.Messages.Add(item);
            _context.SaveChanges();
        }

        public void Update(MessageModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var recordForDelete = _context.Messages.Find(id);
            if (recordForDelete != null)
            {
                _context.Messages.Remove(recordForDelete);
                _context.SaveChanges();
            }
        }

        #endregion
    }
}
