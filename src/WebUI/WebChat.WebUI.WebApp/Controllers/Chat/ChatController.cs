namespace WebChat.WebUI.Controllers.Chat
{
    using Data.Interfaces;
    using Services.Interfaces;
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

        [Authorize(Roles = "Agent")]
        public ActionResult AgentIndex()
        {
            var defaultApp = DependencyContainer.Current.GetService<IDataStorage>().UsersInApplication.Find(agent => agent.UserId == CurrentUserId.Value);          
            return View(defaultApp);
        }

        //GET: /chat-script
        public ActionResult GetMainScript()
        {
            string mainStriptPath = "~/Scripts/Chat/main.js";
            return new FilePathResult(mainStriptPath, contentType: "application/javascript");
        }
    }
}