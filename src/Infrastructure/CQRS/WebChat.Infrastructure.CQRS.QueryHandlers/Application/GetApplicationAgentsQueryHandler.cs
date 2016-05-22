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

    public class GetApplicationAgentsQueryHandler : BaseQueryHandler, IQueryHandler<GetApplicationAgentsQuery, IEnumerable<Agent>>
    {
        private IApplicationRepository application;
        public GetApplicationAgentsQueryHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.application = UnitOfWork.Applications;
        }
        public IEnumerable<Agent> Execute(GetApplicationAgentsQuery query)
        {
            return application.GetApplicationAgents(query.ApplicationId);
        }
    }
}
