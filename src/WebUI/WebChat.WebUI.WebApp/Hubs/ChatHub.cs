namespace WebChat.WebUI.WebApp.Hubs
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.SignalR;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using WebChat.Business.DomainModels;
    using WebChat.Services.Interfaces;
    using WebChat.Business.Chat.Entities;
    using WebChat.WebUI.ViewModels.Сhat;
    using WebChat.Data.Interfaces;

    #endregion
    public class ChatHub : Hub
    {
        private readonly DialogDomainModel dialogs;
        private readonly AgentDomainModel agents;
        private IDataStorage Stoage { get; set; }
        private IEntityConverter Converter { get; set; }

        public ChatHub()
        {
            this.dialogs = DependencyContainer.Current.GetService<DialogDomainModel>();
            this.agents = DependencyContainer.Current.GetService<AgentDomainModel>();
            this.Stoage = DependencyContainer.Current.GetService<IDataStorage>();
            this.Converter = DependencyContainer.Current.GetService<IEntityConverter>();
        }

        /*---------------------------------------- Вспомогательные метотоды ----------------------------------------*/
        private bool IsAuthenticated
        {
            get { return this.Context.User.Identity.IsAuthenticated; }
        }
        private bool IsSupportAgent()
        {
            return Context.User.IsInRole("Agent");
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
                    if (agents.IsOnline(userId))
                        SendAllAgentDialogs(userId);
                    else
                        base.Clients.Caller.RegisterAsAgent();
                }
                if (IsClient())
                {
                    ChatDialog dialog = dialogs.FindByClientId(userId);
                    if (dialog != null)
                    {
                        UpdateDialogOfClient(dialog);
                        var messages = dialogs.GetAllMessagesInDialog(dialog);
                        SendAllDialogMessagesToDialogMember(dialog.Id, messages);
                    }
                    else
                    {
                        Clients.Caller.BeginDialog();
                    }
                }             
            }
            else
            {
                ReturnUnAutorized();
            }
            return base.OnConnected();
        }

        /*------------------------------------------ Появление учасников -----------------------------------------*/
        public void OnNewClient(int appId)
        {
            if (IsAuthenticated)
            {
                ChatAgent agent = agents.FreeAgent(appId);

                if (agent != null)
                {
                    ChatClient client = CreateChatClient(appId);
                    ChatDialog dialog = dialogs.CreateDialog(client, agent, appId);

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

        public void OnConnectAgent(int appId)
        {
            var userId = this.Context.User.Identity.GetUserId<long>();
            if (!agents.IsOnline(userId))
            {
                var agent = new ChatAgent(userId, appId);
                agent.StartWorkAt = DateTime.Now;
                agents.ConnectAgent(agent);
            }
        }

        /*------------------------------------- Операции с чат сущностями ----------------------------------*/
        private ChatClient CreateChatClient(int appId)
        {
            var userId = Context.User.Identity.GetUserId<long>();
            var user = Stoage.Users.GetById(userId);
            ChatClient newClient = new ChatClient(userId, appId);

            return newClient;
        }

        private void UpdateDialogOfClient(ChatDialog dialog)
        {
            var ClientConfig = Converter.ConvertToJson(new
            {
                DialogId = dialog.Id,
                Agent = new
                {
                    Name = Stoage.Users.All.Where(u => u.Id == dialog.Agent.UserId).Select(u => u.Name).FirstOrDefault(),
                    PhotoUrl = "/Content/Images/default-user-image.png"
                },
                AppId  = dialog.ApplicationId 
            });
            Clients.User(dialog.Client.HubUserId).receiveDialogConfig(ClientConfig);
        }

        private void AddDialogToAgent(ChatDialog dialog)
        {
            var AgentConfig = Converter.ConvertToJson(new
            {
                DialogId = dialog.Id,
                Client = new
                {
                    Id = dialog.Client.UserId,
                    Name = Stoage.Users.All.Where(u => u.Id == dialog.Client.UserId).Select(u => u.Name).FirstOrDefault(),
                    PhotoUrl = "/Content/Images/default-user-image.png"
                },
                AppId = dialog.ApplicationId
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
                ChatDialog dialog = dialogs.GetById(message.DialogId);
                if (dialog != null)
                {
                    long senderId = Context.User.Identity.GetUserId<long>();
                    await dialogs.SaveMessageToDatabaseAsync(dialog.StoredDialogId, senderId, message.Text);
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
                ChatDialog dialog = dialogs.GetById(dialogId);
                if (dialog != null)
                {
                    long senderId = Context.User.Identity.GetUserId<long>();
                    var messages = dialogs.GetAllMessagesInDialog(dialog);
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
            string userName = Stoage.Users.GetById(userId).Name;
            var MessageInJson = Converter.ConvertToJson(new
            {
                DialogId = dialog.Id,
                UserName = userName,
                Text = message.Text,
                Time = DateTime.Now.ToShortTimeString()
            });
            Clients.Users(dialog.MembersHubUserIds).addNewMessageToPage(MessageInJson);
        }

        private void SendAllDialogMessagesToDialogMember(int chatDialogId, IEnumerable<MessageViewModel> messages)
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
            var agentDialogs = agents.GetByUserId(userId).Dialogs;
            var agentDialogsInJson = new List<string>();

            if (agentDialogs != null && agentDialogs.Any())
            {
                foreach (var dialog in agentDialogs)
                {
                    var dialogInJson = Converter.ConvertToJson(new
                    {
                        DialogId = dialog.Id,
                        Client = new
                        {
                            Id = dialog.Client.UserId,
                            Name = Stoage.Users.All.Where(u => u.Id == dialog.Client.UserId).Select(u => u.Name).FirstOrDefault(),
                            PhotoUrl = "/Content/Images/default-user-image.png"
                        },
                        AppId = dialog.ApplicationId
                    });
                    agentDialogsInJson.Add(dialogInJson);
                }
            }
            
            Clients.User(userId).UpdateDialogs(agentDialogsInJson);
        }

        /*----------------------------------------- Завершение работы --------------------------------*/

        public void onEndDialog()
        {
            var clientUserId = Context.User.Identity.GetUserId<long>().ToString();
            ChatDialog dialog = dialogs.FindByClientId(clientUserId);
            dialogs.CloseDialog(dialog);
            RemoveDilalogFromAgent(dialog);
            dialogs.DeleteDialog(dialog.Id);
        }
    }
}