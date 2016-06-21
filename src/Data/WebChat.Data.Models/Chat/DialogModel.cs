namespace WebChat.Data.Models.Chat
{
    #region Using

    using System;
    using System.Collections.Generic;
    using Storage.Identity;
    using System.Linq;
    #endregion

    public class DialogModel
    {
        public int Id
        {
            get;
            set;
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

        public virtual ICollection<MessageModel> Messages
        {
            get;
            set;
        }
        public int AppId
        {
            get;
            set;
        }

        public virtual ICollection<UsersInDialogsModel> UsersShortInfo
        {
            get;
            set;
        }

        public IEnumerable<UserModel> Users
        {
            get
            {
                return this.UsersShortInfo.Select(userInDialog => userInDialog.User);
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return this.ClosedAt - this.StartedAt;
            }
        }

        public bool IsClosed
        {
            get
            {
                return this.ClosedAt != this.StartedAt;
            }
        }
    }
}
