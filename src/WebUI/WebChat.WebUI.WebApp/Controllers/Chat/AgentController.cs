using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebChat.WebUI.Controllers.Chat
{
    [Authorize(Roles = "SupportAgent")]
    public class AgentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}