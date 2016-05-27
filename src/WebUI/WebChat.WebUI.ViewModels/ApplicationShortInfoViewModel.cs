namespace WebChat.WebUI.Models
{
    #region Using

    using WebChat.WebUI.ViewModels.Operator;

    #endregion

    public class ApplicationShortInfoViewModel
    {
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
