using WebChat.Infrastructure.CQRS.Interfaces;

namespace WebChat.Infrastructure.CQRS.Queries.Application
{
    public class GetApplicationClientsQuery : IQuery
    {
        public GetApplicationClientsQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
        public int ApplicationId { get; set; }
    }
}
