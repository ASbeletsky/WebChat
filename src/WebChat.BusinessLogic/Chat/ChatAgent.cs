using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.BusinessLogic.Chat
{
    public class ChatAgent : ChatMember
    {

        public int Id { get; set; };
        public List<ChatDialog> Dialogs { get; set; }

        public ChatAgent()
        {
            Dialogs = new List<ChatDialog>();
        }
    }
}
