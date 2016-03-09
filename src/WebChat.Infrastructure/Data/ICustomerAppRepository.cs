using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Concrete.DataBase.Statistic_Entities;
using WebChat.DataAccess.Concrete.Entities.Customer_apps;

namespace WebChat.DataAccess.Abstract
{
    public interface ICustomerAppRepository : IRepository<CustomerApplication>
    {
        string GenerateAppKey();
        int GetIdByAppKey(string appKey);
        void AddUserToApplication(long userId, int appId);
        IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day);
        IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId);
        IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate);
        IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId);
        void DeleteFromApp(int id, int appId);
        void DeleteFromApps(int id);
    }
}
