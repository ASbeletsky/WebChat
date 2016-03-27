namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Domain.Core.Application;

    #endregion

    public interface ICustomerAppRepository : IRepository<CustomerApplicationModel, int>
    {
        string GenerateAppKey();
        int GetIdByAppKey(string appKey);
        ICollection<CustomerApplicationModel> GetApplicationsByOwner(long userId);

        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        //IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        //IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
