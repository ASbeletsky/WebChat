namespace WebChat.Infrastructure.CQRS.QueryHandlers.Application
{
    #region Using

    using System.Collections.Generic;
    using WebChat.Domain.Core.Identity;
    using WebChat.Infrastructure.CQRS.Interfaces;
    using WebChat.Infrastructure.CQRS.Queries;
    using WebChat.Infrastructure.CQRS.Queries.Application;
    using WebChat.Infrastructure.Data.Interfaces;
    using WebChat.Infrastructure.Data.Interfaces.Repositories;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public class GetApplicationClientsQueryHandler : BaseQueryHandler, IQueryHandler<GetApplicationClientsQuery, IEnumerable<Client>>
    {
        private IApplicationRepository application;
        public GetApplicationClientsQueryHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.application = UnitOfWork.Applications;
        }
        public IEnumerable<Client> Execute(GetApplicationClientsQuery query)
        {
           return application.GetApplicationAgents(query.ApplicationId);
        }
    }
}
