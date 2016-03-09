using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebChat.BusinessLogic.Managers;
using WebChat.DataAccess.Concrete.Entities.Identity;
using WebChat.WebUI.Models.Сhat;

namespace WebChat.WebUI.Controllers.Chat
{
    public class ChatStartPageController : Controller
    {
        private AppSignInManager _signInManager;
        private AppUserManager _userManager;

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

        public ChatStartPageController()
        {
        }
        public ChatStartPageController(AppUserManager userManager, AppSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult CompactStartPage()
        {
            return View("~/Views/Chat/StartPage/CompactStartPage.cshtml");
        }

        // GET: ChatStartPage
        [HttpGet]
        public ActionResult StartPageFull()
        {
            var model = new ChatStartPageViewModel()
            {
                loginProviders = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
            };

            return View("~/Views/Chat/StartPage/StartPageFull.cshtml", model);
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmailLogin(ChatStartPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                    return await SignInAndRedirect(user);
                
                user = new AppUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    return await SignInAndRedirect(user);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            model.loginProviders = HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
            return View("~/Views/Chat/StartPage/StartPageFull.cshtml", model);
        }

        private async Task<ActionResult> SignInAndRedirect(AppUser user)
        {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("ChatMainPage", "ChatMainPageController");
        }
    }
}