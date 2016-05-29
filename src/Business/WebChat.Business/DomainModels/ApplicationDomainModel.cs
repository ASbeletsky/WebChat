namespace WebChat.Business.DomainModels
{
    #region Using

    using System.Text;
    using WebChat.Data.Models.Application;
    using WebChat.WebUI.ViewModels.Application;

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
