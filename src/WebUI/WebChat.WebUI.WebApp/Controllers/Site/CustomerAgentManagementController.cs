namespace WebChat.WebUI.WebApp.Controllers.Site
{
    #region Using

    using System.Collections.Generic;
    using System.Web.Mvc;
    using WebChat.WebUI.ViewModels;

    #endregion
    public class CustomerAgentManagementController : MvcBaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AgentsList()
        {
            return JsonData(true, RenderPartialToString("_AgentsList", null), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgentsActivity()
        {
            return JsonData(true, RenderPartialToString("_AgentsActivity", null), JsonRequestBehavior.AllowGet);
        }

        #region private members
        

        #endregion
    }
}