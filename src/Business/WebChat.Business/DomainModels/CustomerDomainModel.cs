namespace WebChat.Business.DomainModels
{
    using Data.Models.Application;
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

        public IEnumerable<ApplicationModel> GetCustomerApps(long customerId)
        {
            return Storage.Applications.GetCustomerApplications(customerId);
        }

    }
}
