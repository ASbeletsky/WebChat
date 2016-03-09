using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.BusinessLogic.Chat.Entities;

namespace WebChat.BusinessLogic.Chat.Storages
{
    public class DialogsStorage : IChatItemRepository<ChatDialog>
    {

        private static DialogsStorage instance = null;
        private static readonly object padlock = new object();

        public static DialogsStorage Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                          instance = new DialogsStorage();
                    return instance;                                   
                }
            }
        }

        private int _dialogsCounter = 0;
        public int GenerateId()
        {
            return ++ _dialogsCounter;
        }
        private BlockingCollection<ChatDialog> _dialogs { get; set; }

        public DialogsStorage()
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
