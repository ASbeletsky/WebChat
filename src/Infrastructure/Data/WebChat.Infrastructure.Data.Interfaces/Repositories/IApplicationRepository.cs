namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region Using

    using System.Collections.Generic;
    using Models.Application;
    using Domain.Core.Customer;
    using Domain.Core.Identity;

    #endregion

    public interface IApplicationRepository : IRepository<Application, int>
    {
        IEnumerable<Client> GetApplicationClients(int appId);
        IEnumerable<Agent> GetApplicationAgents(int appId);

        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        //IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        //IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
