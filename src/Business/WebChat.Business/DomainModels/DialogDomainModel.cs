namespace WebChat.Business.DomainModels
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebChat.Business.Chat.Entities;
    using WebChat.Business.Chat.Stotages;
    using WebChat.Data.Models.Chat;
    using WebChat.WebUI.ViewModels.Сhat;

    #endregion

    public class DialogDomainModel : BaseDomainModel
    {
        private static DialogStorage Dialogs { get; set; }
        static DialogDomainModel()
        {
            Dialogs = DialogStorage.Instance;
        }

        public ChatDialog FindByClientId(string userId)
        {
            return Dialogs.Find(dialog => dialog.Client.HubUserId == userId);
        }
        public ChatDialog GetById(int id)
        {
            return Dialogs.GetById(id);
        }

        public ChatDialog CreateDialog(ChatClient client, ChatAgent agent, int appId)
        {
            ChatDialog dilog = new ChatDialog(appId, client, agent)
            {
                Id = Dialogs.GenerateId(),
            };

            DialogModel storedDialog = new DialogModel()
            {
                StartedAt = DateTime.Now,
                ClosedAt = DateTime.Now,
                AppId = appId
            };

            Storage.Dialogs.Create(storedDialog);

            var clietDialog = new UsersInDialogsModel { UserId = client.UserId, DialogId = storedDialog.Id };
            var agentDialog = new UsersInDialogsModel { UserId = agent.UserId, DialogId = storedDialog.Id };

            Storage.UsersInDialogs.Create(clietDialog);
            Storage.UsersInDialogs.Create(agentDialog);

            Storage.Save();

            dilog.StoredDialogId = storedDialog.Id;
            Dialogs.Add(dilog);

            return dilog;
        }

        public async Task SaveMessageToDatabaseAsync(int StoredDialogId, long SenderId, string messageText)
        {
            MessageModel storedMessage = new MessageModel()
            {
                DialogId = StoredDialogId,
                SendedAt = DateTime.Now,
                SenderId = SenderId,
                Text = messageText
            };
            await Task.Run(() => { Storage.Messages.Create(storedMessage); Storage.Save(); });         
        }

        public IEnumerable<MessageViewModel> GetAllMessagesInDialog(ChatDialog chatDialog)
        {
            var messages = Storage.Dialogs.GetMessages(chatDialog.StoredDialogId);       
            return messages;
        }

        public void CloseDialog(ChatDialog dialog)
        {
            DialogModel storedDialog = Storage.Dialogs.GetById(dialog.StoredDialogId);
            storedDialog.ClosedAt = DateTime.Now;
            Storage.Dialogs.Update(storedDialog);
            Storage.Save();
        }

        public void DeleteDialog(int dialogId)
        {
            Dialogs.Delete(dialogId);
        }
    }
}
