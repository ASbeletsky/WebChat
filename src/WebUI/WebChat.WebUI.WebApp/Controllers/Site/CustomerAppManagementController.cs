namespace WebChat.WebUI.WebApp.Controllers
{
    #region Using

    using WebChat.WebUI.WebApp.Extentions;
    using System.Web.Mvc;
    using Business.DomainModels;
    using ViewModels;
    using System.Collections.Generic;

    #endregion

    [Authorize(Roles = "Customer")]
    public class CustomerAppManagementController : MvcBaseController
    {
        private readonly ApplicationDomainModel applications;
        private readonly CustomerDomainModel customers;

        public CustomerAppManagementController()
        {
            this.applications = DependencyResolver.Current.GetService<ApplicationDomainModel>();
            this.customers = DependencyResolver.Current.GetService<CustomerDomainModel>();
        }

        public ActionResult Index()
        {
            ViewBag.Menu = this.Menu;
            return View();
        }

        #region private members

        private IEnumerable<MenuItem> Menu
        {
            get
            {
                return new List<MenuItem>()
                    {
                        new MenuItem {Labor = "Мои чаты", Link = Url.Action("MyApplications")  },
                        new MenuItem {Labor = "Окно чата" }
                    };
            }
        }

        #endregion

        //private List<AppUser> GetRelatedAgents(int appId)
        //{
        //    return UnitOfWork.Users.GetUsersInRole("SupportAgent").ToArray()
        //                                               .Where(a => a.RelatedApplications.Any(ra => ra.Id == appId))
        //                                               .ToList();
        //}


        public ActionResult AppScript(int appId)
        {
            string script = applications.GenerateScript(appId);
            return View(model: script);
        }

        public JsonResult MyApplications()
        {
            long customerId = CurrentUserId.Value;
            var apps = customers.GetApplicationsInfo(customerId);
            ViewBag.Title = "Мои приложения";
            return JsonData(true, data: RenderPartialToString("_AppsInfo" , apps), behavior: JsonRequestBehavior.AllowGet);
        }

        //        public ActionResult OwnerAgents()
        //        {
        //            List<SupportAgentViewModel> agents = new List<SupportAgentViewModel>();

        //            List<int> appIds = UnitOfWork.CustomerApplications.All
        //                                         .Where(o => o.OwnerUser_Id == OwnerId)
        //                                         .Select(o => o.Id).ToList();

        //            foreach (var appId in appIds)
        //            {
        //                foreach (AppUser user in GetRelatedAgents(appId))
        //                {
        //                    //appIdsCopy.Remove(appId);

        //                    if (user.Roles.FirstOrDefault(r => r.RoleId == 1) != null)
        //                    {
        //                        var PhotoClaim = user.Claims.FirstOrDefault(o => o.ClaimType == "PhotoSrc");
        //                        string userPhotoSrc = (PhotoClaim != null) ? PhotoClaim.ClaimValue : "/Content/Images/default-user-image.png";
        //                        int dialogsCount = user.Dialogs.Count;
        //                        int messagesCount = user.Messages.Count;
        //                        List<CustomerApplication> relatedApps = user.RelatedApplications.ToList();

        //                        SupportAgentViewModel agent = new SupportAgentViewModel()
        //                        {
        //                            Id = user.Id,
        //                            Name = user.Name,
        //                            Image = userPhotoSrc,
        //                            Email = user.Email,
        //                            Phone = user.PhoneNumber,
        //                            ChatsLimit = 0,
        //                            RegDate = user.RegistrationDate.HasValue ? user.RegistrationDate.Value.ToShortDateString() : "&nbsp;",

        //                            DialogsCount = dialogsCount,
        //                            MessagesCount = messagesCount,
        //                            RelatedApps = relatedApps,
        //                            //AllApps = appIdsCopy

        //                        };
        //                        agents.Add(agent);
        //                    }
        //                }
        //            }

        //            ViewBag.Agents = agents;
        //            ViewBag.Title = "Управление агентами";

        //            return View();
        //        }

        //        public ActionResult EditAgent(int id)
        //        {
        //            AppUser agent = UnitOfWork.Users.GetById(id);
        //            RegisterOperatorViewModel model = new RegisterOperatorViewModel();
        //            if (agent != null)
        //            {
        //                var photoClaim = agent.Claims.FirstOrDefault(c => c.ClaimType == "PhotoSrc");
        //                model.Name = agent.Name;
        //                model.Email = agent.Email;
        //                model.Password = agent.PasswordHash;
        //                model.Phone = agent.PhoneNumber;
        //                model.PhotoSrc = (photoClaim != null) ? photoClaim.ClaimValue : "";
        //                model.SelectedApps = agent.RelatedApplications.Select(a => a.Id).ToList();
        //                model.CustomerApps = UnitOfWork.CustomerApplications.All
        //                                     .Where(a => a.OwnerUser_Id == OwnerId)
        //                                     .ToList();

        //            }

        //            return View(model);
        //        }

        //        [HttpPost]
        //        public async Task<ActionResult> EditAgent(int id, RegisterOperatorViewModel model)
        //        {
        //            AppUser agent = UnitOfWork.Users.GetById(id);
        //            if (ModelState.IsValid)
        //            {               
        //                if (agent != null)
        //                {
        //                    agent.Name = model.Name;
        //                    agent.Email = model.Email;
        //                    agent.PhoneNumber = model.Phone;

        //                    var userManager = UnitOfWork.Users.GetUserManager();
        //                    if(!string.IsNullOrEmpty(model.PhotoSrc))
        //                        await userManager.AddClaimAsync(agent.Id, new Claim("PhotoSrc", model.PhotoSrc));

        //                    UnitOfWork.Users.Update(agent);

        //                    foreach (var appId in model.SelectedApps)
        //                    {
        //                        UnitOfWork.CustomerApplications.AddUserToApplication(agent.Id, appId);
        //                    }                   
        //                }

        //                return RedirectToAction("OwnerAgents", "Owner");
        //            }
        //            model.SelectedApps = agent.RelatedApplications.Select(a => a.Id).ToList();
        //            return View(model);
        //        }

        //        [HttpGet]
        //        public ActionResult DeleteAgent(int id)
        //        {
        //            AppUser agent = UnitOfWork.Users.GetById(id);
        //            if (agent != null)
        //                UnitOfWork.CustomerApplications.DeleteFromApps(id);
        //            return RedirectToAction("OwnerAgents", "Owner");
        //        }
        ///*------------------------------------------------------------------------------------------*/
        //        [HttpGet]
        //        public ActionResult AddApplication()
        //        {
        //            var model = new RegisterApplicationViewModel
        //            {
        //                ContactEmail = UnitOfWork.Users.GetById(OwnerId).Email
        //            };
        //            return View(model);
        //        }

        //        [HttpPost]
        //        public ActionResult AddApplication(RegisterApplicationViewModel model)
        //        {
        //            var Owner = UnitOfWork.Users.GetById(OwnerId);
        //            if (ModelState.IsValid)
        //            {
        //                var application = new CustomerApplication()
        //                {
        //                    OwnerUser_Id = OwnerId,
        //                    AppKey = UnitOfWork.CustomerApplications.GenerateAppKey(),
        //                    WebsiteUrl = model.WebsiteUrl,
        //                    SubjectScope = model.SubjectScope,
        //                    ContactEmail = model.ContactEmail
        //                };
        //                UnitOfWork.CustomerApplications.Create(application);
        //                return RedirectToAction("OwnerInfo", "Owner");
        //            }

        //            return View(model);
        //        }

        //        [HttpGet]
        //        public ActionResult EditApplication(int id)
        //        {
        //            var app = UnitOfWork.CustomerApplications.GetById(id);
        //            RegisterApplicationViewModel model = null;

        //            if (app != null)
        //            {
        //                model = new RegisterApplicationViewModel
        //                {
        //                    ContactEmail = app.ContactEmail,
        //                    SubjectScope = app.SubjectScope,
        //                    WebsiteUrl = app.WebsiteUrl
        //                };
        //            }

        //            return View(model);
        //        }

        //        [HttpPost]
        //        public ActionResult EditApplication(int id, RegisterApplicationViewModel model)
        //        {
        //            var Owner = UnitOfWork.Users.GetById(OwnerId);
        //            if (ModelState.IsValid)
        //            {
        //                var app = UnitOfWork.CustomerApplications.GetById(id);
        //                if (app != null)
        //                {
        //                    app.WebsiteUrl = model.WebsiteUrl;
        //                    app.SubjectScope = model.SubjectScope;
        //                    app.ContactEmail = model.ContactEmail;

        //                    UnitOfWork.CustomerApplications.Update(app);
        //                }              

        //                return RedirectToAction("OwnerInfo", "Owner");
        //            }

        //            return View(model);
        //        }

        //        public ActionResult DeleteApplication(int id)
        //        {
        //            UnitOfWork.CustomerApplications.Delete(id);
        //            return RedirectToAction("OwnerInfo", "Owner");
        //        }

        ///*---------------------------------------------------------------------------------------------*/
        //        [HttpGet]
        //        public bool ChangeAgentApp(int agentId, int appId)
        //        {

        //            var foundApp = UnitOfWork.CustomerApplications.All.FirstOrDefault(o => o.Id == appId);
        //            var foundAgent = UnitOfWork.Users.GetById(agentId);

        //            if ( foundApp != null && foundAgent != null)
        //            {
        //                bool isInApp = foundAgent.RelatedApplications.Any(app => app.Id == appId);
        //                if (isInApp)
        //                {
        //                    UnitOfWork.CustomerApplications.DeleteFromApp(agentId, appId);
        //                    return true;
        //                }
        //                else
        //                {
        //                    UnitOfWork.CustomerApplications.AddUserToApplication(agentId, appId);
        //                    return true;
        //                }
        //            }

        //            return false;

        //        }

    }
}