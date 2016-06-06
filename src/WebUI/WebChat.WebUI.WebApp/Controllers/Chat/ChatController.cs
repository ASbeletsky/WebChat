using System.Web.Mvc;
using WebChat.WebUI.WebApp.Controllers;
using WebChat.WebUI.WebApp.Extentions;

namespace WebChat.WebUI.Controllers.Chat
{
    public class ChatController : MvcBaseController
    {      
        public ActionResult ClientIndex()
        {
            return View();
        }

        public JsonpResult CompactView()
        {
            return new JsonpResult(new
            {
                IsSuccess = true,
                Data = RenderPartialToString("~/Views/Chat/Compact.cshtml", model: null)
            });
        }

        //GET: /chat-script
        public ActionResult GetMainScript()
        {
            string mainStriptPath = "~/Scripts/Chat/main.js";
            return new FilePathResult(mainStriptPath, contentType: "application/javascript");
        }
    }
}