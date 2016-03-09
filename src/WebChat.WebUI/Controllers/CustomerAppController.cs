using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace WebChat.WebUI.Controllers
{
    public class CustomerAppController : Controller
    {
        //GET: /chat-script
        public ActionResult GetMainScript()
        {
            string mainStriptPath = "~/Scripts/Chat/main.js";
            return new FilePathResult(mainStriptPath, contentType: "application/javascript");
        }
    }
}
