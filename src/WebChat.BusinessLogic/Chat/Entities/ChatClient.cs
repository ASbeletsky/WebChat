using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.DataAccess.Concrete.Entities.Chat;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.BusinessLogic.Chat.Entities
{
    public class ChatClient : ChatMember
    {

        public ChatClient(string connectionId)
        {
            HubConnectionId = connectionId;
        }
        public ChatClient(string connectionId, AppUser user): this(connectionId)
        {
            AsUser = user;
        }
        public bool ExistAsUser
        {
            get { return (AsUser != null); }
        }

        public ChatDialog Dialog { get; set; }
    }
}
