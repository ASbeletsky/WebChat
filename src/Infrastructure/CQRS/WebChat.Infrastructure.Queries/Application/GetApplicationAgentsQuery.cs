using WebChat.Infrastructure.CQRS.Interfaces;

namespace WebChat.Infrastructure.CQRS.Queries.Application
{
    public class GetApplicationAgentsQuery : IQuery
    {
        public GetApplicationAgentsQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
        public int ApplicationId { get; set; }
    }
}
