using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.Entities.Identity;
using WebChat.DataAccess.Managers;
using WebChat.WebUI.Models.Сhat;
using WebChat.WebUI.Extentions;

namespace WebChat.WebUI.Controllers.Chat
{
    public class ClientController : Controller
    {
        private AppSignInManager _signInManager;
        private AppUserManager _userManager;
        private IDataService _unitOfWork;

        public ClientController(){  }
        public ClientController(AppUserManager userManager, AppSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IDataService UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = this.HttpContext.GetOwinContext().Get<IDataService>();
                return _unitOfWork;
            }
        }

        public JsonpResult CompactView()
        {
            string compactViewPath = "~/Views/Chat/Compact.cshtml";

            var ViewInJsonp = new
            {
                view = RenderHelper.PartialView(this, compactViewPath, model: null)
            };

            return new JsonpResult(ViewInJsonp);
        }

        // GET: Chat/AuthPage
        [HttpGet]
        public ActionResult AuthPage()
        {
            var model = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
                                   .Select(provider => new ExternalProviderViewModel()
                                   {
                                       Provider = provider,
                                       IconClass = "fa fa-" + provider.Caption.ToLower(),        
                                       ReferenceClass = "btn-" + provider.Caption.ToLower()
                                    });
            
            return PartialView("~/Views/Chat/AuthPage.cshtml", model);
        }

        public ActionResult ExternalLoginSuccess(string returnUrl)
        {
             ViewBag.ReturnUrl = returnUrl;
             return View("~/Views/Chat/ExternalLoginSuccess.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> EmailLogin(string appKey, string userName, string userEmail)
        {
            List<string> errors = new List<string>();
            try
            {
                AppUser user = null;
                user = await UserManager.FindByEmailAsync(userEmail);

                if (user != null)
                {
                    return await SignInAndRedirect(user);
                }                    
                else
                {
                    var emailExist = UnitOfWork.Users.All.Any(u => u.Email == userEmail);
                    if(emailExist)
                    {
                        errors.Add("Такая почта уже используется");
                    }
                    else
                    {
                        int appId = UnitOfWork.CustomerApplications.GetIdByAppKey(appKey);
                        user = new AppUser { Name = userName, UserName = userEmail, Email = userEmail };
                        var result = await UserManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            result = await UserManager.AddToRoleAsync(user.Id, "Client");
                            UnitOfWork.CustomerApplications.AddUserToApplication(user.Id, appId);
                            return await SignInAndRedirect(user);
                        }
                    }
                }
            }
            catch 
            {
                errors.Add("Ошибка сервера");
            }
            return Json(new { result = "InvalidLogin", errors = errors }, JsonRequestBehavior.AllowGet);
        }

        private async Task<ActionResult> SignInAndRedirect(AppUser user)
        {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return Json(new { result = "Redirect", url = "/chat#/chat" });
        }
    }
}