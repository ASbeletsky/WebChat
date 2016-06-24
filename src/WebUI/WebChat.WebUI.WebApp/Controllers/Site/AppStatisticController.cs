using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.Business.DomainModels;
using WebChat.WebUI.ViewModels.Statistic;

namespace WebChat.WebUI.WebApp.Controllers.Site
{
    public class AppStatisticController : MvcBaseController
    {
        private readonly ApplicationDomainModel applications;
        private readonly CustomerDomainModel customers;
        private readonly ApplicationStatisticDomainModel statistic;

        public AppStatisticController(ApplicationDomainModel applications, CustomerDomainModel customers, ApplicationStatisticDomainModel statistic)
        {
            this.applications = applications;
            this.customers = customers;
            this.statistic = statistic;
        }

        public ActionResult Index()
        {
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

        public JsonResult UsersStatistic(int appId)
        {
            return JsonData(true, RenderPartialToString("_UsersStatistic", null), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DialogsPerDay(int appId)
        {
            var model = new AppStatisticTimePeriodViewModel
            {
                AppId = appId,
                StartDate = DateTime.Today.AddDays(-7),
                EndDate = DateTime.Today
            };

            return JsonData(true, RenderPartialToString("_DialogsPerDay", model), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DialogsPerDay(AppStatisticTimePeriodViewModel model)
        {
            var data = statistic.GetDialogsPerDay(model);
            return JsonData(true, data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DialogsDurationPerDay(int appId)
        {
            var model = new AppStatisticTimePeriodViewModel
            {
                AppId = appId,
                StartDate = DateTime.Today.AddDays(-7),
                EndDate = DateTime.Today
            };

            return JsonData(true, RenderPartialToString("_DialogsDurationPerDay", model), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DialogsDurationPerDay(AppStatisticTimePeriodViewModel model)
        {
            var data = statistic.GetDialogsDurationPerDay(model);
            return JsonData(true, data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MessageCountDifferencePage(int appId)
        {
            return JsonData(true, RenderPartialToString("_MessageCountDifference", appId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MessageCountDifferenceChart(int appId)
        {
            var data = statistic.GetMessageCountDifference(appId);
            return JsonData(true, data, JsonRequestBehavior.AllowGet);
        }
    }
}