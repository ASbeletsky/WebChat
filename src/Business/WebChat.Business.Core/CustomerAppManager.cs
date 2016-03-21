//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.DataAccess.Abstract;
//using WebChat.DataAccess.Concrete;

//namespace WebChat.BusinessLogic.CustomerApp
//{
//    public class CustomerAppManager
//    {
//        private readonly string baseDomain;
//        private IUnitOfWork _unitOfWork;
//        private IUnitOfWork UnitOfWork
//        {
//            get
//            {
//                if (_unitOfWork == null)
//                    _unitOfWork = new EfUnitOfWork();
//                return _unitOfWork;
//            }
//        }
//        public CustomerAppManager(string baseUrl)
//        {
//            var index = baseUrl.IndexOf("://");
//            this.baseDomain = baseUrl.Substring(index + 3);
//        }
//        public string GenerateScript(int appId)
//        {
//            string targetUrl = UnitOfWork.CustomerApplications.GetById(appId).WebsiteUrl;
//            var mainScriptUrl = baseDomain + "chat-script";

//            StringBuilder scriptBuilder = new StringBuilder();
//            scriptBuilder.AppendLine  (@"<script type='text/javascript'>")
//                         .AppendLine  (@"      var __chat = { };")
//                         .AppendFormat("       __chat.license = {0};", appId).AppendLine()
//                         .AppendFormat("       __chat.targetUrl = '{0}';", targetUrl)
//                         .AppendLine  (@"      localStorage.setItem('webChatAppId', __chat.license);")
//                         .AppendLine  (@"      localStorage.setItem('webChatTargetUrl', __chat.targetUrl);")
//                         .AppendLine  (@"      (function() {")
//                         .AppendLine  (@"           var lc = document.createElement('script');")
//                         .AppendLine  (@"           lc.type = 'text/javascript'; lc.async = true;")
//                         .Append      (@"           lc.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + ")
//                         .AppendFormat(             "'{0}';", mainScriptUrl).AppendLine()
//                         .AppendLine  (@"           var s = document.getElementsByTagName('script')[0];")
//                         .AppendLine  (@"           s.parentNode.insertBefore(lc, s);")
//                         .AppendLine  (@"      })();")
//                         .AppendLine  (@"</script>");

//            string script = scriptBuilder.ToString();
//            return script;
//        }
//    }
//}
