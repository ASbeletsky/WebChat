namespace WebChat.WebUI.WebApp.Controllers.Site
{

    #region Using

    using Business.DomainModels;
    using Services.Interfaces;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels.Agent;
    using ViewModels.Shared;
    #endregion
    public class CustomerAgentManagementController : MvcBaseController
    {
        private CustomerDomainModel customerDomainModel;

        public CustomerAgentManagementController(CustomerDomainModel customerDomainModel)
        {
            this.customerDomainModel = customerDomainModel;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AgentsList()
        {
            var application = DependencyContainer.Current.GetService<ApplicationDomainModel>();
            var customer = DependencyContainer.Current.GetService<CustomerDomainModel>();
            var customerAppsIds = customer.GetCustomerApps(CurrentUserId.Value).Select(app => app.Id).ToList();
            var agents = customerAppsIds.SelectMany(appId => application.GetApplicationAgents(appId));

            return JsonData(true, RenderPartialToString("_AgentsList", agents), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AddAgent()
        {
            var model = new RegisterAgentViewModel();
            BindDropdowns();

            return JsonData(true, RenderPartialToString("_AddAgent", model), JsonRequestBehavior.AllowGet);
        }

        #region private members

        private void BindDropdowns()
        {
            ViewBag.CustomerApps = customerDomainModel.GetCustomerApps(CurrentUserId.Value).Select(app => new SelectListItem
            {
                Value = app.Id.ToString(),
                Text = app.WebsiteUrl
            });
        }

        #endregion
    }
}