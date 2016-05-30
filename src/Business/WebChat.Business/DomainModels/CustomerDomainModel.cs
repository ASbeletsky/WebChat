namespace WebChat.Business.DomainModels
{
    using Services.Interfaces;
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using WebUI.ViewModels.Application;

    #endregion
    public class CustomerDomainModel : BaseDomainModel
    {
        private ApplicationDomainModel applications;

        public CustomerDomainModel()
        {
            this.applications = DependencyContainer.Current.GetService<ApplicationDomainModel>();
        }
        public IEnumerable<ApplicationShortInfoViewModel> GetApplicationsInfo(long customerId)
        {
            var customerAppsInfo = Storage.Applications.GetCustomerApplications(customerId).Select(app => new ApplicationShortInfoViewModel
            {
                AppId = app.Id,
                SiteUrl = app.WebsiteUrl,
            }).ToList();

            foreach(var app in customerAppsInfo)
            {
                app.AgentsCount = Storage.Applications.GetAgents(app.AppId).Count();
                app.DialogsCount = Storage.Applications.GetDialogs(app.AppId).Count();
                app.BestAgent = applications.GetBestAgent(app.AppId);
            }

            return customerAppsInfo;
        }
    }
}
