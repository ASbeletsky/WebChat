namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.AspNet.Identity;
    using System.IO;
    #endregion

    public class MvcBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!CurrentUserId.HasValue)
            {
                var user = filterContext.HttpContext.User;
                if (user != null && user.Identity.IsAuthenticated)
                {
                    CurrentUserId = user.Identity.GetUserId<long>();
                }
            }
        }

        protected long? CurrentUserId
        {
            get;
            private set;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            if(filterContext.Result is JsonResult)
            {
                string errorMessage = string.Format("Error: {0}", filterContext.Exception.Message);
                filterContext.Result = JsonData(false, errorMessage, null, JsonRequestBehavior.AllowGet);
            }
        }

        protected JsonResult JsonData(bool isSuccess, object data, JsonRequestBehavior behavior)
        {
            return JsonData(isSuccess, null, data, behavior);
        }

        protected JsonResult JsonData(bool isSuccess, string message,  object data, JsonRequestBehavior behavior)
        {
            return Json(new
            {
                isSuccess = isSuccess,
                Data = data,
                Мessage = message
            }, behavior);
        }

        public string RenderPartialToString(string viewName, object model)
        {
            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(this.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
    }
}