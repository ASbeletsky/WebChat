namespace WebChat.WebUI.WebApp.Controllers.Site.Account
{
    #region Using

    using AppStart;
    using Business.DomainModels;
    using Data.Storage.Managers;
    using Data.Models.Identity;
    using Data.Storage.Identity;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels.Application;
    using ViewModels.Customer;
    using ViewModels.Shared;

    #endregion

    public class CustomerAccountController : AccountController
    {
        public CustomerAccountController(AppUserManager userManager, AppSignInManager signInManager)
            :base(userManager, signInManager)
        {
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
                App = new ApplicationFieldsViewModel
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
                    model.App.CustomerId = customer.Id;
                    applicationDomainModel.CreateApplication(model.App);
                    return RedirectToAction("Index", "CustomerAppManagement");
                }

                AddErrors(registerResult);
            }

            return View(model);
        }

        protected override ActionResult OnSuccessLogin(string returnUrl)
        {
            return RedirectToAction("Index", "CustomerAppManagement");
        }
    }
}