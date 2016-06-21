namespace WebChat.Data.Storage.Repositories
{
    #region Using

    using Models.Chat;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebChat.Data.Interfaces.Repositories;
    using WebUI.ViewModels.Сhat;

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
        }

        public void Update(DialogModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var recordForDelete = _context.Dialogs.Find(id);
            if (recordForDelete != null)
            {
                _context.Dialogs.Remove(recordForDelete);
            }
        }

        #endregion

        #region IDialogRepository Members
        public IEnumerable<MessageViewModel> GetMessages(int dialogId)
        {
            return from dialog in _context.Dialogs
                   join message in _context.Messages
                       on dialog.Id equals message.DialogId
                   join user in _context.Users
                       on message.SenderId equals user.Id
                   where dialog.Id == dialogId
                   select new MessageViewModel
                   {
                       Id = message.Id,
                       SenderId = message.SenderId,
                       SendedAt = message.SendedAt,
                       Text = message.Text,
                       SenderName = user.Name
                   };
        }

        #endregion
    }
}
