using System;

namespace WebChat.WebUI.ViewModels.Agent
{
    public class AgentShortInfoViewModel
    {
        public long UserId
        {
            get;
            set;
        }

        public int AppId
        {
            get;
            set;
        }

        public string AppUrl
        {
            get;set;
        }
        
        public string Name
        {
            get;
            set;
        }

        public decimal Rating
        {
            get;
            set;
        }

        public string PhotoSource
        {
            get;
            set;
        }

        public int DialogsCount
        {
            get;
            set;
        }
    }
}
