namespace WebChat.Business.Chat.Entities
{
    #region

    using System;
    using System.Collections.Generic;

    #endregion
    public class ChatAgent : ChatMember
    {
        public ChatAgent(long userId, int appId)
            :base(userId, appId)
        {
            Dialogs = new List<ChatDialog>();
        }

        /// <summary>
        /// Storage Id
        /// </summary>
        internal int Id { get; set; }
        public DateTime StartWorkAt { get; set; }      
        public IList<ChatDialog> Dialogs { get; set; }
    }
}
