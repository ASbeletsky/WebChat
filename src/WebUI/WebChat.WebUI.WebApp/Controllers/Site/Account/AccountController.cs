namespace WebChat.WebUI.WebApp.Controllers.Site.Account
{
    #region Using

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using WebChat.Data.Storage.Managers;
    using WebChat.Data.Storage.Identity;
    using WebChat.Data.Interfaces;
    using WebChat.WebUI.ViewModels.Shared;
    using WebChat.Data.Models.Application;
    using WebChat.WebUI.ViewModels.Customer;
    using WebChat.WebUI.ViewModels.Agent;
    using WebChat.Data.Models.Identity;
    using WebChat.WebUI.WebApp.AppStart;
    using ViewModels.Application;
    using Business.DomainModels;
    using Facebook;
    using TweetSharp;
    using Services.Common.Settings;
    using Services.Interfaces;
    using Services.Interfaces.Settings;
    using System.Security.Claims;
    #endregion

    [Authorize]
    public class AccountController : Controller
    {
        private AppSignInManager signInManager;
        private AppUserManager userManager;
        private IDataStorage unitOfWork;

        public AccountController(AppUserManager userManager, AppSignInManager signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Managers And Storage

        protected AppSignInManager SignInManager
        {
            get
            {
                if (signInManager == null)
                    signInManager = DependencyResolver.Current.GetService<AppSignInManager>();
                return signInManager;
            }
        }

        protected AppUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = DependencyResolver.Current.GetService<AppUserManager>();
                return userManager;
            }
        }

        protected IDataStorage Storage
        {
            get
            {
                if (unitOfWork == null)
                    unitOfWork = DependencyResolver.Current.GetService<IDataStorage>();
                return unitOfWork;
            }
        }

        protected IAuthSettings AuthSettings
        {
            get
            {
                return DependencyResolver.Current.GetService<IAuthSettings>();
            }
        }

        #endregion Managers And Storage

        #region Shared

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        protected virtual ActionResult OnSuccessLogin(string returnUrl)
        {
            return RedirectToLocal(returnUrl);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                       return this.OnSuccessLogin(returnUrl);                    
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Не верный адресс эл. почты или пароль.");
                    return View(model);
            }
        }

        protected async Task<IdentityResult> Register(UserModel user, string password = null, params Roles[] roles)
        {
            IdentityResult registerResult = null;
            bool emailExists = UserManager.Users.Any(o => o.Email == user.Email);

            if (emailExists)
            {
                registerResult = new IdentityResult("Пользователь с таким Email уже зарегистрирован");
            }
            else
            {
                if(password == null)
                    registerResult = await UserManager.CreateAsync(user);
                else
                    registerResult = await UserManager.CreateAsync(user, password);

                if (registerResult.Succeeded)
                {
                    foreach (var role in roles)
                    {
                        registerResult = await UserManager.AddToRoleAsync(user.Id, role.ToString());
                    }
                }
            }

            return registerResult;
        }

        #endregion

        #region Agent

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOperator()
        {
            var customerId = User.Identity.GetUserId<long>();
            var model = new RegisterOperatorViewModel
            {
                CustomerApps = Storage.Applications.GetCustomerApplications(customerId)
            };

            return View("~/Views/Owner/RegisterAgent.cshtml", model);
        }

        // POST: /Account/RegisterOperator
        [HttpPost]
        public async Task<ActionResult> RegisterAgent(RegisterOperatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var agent = new UserModel
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    RegistrationDate = DateTime.Today
                };

                var registerResult = await this.Register(agent, model.Password, Roles.Agent);

                if (registerResult.Succeeded)
                {
                    registerResult = await UserManager.AddPhotoAsync(agent.Id, model.PhotoSrc);

                    foreach (var appId in model.SelectedApps)
                    {
                        Storage.Applications.AddUserToApplication(agent.Id, appId);                        
                    }
                    Storage.Save();
                    return RedirectToAction("OwnerAgents", "Owner");
                }
                else
                {
                    AddErrors(registerResult);
                }
            }

            var customerId = User.Identity.GetUserId<long>();
            model.CustomerApps = Storage.Applications.GetCustomerApplications(customerId);

            return View("~/Views/Owner/RegisterAgent.cshtml", model);
        }

        #endregion

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ExternalLogin
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Запрос перенаправления к внешнему поставщику входа
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            // Выполнение входа пользователя посредством данного внешнего поставщика входа, если у пользователя уже есть имя входа
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("ExternalLoginSuccess", "Client");
                case SignInStatus.LockedOut:
                    return View("Lockout");            
                case SignInStatus.Failure:
                default:
                    // Если у пользователя нет учетной записи, то ему предлагается создать ее
                    return RedirectToAction("CreateUserByExternalInfo", new { Provider = loginInfo.Login.LoginProvider });
            }
        }

        [AllowAnonymous]
        public virtual async Task<ActionResult> CreateUserByExternalInfo(string provider)
        {
            string userName, email = "N/A", photoUrl = null, link = null;

            var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
            userName = identity.Name;
            switch (provider)
            {
                case "Facebook":
                    {
                        var accessToken = identity.FindFirstValue("FacebookAccessToken");
                        var fb = new FacebookClient(accessToken);
                        dynamic myInfo = fb.Get("/me?fields=email,picture");
                        email = myInfo.email;
                        photoUrl = string.Format("https://graph.facebook.com/{0}/picture", myInfo.id);
                        link = "https://www.facebook.com/" + myInfo.id;
                    }
                    break;
                case "Twitter":
                    {
                        var tw = new TwitterService
                            (
                                consumerKey: this.AuthSettings.TwitterSettings.AppSecret,
                                consumerSecret: this.AuthSettings.TwitterSettings.AppSecret,
                                token: identity.FindFirstValue("TwitterAccessToken"),
                                tokenSecret: identity.FindFirstValue("TwitterAccessTokenSecret")
                            );

                        var profile = tw.GetUserProfile(new GetUserProfileOptions());
                        if (profile != null)
                        {
                            userName = profile.ScreenName;
                            photoUrl = profile.ProfileImageUrl;
                            link = "https://twitter.com/" + userName;
                        }
                    }
                    break;
            }

            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            var user = new UserModel { Name = userName, UserName = userName, Email = email };
            var result = await UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                if (result.Succeeded)
                {
                    return await this.OnRegisterSuccess(user);                                    
                }
            }

            if (result.Errors.Count() == 1)
            {
                user = UserManager.FindByEmail(email);
                UserManager.AddLogin(user.Id, loginInfo.Login);
            }

            UserManager.AddClaim(user.Id, new Claim("PhotoUrl", photoUrl));
            UserManager.AddClaim(user.Id, new Claim("SocialNetworkLink:" + provider, link));

            return RedirectToAction("Home/Index");
        }

        protected async virtual Task<ActionResult> OnRegisterSuccess(UserModel user)
        {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("Index", "Home");
        }


        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalBearer);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (userManager != null)
                {
                    userManager.Dispose();
                    userManager = null;
                }

                if (signInManager != null)
                {
                    signInManager.Dispose();
                    signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };                
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}