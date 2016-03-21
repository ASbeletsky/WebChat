namespace WebChat.Domain.Data.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Domain.Core.Chat;
    using WebChat.Domain.Interfaces.Repositories;
    using WebChat.Domain.Data;

    #endregion

    public class DialogRepository : IDialogRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public DialogRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public DialogModel GetById(int id)
        {
            return _context.Dialogs.Find(id);
        }

        public DialogModel Find(Func<DialogModel, bool> predicate)
        {
            return _context.Dialogs.FirstOrDefault(predicate);
        }

        public IEnumerable<DialogModel> All
        {
            get
            {
                return _context.Dialogs;
            }
        }

        public void Create(DialogModel item)
        {
            _context.Dialogs.Add(item);
            _context.SaveChanges();
        }

        public void Update(DialogModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var recordForDelete = _context.Dialogs.Find(id);
            if (recordForDelete != null)
            {
                _context.Dialogs.Remove(recordForDelete);
                _context.SaveChanges();
            }
        }

        #endregion
    }
}
