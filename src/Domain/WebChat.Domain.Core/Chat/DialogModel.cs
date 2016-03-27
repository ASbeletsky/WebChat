namespace WebChat.Domain.Core.Chat
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Identity;
    using Application;
    #endregion

    public class DialogModel
    {
        public DialogModel()
        {
            this.Messages = new HashSet<MessageModel>();
            this.Users = new HashSet<UserModel>();
        }

        public DialogModel(DateTime startedAt, ICollection<long> userIds) : this()
        {
            this.StartedAt = startedAt;
            //TODO: Add Users Initialization
        }

        public int Id
        {
            get;
            private set;
        }
        public DateTime StartedAt
        {
            get;
            set;
        }

        public DateTime ClosedAt
        {
            get;
            set;
        }

        public int AppId
        {
            get;
            set;
        }

        public virtual ICollection<MessageModel> Messages
        {
            get;
            private set;
        }

        public virtual CustomerApplicationModel App
        {
            get;
        }

        public virtual ICollection<UserModel> Users
        {
            get;
            private set;
        }

    }
}
