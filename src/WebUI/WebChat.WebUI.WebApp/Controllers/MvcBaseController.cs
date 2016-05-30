namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.AspNet.Identity;

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
    }
}