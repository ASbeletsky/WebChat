namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System.Web.Mvc;
    using Business.DomainModels;
    using System.Collections.Generic;
    using System.Linq;
    #endregion

    [Authorize(Roles = "Customer")]
    public class CustomerAppManagementController : MvcBaseController
    {
        private readonly ApplicationDomainModel applications;
        private readonly CustomerDomainModel customers;

        public CustomerAppManagementController()
        {
            this.applications = DependencyResolver.Current.GetService<ApplicationDomainModel>();
            this.customers = DependencyResolver.Current.GetService<CustomerDomainModel>();
        }

        public long CustomerId
        {
            get { return CurrentUserId.Value; }
        }

        public ActionResult Index()
        {
            BindDropdowns();
            return View();
        }

        public JsonResult ApplicationUsersAndChatsInfo(int appId)
        {
            var appsInfo = applications.GetUsersAndChatsInfo(appId);

            return JsonData(true, RenderPartialToString("_AppUsersAndChatsInfo", appsInfo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UsersOnMap(int appId)
        {
            return JsonData(true, RenderPartialToString("_UsersOnMap", null), JsonRequestBehavior.AllowGet);
        }

        #region private members

        private void BindDropdowns()
        {
            ViewBag.CustomerApps = ApplicationsSelectList;
        }

        private IEnumerable<SelectListItem> ApplicationsSelectList
        {
            get
            {
                var apps = customers.GetCustomerApps(CustomerId).Select(app => new SelectListItem
                {
                    Value = app.Id.ToString(),
                    Text = app.Name
                });

                apps.First().Selected = true;
                return apps;
            }
        }

        #endregion

        

    }
}