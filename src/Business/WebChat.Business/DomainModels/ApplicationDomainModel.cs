using System;
using System.Configuration;
using System.Text;

namespace WebChat.Business.DomainModels
{
    #region Using

    #endregion

    public class ApplicationDomainModel : BaseDomainModel
    {
        public string GenerateScript(int appId)
        {
            string targetSiteUrl = Storage.Applications.GetById(appId).WebsiteUrl;
            string mainScriptUrl = base.Settings.AppUrl.Host + "chat-script";
            
            StringBuilder scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine(@"<script type='text/javascript'>")
                         .AppendLine(@"      var __chat = { };")
                         .AppendFormat("       __chat.license = {0};", appId).AppendLine()
                         .AppendFormat("       __chat.targetUrl = '{0}';", targetSiteUrl)
                         .AppendLine(@"      localStorage.setItem('webChatAppId', __chat.license);")
                         .AppendLine(@"      localStorage.setItem('webChatTargetUrl', __chat.targetUrl);")
                         .AppendLine(@"      (function() {")
                         .AppendLine(@"           var lc = document.createElement('script');")
                         .AppendLine(@"           lc.type = 'text/javascript'; lc.async = true;")
                         .Append(@"           lc.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + ")
                         .AppendFormat("'{0}';", mainScriptUrl).AppendLine()
                         .AppendLine(@"           var s = document.getElementsByTagName('script')[0];")
                         .AppendLine(@"           s.parentNode.insertBefore(lc, s);")
                         .AppendLine(@"      })();")
                         .AppendLine(@"</script>");

            string script = scriptBuilder.ToString();
            return script;
        }
    }
}
