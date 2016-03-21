namespace WebChat.Domain.Core.Chat
{
    #region Using

    using System;
    using Identity;

    #endregion

    public class MessageModel
    {
        private MessageModel() { }       
        public MessageModel(string text, int dialogId, long senderId)
        {
            this.Text = text;
            this.DialogId = DialogId;
            this.SenderId = senderId;
        }

        public long Id
        {
            get;
            private set;
        }

        public int DialogId
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }

        public DateTime SendedAt
        {
            get;
            private set;
        }

        public long SenderId
        {
            get;
            private set;
        }

        public virtual DialogModel Dialog
        {
            get;
            private set;
        }

        public virtual UserModel Sender
        {
            get;
            private set;
        }

    }
}
