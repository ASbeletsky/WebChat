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
    public class MessagesStorage : IChatItemRepository<ChatMessage>
    {
        private int _messagesCounter = 0;
        private BlockingCollection<ChatMessage> _messages { get; set; }

        public MessagesStorage()
        {
            _messages = new BlockingCollection<ChatMessage>();
        }


        public IEnumerable<ChatMessage> All { get; }
        public ChatMessage GetById(int Id)
        {
            ChatMessage message = _messages.FirstOrDefault(d => d.Id == Id);
            if (message != null)
            {
                return message;
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public ChatMessage Find(Func<ChatMessage, bool> predicate)
        {
            ChatMessage message = _messages.FirstOrDefault(predicate);
            if (message != null)
            {
                return message;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public bool Add(ChatMessage item)
        {
            item.Id = _messagesCounter++;
            return _messages.TryAdd(item, 2000);
        }
    }
}
