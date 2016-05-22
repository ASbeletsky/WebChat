namespace WebChat.Data.Storage.Repositories
{
    using Models.Application;
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion

    public class CustomerAppRepository : ICustomerAppRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;


        #endregion

        #region Constructors
        public CustomerAppRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public CustomerApplicationModel GetById(int id)
        {
            return _context.CustomerApplications.Find(id);
        }

        public CustomerApplicationModel Find(Func<CustomerApplicationModel, bool> predicate)
        {
            return _context.CustomerApplications.FirstOrDefault(predicate);
        }

        public IEnumerable<CustomerApplicationModel> All
        {
            get
            {
                return _context.CustomerApplications;
            }
        }

        public void Create(CustomerApplicationModel item)
        {
            _context.CustomerApplications.Add(item);
            _context.SaveChanges();
        }

        public void Update(CustomerApplicationModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var appForDelete = _context.CustomerApplications.Find(id);
            if(appForDelete != null)
            {
                _context.CustomerApplications.Remove(appForDelete);
                _context.SaveChanges();
            }
        }

        #endregion

        #region ICustomerAppRepository Members

        public IEnumerable<CustomerApplicationModel> GetCustomerApplications(long ownerId)
        {
            return _context.CustomerApplications.Where(a => a.OwnerId == ownerId);
        }

        public void AddUserToApplication(long userId, int appId)
        {
            _context.UsersInApplications.Add(new UsersInAppsModel(userId, appId));
        }

        #endregion

        /*------------------------------------- Функции ---------------------------------------------*/
        //public IEnumerable<ResponceTimePerHour> AverageChatResponceTimeByHours(int AppId)
        //{
        //    return _context.Database.SqlQuery<ResponceTimePerHour>(
        //        "select * from dbo.AverageChatResponceTimeByHours(@appId)",
        //        new SqlParameter("@appId", AppId))
        //    .ToList();
        //}

        //public IEnumerable<ChatDurationPerDay> ChatDurationInPeriod(int AppId, DateTime StartDate, DateTime EndDate)
        //{
        //    return _context.Database.SqlQuery<ChatDurationPerDay>(
        //        "select * from dbo.ChatDurationInPeriod(@appId, @StartDate, @EndDate)",
        //        new SqlParameter("@StartDate", StartDate),
        //        new SqlParameter("@EndDate", EndDate),
        //        new SqlParameter("@appId", AppId))
        //    .ToList();
        //}

        //public IEnumerable<DialogsPerDay> ChatsCountInPeriod(int AppId, DateTime StartDate, DateTime EndDate)
        //{
        //    return _context.Database.SqlQuery<DialogsPerDay>(
        //        "select * from dbo.ChatsCountInPeriod(@appId, @StartDate, @EndDate)",
        //        new SqlParameter("@StartDate", StartDate),
        //        new SqlParameter("@EndDate", EndDate),
        //        new SqlParameter("@appId", AppId))
        //    .ToList();
        //}

        //public IEnumerable<MessagesByMonth> MessageCountInCurrentAndPreviosMonth(int AppId)
        //{
        //    return _context.Database.SqlQuery<MessagesByMonth>(
        //        "select * from dbo.MessageCountInCurrentAndPreviosMonth(@appId)",
        //        new SqlParameter("@appId", AppId))
        //    .ToList();
        //}

        //public IEnumerable<AgentAndMessageCount> MostActiveAgentOnDay(int AppId, DateTime Day)
        //{
        //    return _context.Database.SqlQuery<AgentAndMessageCount>(
        //        "select * from dbo.MostActiveAgentOnDay(@date, @appId)",
        //        new SqlParameter("@date", Day),
        //        new SqlParameter("@appId", AppId))
        //    .ToList();
        //}

        //public void AddUserToApplication(long userId, int appId)
        //{
        //    _context.Database.ExecuteSqlCommand("insert into dbo.UserApp values(@p0, @p1)", userId, appId);
        //    _context.SaveChanges();
        //}
    }
}
