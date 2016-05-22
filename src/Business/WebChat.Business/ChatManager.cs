//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Abstract;
//using WebChat.BusinessLogic.Chat;
//using WebChat.BusinessLogic.Chat.Entities;
//using WebChat.BusinessLogic.Chat.Storages;

//namespace WebChat.BusinessLogic.Managers
//{
//    public class ChatManager
//    {
//        public ChatManager()
//        {
//            AgentManager = new AgentManager();
//            DialogManager = new DialogManager();
//        }
//        public AgentManager AgentManager { get; set; } //слой, над AgentsStorage, позволяющий делать сложную выборку агентов 
//        public DialogManager DialogManager { get; set; }
//        public MessagesStorage MessagesStorage { get; set; } //доступ ко всем сообщениям без зависимостей
//    }

//}
