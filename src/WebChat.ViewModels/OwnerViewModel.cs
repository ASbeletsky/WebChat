using System;
using System.Collections.Generic;
using System.Web;
using WebChat.BusinessLogic.Chat.Entities;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.WebUI.Models
{
    public class OwnerViewModelApp
    {
        public int appId { get; set; }
        public string siteUrl { get; set; }
        public List<AppUser> relatedAgents { get; set; }
        public List<ChatAgent> relatedAgentsOnline { get; set; }
        public int appDialogsCount { get; set; }
        public int appMessagesCount { get; set; }
        public string mostActiveAgentName { get; set; }
        public int mostActiveAgentDialogsCount { get; set; }
    }
}
