
namespace WebChat.Business.Chat.Stotages
{
    #region Using

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using WebChat.Business.Chat.Entities;
    using WebChat.Business.Interfaces;

    #endregion

    public class DialogStorage : IChatItemRepository<ChatDialog>
    {

        private static DialogStorage instance = null;
        private static readonly object padlock = new object();

        public static DialogStorage Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new DialogStorage();
                    return instance;
                }
            }
        }

        private int _dialogsCounter = 0;
        public int GenerateId()
        {
            return ++_dialogsCounter;
        }
        private BlockingCollection<ChatDialog> _dialogs { get; set; }

        public DialogStorage()
        {
            _dialogs = new BlockingCollection<ChatDialog>();
        }
        public IEnumerable<ChatDialog> All
        {
            get { return _dialogs; }
        }
        public ChatDialog GetById(int Id)
        {
            ChatDialog dialog = _dialogs.FirstOrDefault(d => d.Id == Id);
            return dialog;
        }

        public ChatDialog Find(Func<ChatDialog, bool> predicate)
        {
            ChatDialog dialog = _dialogs.FirstOrDefault(predicate);
            return dialog;
        }

        public bool Add(ChatDialog item)
        {
            return _dialogs.TryAdd(item, 2000);
        }

        public void Delete(dynamic Id)
        {
            ChatDialog dialogForDelete = _dialogs.FirstOrDefault(d => d.Id == Id);
            if (dialogForDelete != null)
                _dialogs.TryTake(out dialogForDelete, 2000);
        }

    }
}
