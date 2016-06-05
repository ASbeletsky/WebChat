using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebChat.Business.DomainModels;

namespace WebChat.WebUI.WebApp.Controllers.Site
{
    public class AppStatisticController : MvcBaseController
    {
        private readonly ApplicationDomainModel applications;
        private readonly CustomerDomainModel customers;

        public AppStatisticController()
        {
            this.applications = DependencyResolver.Current.GetService<ApplicationDomainModel>();
            this.customers = DependencyResolver.Current.GetService<CustomerDomainModel>();
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


        public JsonResult DialogsPerDay(int appId)
        {
            return JsonData(true, RenderPartialToString("_AppCharts", null), JsonRequestBehavior.AllowGet);
        }
    }
}