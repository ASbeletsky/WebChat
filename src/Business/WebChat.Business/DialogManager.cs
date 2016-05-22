//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Chat.Entities;
//using WebChat.BusinessLogic.Chat.Storages;
//using WebChat.DataAccess.Abstract;
//using WebChat.DataAccess.Concrete;
//using WebChat.DataAccess.Concrete.Entities.Chat;
//using WebChat.DataAccess.Concrete.Entities.Identity;

//namespace WebChat.BusinessLogic.Managers
//{
//    public class DialogManager
//    {
//        private IUnitOfWork _unitOfWork;
//        private static DialogsStorage DialogsStorage { get; set; }
//        static DialogManager()
//        {
//            DialogsStorage = DialogsStorage.Instance;
//        }
//        private IUnitOfWork UnitOfWork
//        {
//            get
//            {
//                if (_unitOfWork == null)
//                    _unitOfWork = new EfUnitOfWork();
//                return _unitOfWork;
//            }
//        }

//        public Dialog FindByClientId(string userId)
//        {
//            return DialogsStorage.Find(dialog => dialog.Client.HubUserId == userId);
//        }
//        public Dialog GetById(int id)
//        {
//            return DialogsStorage.GetById(id);
//        }

//        public Dialog CreateDialog(ChatClient client, ChatAgent agent, string AppKey)
//        {
//            Dialog dilog = new Dialog()
//            {
//                Id = DialogsStorage.GenerateId(),
//                Client = client,
//                Agent = agent,
//                ApplicationKey = AppKey,
//            };

//            Dialog storedDialog = new Dialog()
//            {
//                StartedAt = DateTime.Now,
//                ClosedAt = DateTime.Now,
//                Users = new AppUser[] { client.AsUser, agent.AsUser }
//            };

//            UnitOfWork.Dialogs.Create(storedDialog);
//            dilog.StoredDialogId = storedDialog.Id;         
//            DialogsStorage.Add(dilog);

//            return dilog;
//        }

//        public async Task SaveMessageToDatabaseAsync(int StoredDialogId, long SenderId, string messageText)
//        {
//            Message storedMessage = new Message()
//            {
//                Dialog_id = StoredDialogId,
//                SendedAt = DateTime.Now,
//                Sender_id = SenderId,
//                Text = messageText
//            };
//            await Task.Run(() => UnitOfWork.Messages.Create(storedMessage));
//        }

//        public IEnumerable<JObject> GetAllMessagesInDialog(Dialog chatDialog)
//        {
//            var messages = UnitOfWork.Messages.GetAllMessagesInDialog(chatDialog.StoredDialogId).ToList()
//                                     .Select(message => JObject.FromObject(new
//                                     {
//                                         DialogId = message.DialogId,
//                                         UserName = message.UserName,
//                                         Text = message.Text,
//                                         Time = message.Time.ToShortTimeString()
//                                     }));
                                        
                
//            return messages;
//        }

//        public void CloseDialog(Dialog dialog)
//        {
//            Dialog storedDialog = UnitOfWork.Dialogs.GetById(dialog.StoredDialogId);
//            storedDialog.ClosedAt = DateTime.Now;
//            UnitOfWork.Dialogs.Update(storedDialog);           
//        }

//        public void DeleteDialog(int dialogId)
//        {
//            DialogsStorage.Delete(dialogId);
//        }
//    }
//}
