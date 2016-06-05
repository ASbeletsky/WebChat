namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System.Web.Mvc;
    using Business.DomainModels;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models.Application;
    using ViewModels.Application;
    #endregion

    [Authorize(Roles = "Customer")]
    public class CustomerAppManagementController : MvcBaseController
    {
        private readonly ApplicationDomainModel applications;
        private readonly CustomerDomainModel customers;
        private IEnumerable<ApplicationModel> cusomerApplications;

        public CustomerAppManagementController()
        {
            this.applications = DependencyResolver.Current.GetService<ApplicationDomainModel>();
            this.customers = DependencyResolver.Current.GetService<CustomerDomainModel>();
        }

        public long CustomerId
        {
            get { return CurrentUserId.Value; }
        }

        public ViewResult Index(int? id)
        {
            ApplicationModel currentApp;
            if (id.HasValue)
            {
                currentApp = CustomerApps.First(app => app.Id == id.Value);
            }
            else
            {
                currentApp = CustomerApps.First();
            }

            var model = Converter.Convert<ApplicationModel, ApplicationFieldsViewModel>(currentApp);
            this.BindDropdowns();

            return View(model);
        }

        #region Application CRUD

        [HttpGet]
        public JsonResult CreateApplication()
        {
            var newApp = new ApplicationFieldsViewModel
            {
                ContactEmail = User.Identity.Name
            };

            return JsonData(true, data: RenderPartialToString("~/Views/Application/_NewModal.cshtml", newApp), behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateApplication(ApplicationFieldsViewModel app)
        {
            app.CustomerId = CustomerId;
            applications.CreateApplication(app);
            cusomerApplications = customers.GetCustomerApps(CustomerId).ToArray();
            this.BindDropdowns();

            return JsonData(isSuccess: true,
                message: "Изменения успешно сохранены.",
                data: RenderPartialToString("~/Views/Shared/Customer/_ApplicationSelector.cshtml", app));
        }

        [HttpGet]
        public JsonResult EditApplication(int appId)
        {
            var app = applications.GetAppInfo(appId);
            return JsonData(true, data: RenderPartialToString("~/Views/Application/_EditModal.cshtml", app), behavior: JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditApplication(ApplicationFieldsViewModel app)
        {
            app.CustomerId = CustomerId;
            applications.EditApplication(app);
            return JsonData(true, message: "Изменения успешно сохранены", data: RenderPartialToString("_AppInfo", app));
        }

        [HttpGet]
        public JsonResult DeleteApplication(int appId)
        {
            applications.DeleteApplication(appId);
            return JsonData(true, data: null, behavior: JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpGet]
        public JsonResult ApplicationScript(int appId)
        {
            var appScript = applications.GetAppScript(appId);
            return JsonData(true, data: RenderPartialToString("~/Views/Application/_ScriptModal.cshtml", appScript), behavior: JsonRequestBehavior.AllowGet);
        }

        #region private members

        private void BindDropdowns()
        {
            ViewBag.CustomerApps = ApplicationsSelectList;
        }

        private IEnumerable<ApplicationModel> CustomerApps
        {
            get
            {
                if (cusomerApplications == null)
                {
                    cusomerApplications = customers.GetCustomerApps(CustomerId).ToArray();
                }

                return cusomerApplications;
            }
        }

        private IEnumerable<SelectListItem> ApplicationsSelectList
        {
            get
            {
                var apps = CustomerApps.Select(app => new SelectListItem
                {
                    Value = app.Id.ToString(),
                    Text = app.WebsiteUrl
                });

                return apps;
            }
        }

        #endregion



    }
}