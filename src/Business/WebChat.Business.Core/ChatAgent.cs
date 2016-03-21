//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Core.Mapping;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Abstract;
//using WebChat.BusinessLogic.Chat.Storages;
//using WebChat.DataAccess.Concrete.Entities.Identity;

//namespace WebChat.BusinessLogic.Chat.Entities
//{
//    public class ChatAgent : ChatMember
//    {
//        private IChatItemRepository<Dialog> _allDialogs;
//        public ChatAgent()
//        {
//            _allDialogs = DialogsStorage.Instance;
//        }

//        public int Id { get; set; }
//        public IEnumerable<Dialog> Dialogs
//        {
//            get
//            {
//                var myDialogs = _allDialogs.All.Where(d => d.Agent.Id == this.Id);
//                return myDialogs;
//            }
//        }
      
//        public DateTime LastMessageTime { get; set; }

//        public TimeSpan? DialogWorkTime = null;
//    }
//}
