using System;


namespace WebChat.WebUI.ViewModels.Сhat
{
    public class MessageViewModel
    {
        public long Id
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

        public string FormatedSendedAt
        {
            get
            {
                return this.SendedAt.ToShortTimeString();
            }
        }

        public long SenderId
        {
            get;
            set;
        }

        public string SenderName
        {
            get;
            set;
        }
    }
}
