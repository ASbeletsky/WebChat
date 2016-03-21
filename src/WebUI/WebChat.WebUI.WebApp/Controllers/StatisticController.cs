namespace WebChat.WebUI.WebApp.Controllers
{

    #region Using
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    public class BaseMVCController : Controller
    {
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