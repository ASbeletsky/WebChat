using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.DataAccess.Concrete.Entities.Chat;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.BusinessLogic.Chat
{
    public class ChatClient : ChatMember
    {

        public ChatClient() { }

        public ChatClient(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }
        public ChatClient(string connectionId, string name, AppUser user): this(connectionId,name)
        {
            AsUser = user;
        }
        public string Name { get; set; }
        public bool ExistAsUser
        {
            get { return (AsUser != null); }
        }

        public ChatDialog Dialog { get; set; }
    }
}
