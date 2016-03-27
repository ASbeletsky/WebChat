﻿namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using BusinessLogic.DomainModels;
    using Services.Interfaces;
    using Services.Common;
    #endregion

    public class BaseMvcController : Controller
    {

        /// <summary>
        /// Overridden OnException method to catch server exceptions. If the request is AJAX return JSON else redirect user to Error view.
        /// </summary>
        /// <param name="filterContext">Exception context.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { error = true, message = filterContext.Exception.Message }
                };
            }
            else
            {
                filterContext.Result = this.View("Error", new HandleErrorInfo(filterContext.Exception,
                    filterContext.RouteData.Values["controller"].ToString(),
                    filterContext.RouteData.Values["action"].ToString()));
            }

            base.OnException(filterContext);
        }

        protected void AddModelErrors(IEnumerable<string> errors)
        {
            foreach(var error in errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        protected void AddModelErrors(Exception ex)
        {
            this.ModelState.AddModelError("", ex.Message);
        }
    }
}