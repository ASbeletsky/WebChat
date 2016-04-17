namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{   
    #region Using

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Application;
    #endregion

    public interface ICustomerAppRepository : IRepository<CustomerApplicationModel, int>
    {
        string GenerateAppKey();
        int GetIdByAppKey(string appKey);
        ICollection<CustomerApplicationModel> GetApplicationsByOwner(long userId);
        Task<CustomerApplicationModel> GetByIdAsync(int appId);

        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        //IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        //IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
