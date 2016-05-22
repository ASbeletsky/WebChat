namespace WebChat.Infrastructure.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Repositories;
    using Models.Application;
    using Services.Interfaces;
    using Factories;
    using Domain.Core.Customer;
    using Domain.Core.Identity;
    using Models.Identity;
    #endregion

    public class ApplicationRepository : IApplicationRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext context;
        private readonly IEntityConverter converter;
        private readonly ApplicationFactory factory;

        #endregion

        #region Constructors
        public ApplicationRepository(WebChatDbContext context, IEntityConverter converter, ApplicationFactory factory)
        {
            this.context = context;
            this.converter = converter;
            this.factory = factory;
        }

        #endregion

        public IEnumerable<Application> All
        {
            get
            {
                IEnumerable<ApplicationModel> models = context.Applications;
                IEnumerable<Application> apps = factory.RestoreApplicationsFromModels(models);
                return apps;
            }
        }

        public void Create(Application item)
        {
            ApplicationModel newApp = converter.Convert<Application, ApplicationModel>(item);
            context.Applications.Add(newApp);
        }

        public void Delete(int id)
        {
            var recordForDelete = context.Applications.Find(id);
            if (recordForDelete != null)
            {
                context.Applications.Remove(recordForDelete);
            }
        }

        public Application GetById(int id)
        {
            ApplicationModel model = context.Applications.Find(id);
            Application app = factory.RestoreApplicationFromModel(model);
            return app;
        }

        public void Update(Application item)
        {
            ApplicationModel app = converter.Convert<Application, ApplicationModel>(item);
            context.Entry(app).State = EntityState.Modified;
        }

        #region ICustomerAppRepository Members

        public IEnumerable<Client> GetApplicationClients(int appId)
        {
            var userModels = from userInApp in context.UsersInApplications
                             join userModel in context.Users on userInApp.UserId equals userModel.Id
                             where userInApp.AppId == appId && userModel.Roles.Any(role => role.RoleId == (int)Roles.Client)
                             select userModel;

            return converter.Convert<IEnumerable<UserModel>, IEnumerable<User>>(userModels) as IEnumerable<Client>;
        }

        public IEnumerable<Agent> GetApplicationAgents(int appId)
        {
            var userModels = from userInApp in context.UsersInApplications
                             join userModel in context.Users on userInApp.UserId equals userModel.Id
                             where userInApp.AppId == appId && userModel.Roles.Any(role => role.RoleId == (int)Roles.Agent)
                             select userModel;

            return converter.Convert<IEnumerable<UserModel>, IEnumerable<User>>(userModels) as IEnumerable<Agent>;
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
