namespace WebChat.Infrastructure.CQRS.QueryHandlers.Application
{
    #region Using

    using WebChat.Infrastructure.CQRS.Interfaces;
    using WebChat.Infrastructure.CQRS.Queries;
    using WebChat.Infrastructure.CQRS.Queries.Application;
    using WebChat.Infrastructure.Data.Interfaces;
    using WebChat.Infrastructure.Data.Interfaces.Repositories;
    using WebChat.Infrastructure.Data.Models.Application;
    using WebChat.Infrastructure.Services.Interfaces;
    using WebChat.WebUI.ViewModels.Customer;

    #endregion
    public class EditApplicationQueryHandler : BaseQueryHandler, IQueryHandler<EditApplicationQuery, ApplicationViewModel>
    {
        private IApplicationRepository application;
        public EditApplicationQueryHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.application = UnitOfWork.Applications;
        }

        public ApplicationViewModel Execute(EditApplicationQuery query)
        {
            var currentApp = application.GetById(query.AppId);
            var appViewModel = EntityConverter.Convert<ApplicationModel, ApplicationViewModel>(currentApp);
            return appViewModel;
        }
    }
}
