namespace WebChat.Infrastructure.CQRS.Queries.CustomerApp
{
    #region Using

    using System.Threading.Tasks;
    using WebChat.WebUI.ViewModels.Customer;
    using Data.Interfaces;
    using Data.Interfaces.Repositories;
    using Interfaces;
    using Services.Interfaces;
    using Data.Models.Application;

    #endregion

    public class EditCustomerAppQueryHandler : BaseQueryHandler, IQueryHandler<EditCustomerAppQuery, ApplicationViewModel>
    {
        private ICustomerAppRepository customerApps;
        public EditCustomerAppQueryHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.customerApps = UnitOfWork.CustomerApplications;
        }

        public async Task<ApplicationViewModel> Retrieve(EditCustomerAppQuery query)
        {
            var currentApp = await customerApps.GetByIdAsync(query.AppId);
            var appViewModel = EntityConverter.Convert<CustomerApplicationModel, ApplicationViewModel>(currentApp);
            return appViewModel;
        }
    }
}
