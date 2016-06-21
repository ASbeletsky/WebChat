namespace WebChat.WebUI.Controllers.Chat
{
    #region Using

    using Microsoft.Owin.Security;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using WebChat.WebUI.WebApp.Controllers;
    using WebChat.WebUI.WebApp.Extentions;
    using System.Web.Mvc;
    using WebChat.WebUI.ViewModels.Сhat;
    using WebChat.Data.Storage.Identity;
    using WebChat.Data.Models.Identity;
    using Data.Interfaces;
    #endregion

    public class ClientController : MvcBaseController
    {
        private IDataStorage Storage
        {
            get { return DependencyResolver.Current.GetService<IDataStorage>(); }
        }

        public JsonpResult CompactView()
        {
            return new JsonpResult(RenderPartialToString("~/Views/Chat/Compact.cshtml", model: null));
        }


        [HttpGet]
        public ActionResult AuthPage()
        {
            var model = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
                                   .Select(provider => new ExternalProviderViewModel()
                                   {
                                       ProviderAuthenticationType = provider.AuthenticationType,
                                       IconClass = "fa fa-" + provider.Caption.ToLower(),
                                       ReferenceClass = "btn-" + provider.Caption.ToLower()
                                   });

            return PartialView("~/Views/Chat/_AuthExternalLogin.cshtml", model);
        }

        public ActionResult ExternalLoginSuccess(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("~/Views/Chat/ExternalLoginSuccess.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> EmailLogin(int appId, string userName, string userEmail)
        {
            var registerDomainModel = DependencyResolver.Current.GetService<AccountController>();
            var client = new UserModel { UserName = userEmail, Email = userEmail, Name = userName };
            var result = await registerDomainModel.Register(client, password: null, roles: Roles.Client);
            if (result.Succeeded)
            {
                Storage.Applications.AddUserToApplication(client.Id, appId);
                await registerDomainModel.SignInManager.SignInAsync(client, isPersistent: false, rememberBrowser: false);
                return Json(new { result = "Redirect", url = "/chat#/chat" });
            }
            else
            {
                return Json(new { result = "InvalidLogin", errors = result.Errors }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}