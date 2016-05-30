namespace WebChat.Data.Interfaces.Repositories
{
    #region Using

    using Data.Models.Chat;
    using Data.Storage.Identity;
    using System.Collections.Generic;
    using WebChat.Data.Models.Application;

    #endregion

    public interface ICustomerAppRepository : IRepository<ApplicationModel, int>
    {
        IEnumerable<ApplicationModel> GetCustomerApplications(long customerId);
        IEnumerable<UserModel> GetAgents(int appId);
        IEnumerable<UserModel> GetClients(int appId);
        void AddUserToApplication(long userId, int appId);
        IEnumerable<DialogModel> GetDialogs(int id);

        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        //IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        //IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
