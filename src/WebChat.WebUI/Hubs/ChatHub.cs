using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using WebChat.WebUI.Models.Сhat;
using WebChat.DataAccess.Concrete.Entities.Chat;
using WebChat.BusinessLogic.Chat;
using WebChat.DataAccess.Concrete.Entities.Identity;
using WebChat.DataAccess.Abstract;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Helpers;
using WebChat.BusinessLogic.Chat.Entities;
using WebChat.DataAccess.Concrete;
using Microsoft.AspNet.Identity;
using WebChat.BusinessLogic;
using WebChat.BusinessLogic.Abstract;
using WebChat.BusinessLogic.Managers;
using Newtonsoft.Json.Linq;

namespace WebChat.WebUI.Hubs
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        private IDataService _unitOfWork;
        private ChatManager _chatManger;
        private IDataService UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = this.Context.Request.GetHttpContext().GetOwinContext().Get<IDataService>();
                return _unitOfWork;
            }
        }
        private ChatManager ChatManager
        {
            get
            {
                if (_chatManger == null)
                    _chatManger = new ChatManager();
                return _chatManger;
            }
        }

        private AgentManager AgentManager
        {
            get { return ChatManager.AgentManager; }
        }

        private DialogManager DialogManager
        {
            get { return ChatManager.DialogManager; }
        }

        /*---------------------------------------- Вспомогательные метотоды ----------------------------------------*/
        private bool IsAuthenticated
        {
            get { return this.Context.User.Identity.IsAuthenticated; }
        }
        private bool IsSupportAgent()
        {
            return Context.User.IsInRole("SupportAgent");
        }
        private bool IsClient()
        {
            return Context.User.IsInRole("Client");
        }
        private void ReturnUnAutorized()
        {
            Clients.Caller.AuthorizeRedirect();
        }

        /*--------------------------------------- Начальные Настройки ---------------------------------------------*/
        //Каждый раз при перезагрузке страницы
        public override Task OnConnected()
        {
            if (IsAuthenticated)
            {
                string userId = Context.User.Identity.GetUserId<long>().ToString();
                if (IsSupportAgent())
                {
                    if (AgentManager.IsOnline(userId))
                        SendAllAgentDialogs(userId);
                    else
                        base.Clients.Caller.RegisterAsAgent();
                }
                if (IsClient())
                {
                    ChatDialog dialog = DialogManager.FindByClientId(userId);
                    if (dialog != null)
                    {
                        UpdateDialogOfClient(dialog);
                        var messages = DialogManager.GetAllMessagesInDialog(dialog);
                        SendAllDialogMessagesToDialogMember(dialog.Id, messages);
                    }
                    else
                    {
                        Clients.Caller.BeginDialog();
                    }
                }
                else ReturnUnAutorized();
            }
            else
            {
                ReturnUnAutorized();
            }
            return base.OnConnected();
        }

