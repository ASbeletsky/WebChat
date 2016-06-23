using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebChat.Business.DomainModels;
using WebChat.Data.Models.Identity;
using WebChat.Data.Storage.Identity;
using WebChat.Data.Storage.Managers;
using WebChat.Services.Interfaces;
using WebChat.WebUI.ViewModels.Agent;
using WebChat.WebUI.ViewModels.Shared;
using WebChat.WebUI.WebApp.AppStart;

namespace WebChat.WebUI.WebApp.Controllers.Site.Account
{
    public class AgentAccountController : AccountController
    {
        public AgentAccountController(AppUserManager userManager, AppSignInManager signInManager)
            :base(userManager, signInManager)
        {
        }

        public async Task<JsonResult> RegisterAgent(RegisterAgentViewModel model)
        {
            var agent = new UserModel
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                RegistrationDate = DateTime.Today
            };

            var result = await this.Register(agent, model.Password, Roles.Agent);
            if(result.Succeeded)
            {
                foreach(var appId in model.SelectedApps)
                {
                    Storage.Applications.AddUserToApplication(agent.Id, appId);
                }
                               
                Storage.Save();                
            }

            var application = DependencyContainer.Current.GetService<ApplicationDomainModel>();
            var customer = DependencyContainer.Current.GetService<CustomerDomainModel>();
            var customerAppsIds = customer.GetCustomerApps(CurrentUserId.Value).Select(app => app.Id).ToList();
            var agents = customerAppsIds.SelectMany(id => application.GetApplicationAgents(id));

            return JsonData(true, 
                message: "Оператор успешно зарегистрирован",
                data: RenderPartialToString("~/Views/CustomerAgentManagement/_AgentsList.schtml", agents));
        } 
    }
}
