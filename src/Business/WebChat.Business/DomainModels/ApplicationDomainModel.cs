namespace WebChat.Business.DomainModels
{

    #region Using

    using System.Text;
    using WebChat.Data.Models.Application;
    using WebChat.WebUI.ViewModels.Application;
    using WebUI.ViewModels.Agent;
    using Data.Models.Identity;
    using Data.Storage;
    using System.Linq;
    using System.Collections.Generic;
    using Services.Interfaces;
    using Chat.Stotages;
    using System;
    #endregion

    public class ApplicationDomainModel : BaseDomainModel
    {
        public ApplicationFieldsViewModel GetAppInfo(int appId)
        {
            var appModel = Storage.Applications.GetById(appId);
            return Converter.Convert<ApplicationModel, ApplicationFieldsViewModel>(appModel);
        }

        public ApplicationModel CreateApplication(ApplicationFieldsViewModel app)
        {
            var appModel = Converter.Convert<ApplicationFieldsViewModel, ApplicationModel>(app);
            appModel.Script = this.GenerateScript(appModel);

            Storage.Applications.Create(appModel);
            Storage.Save();

            return appModel;
        }

        public void EditApplication(ApplicationFieldsViewModel app)
        {
            var appModel = Storage.Applications.GetById(app.Id);
            appModel = Converter.ConvertToExisting<ApplicationFieldsViewModel, ApplicationModel>(app, appModel);
            appModel.Script = this.GenerateScript(appModel);

            Storage.Applications.Update(appModel);
            Storage.Save();
        }

        public void DeleteApplication(int appId)
        {
            Storage.Applications.Delete(appId);
            Storage.Save();
        }


        #region Application Dashboard

        public AppUsersAndChatsInfoViewModel GetUsersAndChatsInfo(int appId)
        {
            var appInfo = new AppUsersAndChatsInfoViewModel
            {
                AgentsCount = Storage.Applications.GetAgents(appId).Count(),
                ClientsCount = Storage.Applications.GetClients(appId).Count(),
                DialogsCount = Storage.Applications.GetDialogs(appId).Count(),
                MessagesCount = Storage.Applications.GetMessages(appId).Count(),
            };

            return appInfo;
        }

        #endregion

        public IList<AgentShortInfoViewModel> GetApplicationAgents(int appId)
        {
            var storedAgents = (from agent in Storage.Applications.GetAgents(appId)
                                join dialogInfo in Storage.UsersInDialogs.All on agent.Id equals dialogInfo.UserId into agentGroup
                                select new AgentShortInfoViewModel
                                {
                                    UserId = agent.Id,
                                    Name = agent.UserName,
                                    PhotoSource = "/Content/Images/default-user-image.png",
                                    DialogsCount = agentGroup.Count(),
                                    AppId = appId
                                }).ToList();

            return storedAgents;
        }

        public AgentShortInfoViewModel GetBestAgent(int appId)
        {
            var bestAgentInfo = (from dialog in Storage.Dialogs.All
                                 join dialogInfo in Storage.UsersInDialogs.All on dialog.Id equals dialogInfo.DialogId
                                 join userInRole in Storage.UsersInRoles.All on dialogInfo.UserId equals userInRole.UserId
                                 where dialog.AppId == appId && userInRole.RoleId == (long)Roles.Agent
                                 group dialogInfo by dialogInfo.UserId into userAndDialogsGroup
                                 let dialagsCount = userAndDialogsGroup.Count()
                                 let bestAgentId = userAndDialogsGroup.Key
                                 orderby dialagsCount descending
                                 select new AgentShortInfoViewModel
                                 {
                                     DialogsCount = dialagsCount,
                                     UserId = bestAgentId
                                 }).FirstOrDefault();

            if (bestAgentInfo != null)
            {
                bestAgentInfo = Storage.Users.All.Where(user => user.Id == bestAgentInfo.UserId).Select(user =>
                {
                    bestAgentInfo.Name = user.Name;
                    bestAgentInfo.PhotoSource = user.Claims.FirstOrDefault(claim => claim.ClaimType == AppClaimTypes.PhotoSource).ClaimValue;
                    return bestAgentInfo;
                }).FirstOrDefault();
            }
            return bestAgentInfo;
        }

        public string GetAppScript(int appId)
        {
            return Storage.Applications.GetById(appId).Script;
        }

        private string GenerateScript(ApplicationModel app)
        {
            string applicationSiteUrl = app.WebsiteUrl;
            string mainChatScriptUrl = base.Settings.ServiceUrl.Host + ":" + base.Settings.ServiceUrl.Port + "/chat-script";

            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine(@"<script type='text/javascript'>")
                         .AppendLine(@"      var __chat = { };")
                         .AppendFormat("       __chat.appId = {0};", app.Id).AppendLine()
                         .AppendFormat("       __chat.targetUrl = '{0}';", applicationSiteUrl)
                         .AppendLine(@"      localStorage.setItem('webChatAppId', __chat.appId);")
                         .AppendLine(@"      localStorage.setItem('webChatTargetUrl', __chat.targetUrl);")
                         .AppendLine(@"      (function() {")
                         .AppendLine(@"           var lc = document.createElement('script');")
                         .AppendLine(@"           lc.type = 'text/javascript'; lc.async = true;")
                         .Append(@"           lc.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + ")
                         .AppendFormat("'{0}';", mainChatScriptUrl).AppendLine()
                         .AppendLine(@"           var s = document.getElementsByTagName('script')[0];")
                         .AppendLine(@"           s.parentNode.insertBefore(lc, s);")
                         .AppendLine(@"      })();")
                         .AppendLine(@"</script>");

            string script = scriptBuilder.ToString();
            return script;
        }
    }
}