/*------------------------------------------ Появление учасников -----------------------------------------*/
        public void OnNewClient(string appKey)
        {
            if (IsAuthenticated)
            {
                ChatAgent agent = AgentManager.FreeAgent();

                if (agent != null)
                {
                    ChatClient client = CreateChatClient();
                    ChatDialog dialog = DialogManager.CreateDialog(client, agent, appKey);

                    UpdateDialogOfClient(dialog);
                    AddDialogToAgent(dialog);
                }
                else
                {
                    Clients.Caller.WaitAgentRedirect();
                }
            }
            else
            {
                ReturnUnAutorized();
            }
        }

        public void OnConnectAgent()
        {
            var userId = this.Context.User.Identity.GetUserId<long>();
            if( ! AgentManager.IsOnline(userId))
            {
                var agent = new ChatAgent()
                {
                    HubConnectionId = Context.ConnectionId,
                    AsUser = UnitOfWork.Users.GetById(userId)
                };
                AgentManager.ConnectAgent(agent);
            }            
        }

        /*------------------------------------- Операции с чат сущностями ----------------------------------*/
        private ChatClient CreateChatClient()
        {
            var userId = Context.User.Identity.GetUserId<long>();
            var user = UnitOfWork.Users.GetById(userId);
            ChatClient newClient = new ChatClient(Context.ConnectionId, user);

            return newClient;
        }

        private void UpdateDialogOfClient(ChatDialog dialog)
        {
            var ClientConfig = Json.Encode(new
            {
                DialogId = dialog.Id,
                Agent = new
                {
                    Name = dialog.Agent.AsUser.Name,
                    Rank = "Support Agent",
                    PhotoUrl = dialog.Agent.GetPhotoSource() 
                }
            });
            Clients.User(dialog.Client.HubUserId).receiveDialogConfig(ClientConfig);
        }

        private void AddDialogToAgent(ChatDialog dialog)
        {
            var AgentConfig = Json.Encode(new
            {
                DialogId = dialog.Id,
                Client = new
                {
                    Id = dialog.Client.AsUser.Id,
                    Name = dialog.Client.AsUser.Name,
                    PhotoUrl = dialog.Client.GetPhotoSource()
                },
                AppKey = dialog.ApplicationKey
            });
            Clients.User(dialog.Agent.HubUserId).addDialog(AgentConfig);
        }

        private void RemoveDilalogFromAgent(ChatDialog dialog)
        {
            Clients.User(dialog.Agent.HubUserId).RemoveDialog(dialog.Id);
        }

        /*--------------------------------------- Основной функционал чата -------------------------------------------*/

        public async Task OnNewMessage(MessageDTO message)
        {
            if (IsAuthenticated)
            {
                ChatDialog dialog = DialogManager.GetById(message.DialogId);
                if (dialog != null)
                {
                    long senderId = Context.User.Identity.GetUserId<long>();
                    await DialogManager.SaveMessageToDatabaseAsync(dialog.StoredDialogId, senderId, message.Text);
                    SendMessageToDialogMembers(message, dialog);
                }
            }
            else
            {
                ReturnUnAutorized();
            }
        }
        
        public void OnAllMessageInDialog(int dialogId)
        {
            if (IsAuthenticated)
            {
                ChatDialog dialog = DialogManager.GetById(dialogId);
                if (dialog != null)
                {
                    long senderId = Context.User.Identity.GetUserId<long>();
                    var messages = DialogManager.GetAllMessagesInDialog(dialog);
                    SendAllDialogMessagesToDialogMember(dialogId, messages);
                }
            }
            else
            {
                ReturnUnAutorized();
            }
        }

        /*---------------------------------------- Рассылка ифнормации -------------------------------------*/
        private void SendMessageToDialogMembers(MessageDTO message, ChatDialog dialog)
        {
            var userId = Context.User.Identity.GetUserId<int>();
            string userName = UnitOfWork.Users.GetById(userId).Name;
            var MessageInJson = Json.Encode(new
            {
                DialogId = dialog.Id,
                UserName = userName,
                Text = message.Text,
                Time = DateTime.Now.ToShortTimeString()
            });
            Clients.Users(dialog.MembersHubUserIds).addNewMessageToPage(MessageInJson);
        }

        private void SendAllDialogMessagesToDialogMember(int chatDialogId, IEnumerable<JObject> messages)
        {
            var data = new
            {
                DialogId = chatDialogId,
                Messages = messages
            };
            Clients.Caller.UpdateMessagesInDialog(data);
        }

        private void SendAllAgentDialogs(string userId)
        {
            var agentDialogs = AgentManager.GetByUserId(userId).Dialogs.ToList();
            var agentDialogsInJson = new List<string>();
            foreach (var dialog in agentDialogs)
            {
                var dialogInJson = Json.Encode(new
                {
                    DialogId = dialog.Id,
                    Client = new
                    {
                        Id = dialog.Client.AsUser.Id,
                        Name = dialog.Client.AsUser.Name,
                        PhotoUrl = dialog.Client.GetPhotoSource()
                    },
                    AppKey = dialog.ApplicationKey
                });
                agentDialogsInJson.Add(dialogInJson);
            }
            Clients.User(userId).UpdateDialogs(agentDialogsInJson);
        }

        /*----------------------------------------- Завершение работы --------------------------------*/

        public void onEndDialog()
        {
            var clientUserId = Context.User.Identity.GetUserId<long>().ToString();
            ChatDialog dialog = DialogManager.FindByClientId(clientUserId);
            DialogManager.CloseDialog(dialog);
            RemoveDilalogFromAgent(dialog);
            DialogManager.DeleteDialog(dialog.Id);
        }
    }
}