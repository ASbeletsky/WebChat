namespace WebChat.WebUI.Controllers
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
    using WebChat.Data.Data.Managers;
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

        public AppSignInManager SignInManager
        {
            get
            {
                if (signInManager == null)
                    signInManager = DependencyResolver.Current.GetService<AppSignInManager>();
                return signInManager;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = DependencyResolver.Current.GetService<AppUserManager>();
                return userManager;
            }
        }

        private IDataStorage UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                    unitOfWork = DependencyResolver.Current.GetService<IDataStorage>();
                return unitOfWork;
            }
        }

        #endregion Managers And Storage

        #region Shared

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Url.Action("MyApplications", "Customer");
            return View();
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
                    return RedirectToLocal(returnUrl);
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

        private async Task<IdentityResult> Register(UserModel user, string password, params Roles[] roles)
        {
            IdentityResult registerResult = null;
            bool emailExists = UserManager.Users.Any(o => o.Email == user.Email);

            if (emailExists)
            {
                registerResult = new IdentityResult("Пользователь с таким Email уже зарегистрирован");
            }
            else
            {
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

        #region Customer

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCustomer(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new UserModel
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    RegistrationDate = DateTime.Today
                };

                var registerResult = await this.Register(customer, model.Password, Roles.Customer);

                if (registerResult.Succeeded)
                {
                    await SignInManager.SignInAsync(customer, isPersistent: false, rememberBrowser: false);
                    //TODO app domain model create app
                    return RedirectToAction("OwnerInfo", "Owner");
                }

                AddErrors(registerResult);
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterCustomerAndFirstApp(string email)
        {
            var model = new CustomerAndAppViewModel()
            {
                Customer = new RegisterViewModel
                {
                    Email = email
                },
                App = new ApplicationViewModel
                {
                    ContactEmail = email
                }
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCustomerAndFirstApp(CustomerAndAppViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = new UserModel
                {
                    Name = model.Customer.Name,
                    UserName = model.Customer.Email,
                    Email = model.Customer.Email,
                    RegistrationDate = DateTime.Today
                };

                var registerResult = await this.Register(customer, model.Customer.Password, Roles.Customer);
                if (registerResult.Succeeded)
                {
                    await SignInManager.SignInAsync(customer, isPersistent: false, rememberBrowser: false);
                    var applicationDomainModel = DependencyResolver.Current.GetService<ApplicationDomainModel>();
                    applicationDomainModel.CreateApplication(model.App, customer.Id);
                    return RedirectToAction("CustomerHome", "Customer");
                }
                AddErrors(registerResult);
            }

            return View(model);
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
                CustomerApps = UnitOfWork.Applications.GetCustomerApplications(customerId)
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
                        UnitOfWork.Applications.AddUserToApplication(agent.Id, appId);                        
                    }
                    UnitOfWork.Save();
                    return RedirectToAction("OwnerAgents", "Owner");
                }
                else
                {
                    AddErrors(registerResult);
                }
            }

            var customerId = User.Identity.GetUserId<long>();
            model.CustomerApps = UnitOfWork.Applications.GetCustomerApplications(customerId);

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
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("ExternalLoginSuccess", "Client", new { returnUrl = returnUrl });
            }

            // Выполнение входа пользователя посредством данного внешнего поставщика входа, если у пользователя уже есть имя входа
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("ExternalLoginSuccess", "Client", new { returnUrl = returnUrl });
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Если у пользователя нет учетной записи, то ему предлагается создать ее
                    return RedirectToAction("CreateUserByExternalInfo", new { Provider = loginInfo.Login.LoginProvider, ReturnUrl = returnUrl });
            }
        }

        //[AllowAnonymous]
        //public async Task<ActionResult> CreateUserByExternalInfo(string provider, string returnUrl)
        //{
        //    string email = null,
        //        userName = null,
        //        photoUrl = null,
        //        link = null;

        //    var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
        //    userName = identity.Name;
        //    switch (provider)
        //    {
        //        case "Facebook":
        //            {
        //                var accessToken = identity.FindFirstValue("FacebookAccessToken");
        //                var fb = new FacebookClient(accessToken);
        //                dynamic myInfo = fb.Get("/me?fields=email,picture");
        //                email = myInfo.email;
        //                photoUrl = string.Format("https://graph.facebook.com/{0}/picture", myInfo.id);
        //                link = "https://www.facebook.com/" + myInfo.id;
        //            }
        //            break;
        //        case "Twitter":
        //            {
        //                var tw = new TwitterService(
        //                    "owRihJ8kuYIMDAa6XaK88O7Vj",
        //                    "YTloVLtW2CDBhrFrzNbrZOJkaB2mxPBqZ36IWtxu3BNtN7td1u",
        //                    identity.FindFirstValue("TwitterAccessToken"),
        //                    identity.FindFirstValue("TwitterAccessTokenSecret")
        //               );
        //                var profile = tw.GetUserProfile(new GetUserProfileOptions());
        //                if (profile != null)
        //                {
        //                    userName = profile.ScreenName;
        //                    photoUrl = profile.ProfileImageUrl;
        //                    link = "https://twitter.com/" + userName;
        //                }
        //            }
        //            break;
        //    }

        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    var user = new AppUser { Name = userName, UserName = userName, Email = email };
        //    var result = await UserManager.CreateAsync(user);
        //    if (result.Succeeded)
        //    {
        //        result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
        //        if (result.Succeeded)
        //        {
        //            await UserManager.AddToRoleAsync(user.Id, "Client");
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //    }
        //    if (result.Errors.Count() == 1)
        //    {
        //        user = UserManager.FindByEmail(email);
        //        UserManager.AddLogin(user.Id, loginInfo.Login);
        //    }
        //    UserManager.AddClaim(user.Id, new Claim("PhotoUrl", photoUrl));
        //    UserManager.AddClaim(user.Id, new Claim("SocialNetworkLink:" + provider, link));

        //    return RedirectToAction("ExternalLoginSuccess", "Client", new { returnUrl = returnUrl });
        //}



        //
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

        private void AddErrors(IdentityResult result)
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