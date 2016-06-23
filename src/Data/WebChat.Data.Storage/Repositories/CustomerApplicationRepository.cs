namespace WebChat.Data.Storage.Repositories
{
    using Models.Application;
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebChat.Data.Interfaces.Repositories;
    using Models.Chat;
    using Identity;
    using Models.Identity;
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

        public ApplicationModel GetById(int id)
        {
            return _context.CustomerApplications.Find(id);
        }

        public ApplicationModel Find(Func<ApplicationModel, bool> predicate)
        {
            return _context.CustomerApplications.FirstOrDefault(predicate);
        }

        public IEnumerable<ApplicationModel> All
        {
            get
            {
                return _context.CustomerApplications;
            }
        }

        public void Create(ApplicationModel item)
        {
            _context.CustomerApplications.Add(item);
        }

        public void Update(ApplicationModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var appForDelete = _context.CustomerApplications.Find(id);
            if (appForDelete != null)
            {
                _context.CustomerApplications.Remove(appForDelete);
            }
        }

        #endregion

        #region ICustomerAppRepository Members

        public IEnumerable<ApplicationModel> GetCustomerApplications(long customerId)
        {
            return _context.CustomerApplications.Where(a => a.CustomerId == customerId);
        }

        public void AddUserToApplication(long userId, int appId)
        {
            _context.UsersInApplications.Add(new UsersInAppsModel(userId, appId));
        }

        public IEnumerable<UserModel> GetAgents(int appId)
        {
            var agents = GetAppUserByRole(appId, Roles.Agent);
            return agents;
        }

        public IEnumerable<UserModel> GetClients(int appId)
        {
            var clients = GetAppUserByRole(appId, Roles.Client);
            return clients;
        }

        public IEnumerable<DialogModel> GetDialogs(int appId)
        {
            return _context.Dialogs.Where(dialog => dialog.AppId == appId);
        }

        public IEnumerable<MessageModel> GetMessages(int appId)
        {
            return from dialog in _context.Dialogs
                    join message in _context.Messages
                    on dialog.Id equals message.DialogId
                    where dialog.AppId == appId
                    select message;
        }

        #endregion

        private IEnumerable<UserModel> GetAppUserByRole(int appId, Roles role)
        {
            var usersInApp = _context.UsersInApplications.Where(usersInApps => usersInApps.AppId == appId).AsEnumerable();
            return from usersInRole in _context.UsersInRoles
                   join userInApp in usersInApp
                      on usersInRole.UserId equals userInApp.UserId
                   where usersInRole.RoleId == (long)role
                   select userInApp.User;
        }


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
