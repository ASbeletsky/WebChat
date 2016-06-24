namespace WebChat.Data.Interfaces.Repositories
{
    #region Using

    using Data.Models.Chat;
    using Data.Storage.Identity;
    using System;
    using System.Collections.Generic;
    using WebChat.Data.Models.Application;
    using WebUI.ViewModels.Statistic;
    #endregion

    public interface ICustomerAppRepository : IRepository<ApplicationModel, int>
    {
        IEnumerable<ApplicationModel> GetCustomerApplications(long customerId);
        IEnumerable<UserModel> GetAgents(int appId);
        IEnumerable<UserModel> GetClients(int appId);
        void AddUserToApplication(long userId, int appId);
        IEnumerable<DialogModel> GetDialogs(int id);
        IEnumerable<MessageModel> GetMessages(int appId);


        //IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        MessageCountDifferenceChartDataItem MessageCountInCurrentAndPreviosMonth(int AppId);
        IEnumerable<DialogPerDayChartIDataItem> GetChatsCountInPeriod(int appId, DateTime startDate, DateTime endDate);
        IEnumerable<DialogsDurationPerDayChartDataItem> GetChatDurationInPeriod(int appId, DateTime startDate, DateTime endDate);
        //IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
    }
}
