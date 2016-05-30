namespace WebChat.WebUI.ViewModels.Application
{
    #region Using

    using WebChat.WebUI.ViewModels.Agent;

    #endregion

    public class ApplicationShortInfoViewModel
    {
        public string Name
        {
            get;
            set;
        }
        public int AppId
        {
            get;
            set;
        }

        public string SiteUrl
        {
            get;
            set;
        }

        public int AgentsCount
        {
            get;
            set;
        }

        public int AgentsOnlineCount
        {
            get;
            set;
        }

        public int DialogsCount
        {
            get;
            set;
        }

        public AgentShortInfoViewModel BestAgent
        {
            get;
            set;
        }
    }
}
