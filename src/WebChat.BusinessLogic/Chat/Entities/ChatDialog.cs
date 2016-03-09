using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.BusinessLogic.Chat.Entities
{
    public class ChatDialog
    {
        public ChatDialog() { }

        public ChatDialog(int id, ChatClient client, ChatAgent agent)
        {
            Id = id;
            Client = client;
            Agent = agent;
        }
        public int Id { get; set; }
        public string ApplicationKey { get; set; }
        public ChatClient Client { get; set; }
        public ChatAgent Agent { get; set; }
        public int StoredDialogId { get; set; }
        public List<string> MembersHubConnectionIds
        {
            get { return new List<string> { Client.HubConnectionId, Agent.HubConnectionId }; }
        }
        public List<string> MembersHubUserIds
        {
            get { return new List<string> { Client.HubUserId, Agent.HubUserId }; }
        }
    }
}
