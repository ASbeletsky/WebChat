namespace WebChat.WebUI.WebApp.Controllers.Site.Account
{
    #region Using

    using AppStart;
    using Data.Storage.Managers;
    using Data.Models.Identity;
    using Data.Storage.Identity;
    using Microsoft.Owin.Security;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels.Сhat;

    #endregion

    public class ClientAccountController : AccountController
    {
        public ClientAccountController(AppUserManager userManager, AppSignInManager signInManager)
            :base(userManager, signInManager)
        {
        }

        [HttpGet]
        [AllowAnonymous]
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

        [AllowAnonymous]
        protected async override Task<ActionResult> OnRegisterSuccess(UserModel user)
        {
            await UserManager.AddToRoleAsync(user.Id, "Client");
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("ExternalLoginSuccess", "ClientAccount");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> EmailLogin(int appId, string userName, string userEmail)
        {
            var client = await UserManager.FindByEmailAsync(userEmail);
            if (client != null)
            {
                await SignInManager.SignInAsync(client, isPersistent: false, rememberBrowser: true);
                return Json(new { result = "Redirect", url = "/chat#/chat" });
            }
            else
            {
                var registerDomainModel = DependencyResolver.Current.GetService<AccountController>();
                client = new UserModel { UserName = userEmail, Email = userEmail, Name = userName };
                var result = await this.Register(client, password: null, roles: Roles.Client);
                if (result.Succeeded)
                {
                    Storage.Applications.AddUserToApplication(client.Id, appId);
                    await this.SignInManager.SignInAsync(client, isPersistent: false, rememberBrowser: false);
                    return Json(new { result = "Redirect", url = "/chat#/chat" });
                }
                else
                {
                    return Json(new { result = "InvalidLogin", errors = result.Errors }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult FacebookLogin()
        {
            return View();
        }
    }
}