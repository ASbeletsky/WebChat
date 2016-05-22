namespace WebChat.Domain.Interfaces.Repositories
{
    using System.Collections.Generic;
    #region Using

    using WebChat.Data.Models.Application;

    #endregion

    public interface ICustomerAppRepository : IRepository<CustomerApplicationModel, int>
    {
        IEnumerable<CustomerApplicationModel> GetCustomerApplications(long customerId);
        void AddUserToApplication(long userId, int appId);

        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        //IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        //IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
