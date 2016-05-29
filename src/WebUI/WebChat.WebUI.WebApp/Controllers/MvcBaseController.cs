namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;

    #endregion

    public class MvcBaseController : Controller
    {
        protected long CurrentUserId
        {
            get
            {
                return long.Parse(User.Identity.GetUserId());
            }
        }
    }
}