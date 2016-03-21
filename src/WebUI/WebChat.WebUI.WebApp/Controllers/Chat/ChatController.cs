using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.Entities.Identity;
using WebChat.DataAccess.Managers;
using WebChat.WebUI.Models.Сhat;

namespace WebChat.WebUI.Controllers.Chat
{
    public class ChatController : Controller
    {      
        public ActionResult ClientIndex()
        {
            return View();
        }
    }
}