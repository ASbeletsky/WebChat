namespace WebChat.WebUI.Controllers.Chat
{
    #region Using

    using System.Web.Mvc;
    using WebChat.WebUI.WebApp.Controllers;

    #endregion
    public class ChatController : MvcBaseController
    {      
        public ActionResult ClientIndex()
        {
            return View();
        }
    
        //GET: /chat-script
        public ActionResult GetMainScript()
        {
            string mainStriptPath = "~/Scripts/Chat/main.js";
            return new FilePathResult(mainStriptPath, contentType: "application/javascript");
        }
    }
}