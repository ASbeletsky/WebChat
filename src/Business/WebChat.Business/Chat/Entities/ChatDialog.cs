using System.Collections.Generic;

namespace WebChat.Business.Chat.Entities
{
    public class ChatDialog
    {
        public ChatDialog(int id, ChatClient client, ChatAgent agent)
        {
            Id = id;
            Client = client;
            Agent = agent;
        }
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public ChatClient Client { get; set; }
        public ChatAgent Agent { get; set; }
        public int StoredDialogId { get; set; }

        public List<string> MembersHubUserIds
        {
            get { return new List<string> { Client.HubUserId, Agent.HubUserId }; }
        }
    }
}
