namespace WebChat.Business.DomainModels
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using WebUI.Models;

    #endregion
    public class CustomerDomainModel : BaseDomainModel
    {
        public IEnumerable<ApplicationShortInfoViewModel> GetApplicationsInfo(long customerId)
        {
            var customerAppsInfo = Storage.Applications.GetCustomerApplications(customerId).Select(app => new ApplicationShortInfoViewModel
            {
                AppId = app.Id,
                SiteUrl = app.WebsiteUrl,
                AgentsCount = Storage.Applications.GetAgents(app.Id).Count(),
                DialogsCount = Storage.Applications.GetDialogs(app.Id).Count()
                //TODO: get best agent
            });

            return customerAppsInfo;
        }
    }
}
