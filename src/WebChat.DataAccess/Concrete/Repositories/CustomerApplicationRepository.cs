using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.DataBase.Statistic_Entities;
using WebChat.DataAccess.Concrete.Entities.Customer_apps;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.DataAccess.Concrete.Repositories
{
    public class CustomerAppRepository : ICustomerAppRepository
    {
        private readonly WebChatDbContext _context;
        public CustomerAppRepository(WebChatDbContext context)
        {
            _context = context;
        }

        public string GenerateAppKey()
        {
            return _context.GenerateCustomerAppKey();
        }

        public IEnumerable<CustomerApplication> All
        {
            get { return _context.CustomerApplication.AsQueryable(); }
        }

        public void Create(CustomerApplication item)
        {

                _context.CustomerApplication.Add(item);
                _context.SaveChanges();
        }

        public void Delete(dynamic Id)
        {
            int id = (int)Id;
            var appForDelete = _context.CustomerApplication.Find(id);
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserApp WHERE AppId = @p0", id);
            _context.CustomerApplication.Remove(appForDelete);
            _context.SaveChanges();
        }

        public void DeleteFromApp(int id, int appId)
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserApp WHERE UserId = @p0 AND AppId = @p1", id, appId);
            _context.SaveChanges();
        }

        public void DeleteFromApps(int id)
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserApp WHERE UserId = @p0", id);
            _context.SaveChanges();
        }

        public CustomerApplication Find(Func<CustomerApplication, bool> predicate)
        {
            return _context.CustomerApplication.Where(predicate).FirstOrDefault();
        }

        public CustomerApplication GetById(dynamic Id)
        {
            int id = (int)Id;
            return _context.CustomerApplication.Find(id);
        }

        public void Update(CustomerApplication item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int GetIdByAppKey(string appKey)
        {
            return _context.CustomerApplication.FirstOrDefault(app => app.AppKey == appKey).Id;
        }


        /*------------------------------------- Функции ---------------------------------------------*/
        public IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId)
        {
            return _context.Database.SqlQuery<ResponceTimePerHour>(
                "select * from dbo.AverageChatResponceTimeByHours(@appId)",
                new SqlParameter("@appId", AppId))
            .ToList();
        }

        public IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate)
        {
            return _context.Database.SqlQuery<ChatDurationPerDay>(
                "select * from dbo.ChatDurationInPeriod(@appId, @StartDate, @EndDate)",
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate", EndDate),
                new SqlParameter("@appId", AppId))
            .ToList();
        }

        public IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate)
        {
            return _context.Database.SqlQuery<DialogsPerDay>(
                "select * from dbo.ChatsCountInPeriod(@appId, @StartDate, @EndDate)",
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate", EndDate),
                new SqlParameter("@appId", AppId))
            .ToList();
        }

        public IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId)
        {
            return _context.Database.SqlQuery<MessagesByMonth>(
                "select * from dbo.MessageCountInCurrentAndPreviosMonth(@appId)",
                new SqlParameter("@appId", AppId))
            .ToList();
        }

        public IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day)
        {
            return _context.Database.SqlQuery<AgentAndMessageCount>(
                "select * from dbo.MostActiveAgentOnDay(@date, @appId)",
                new SqlParameter("@date", Day),
                new SqlParameter("@appId", AppId))
            .ToList();
        }

        public void AddUserToApplication(long userId, int appId)
        {
            _context.Database.ExecuteSqlCommand("insert into dbo.UserApp values(@p0, @p1)", userId, appId);
            _context.SaveChanges();
        }
    }
}
