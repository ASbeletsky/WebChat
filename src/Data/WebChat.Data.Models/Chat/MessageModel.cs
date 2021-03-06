﻿namespace WebChat.Data.Models.Chat
{
    #region Using

    using System;
    using Storage.Identity;

    #endregion

    public class MessageModel
    {
        public long Id
        {
            get;
            set;
        }

        public int DialogId
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public DateTime SendedAt
        {
            get;
            set;
        }

        public long SenderId
        {
            get;
            set;
        }

        public virtual DialogModel Dialog
        {
            get;
            set;
        }

        public virtual UserModel Sender
        {
            get;
            set;
        }

    }
}
