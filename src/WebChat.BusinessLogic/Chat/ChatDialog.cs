using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.BusinessLogic.Chat
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
        public string ApplicationKey { get; set; }
        public ChatClient Client { get; set; }
        public ChatAgent Agent { get; set; }
        public Dialog AsStoredDialog { get; set; }

        public List<string> MembersIds
        {
            get
            {
                return new List<string> { Client.ConnectionId, Agent.ConnectionId };
            }
        }

    }
}
