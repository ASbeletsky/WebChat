namespace WebChat.Business.DomainModels
{
    using Data.Models.Identity;
    using Data.Storage;
    using System.Linq;
    #region Using

    using System.Text;
    using WebChat.Data.Models.Application;
    using WebChat.WebUI.ViewModels.Application;
    using WebUI.ViewModels.Agent;
    #endregion

    public class ApplicationDomainModel : BaseDomainModel
    {
        public void CreateApplication(ApplicationViewModel app, long customerId)
        {
            var newApp = new ApplicationModel
            {
                Name = app.Name,
                CustomerId = customerId,
                WebsiteUrl = app.WebsiteUrl,
                ContactEmail = app.ContactEmail,
                Occupation = app.Occupation
            };

            Storage.Applications.Create(newApp);
            Storage.Save();
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

        public string GenerateScript(int appId)
        {
            string applicationSiteUrl = Storage.Applications.GetById(appId).WebsiteUrl;
            string mainChatScriptUrl = base.Settings.ServiceUrl.Host + "chat-script";

            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine(@"<script type='text/javascript'>")
                         .AppendLine(@"      var __chat = { };")
                         .AppendFormat("       __chat.license = {0};", appId).AppendLine()
                         .AppendFormat("       __chat.targetUrl = '{0}';", applicationSiteUrl)
                         .AppendLine(@"      localStorage.setItem('webChatAppId', __chat.license);")
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
