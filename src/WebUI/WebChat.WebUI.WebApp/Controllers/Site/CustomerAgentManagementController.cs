namespace WebChat.WebUI.WebApp.Controllers.Site
{
    #region Using

    using System.Collections.Generic;
    using System.Web.Mvc;
    using WebChat.WebUI.ViewModels;

    #endregion
    public class CustomerAgentManagementController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Menu = this.Menu;
            return View();
        }

        public ActionResult AgentsList()
        {
            return View();
        }

        #region private members

        private IEnumerable<MenuItem> Menu
        {
            get
            {
                return new List<MenuItem>()
                    {
                        new MenuItem {Labor = "Все операторы", Link = Url.Action("AgentsList")  },
                        new MenuItem {Labor = "Активности операторов" }
                    };
            }
        }

        #endregion
    }
}