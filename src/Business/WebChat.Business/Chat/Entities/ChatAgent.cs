
using System;
using System.Collections.Generic;


namespace WebChat.Business.Chat.Entities
{
    public class ChatAgent : ChatMember
    {
        public ChatAgent(long userId, int appId)
            :base(userId, appId)
        {

        }

        /// <summary>
        /// Storage Id
        /// </summary>
        internal int Id { get; set; }

        public IEnumerable<ChatDialog> Dialogs { get; set; }
    }
}
