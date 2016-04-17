namespace WebChat.Infrastructure.Data.Storage.Repositories
{

    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces.Repositories;
    using Models.Application;
    using Interfaces;

    #endregion
    public class UsersInAppsRepository : IUsersInAppsRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public UsersInAppsRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public UsersInAppsModel GetById(IComplexKey<long, int> id)
        {
            return _context.UsersInApplications.Find(id);
        }

        public UsersInAppsModel Find(Func<UsersInAppsModel, bool> predicate)
        {
            return _context.UsersInApplications.FirstOrDefault(predicate);
        }

        public IEnumerable<UsersInAppsModel> All
        {
            get
            {
                return _context.UsersInApplications;
            }
        }

        public void Create(UsersInAppsModel item)
        {
            _context.UsersInApplications.Add(item);
            _context.SaveChanges();
        }

        public void Update(UsersInAppsModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(IComplexKey<long, int> id)
        {
            var recordToDelete = _context.UsersInApplications.Find(id.Key1, id.Key2);
            if (recordToDelete != null)
            {
                _context.UsersInApplications.Remove(recordToDelete);
                _context.SaveChanges();
            }
        }

        #endregion

        #region IUsersInAppsRepository Members

        public void DeleteUserFromAllApps(long userId)
        {
            var appsWithCurrentUser = _context.UsersInApplications.Where(x => x.UserId == userId);
            if(appsWithCurrentUser.Any())
            {
                _context.UsersInApplications.RemoveRange(appsWithCurrentUser);
                _context.SaveChanges();
            }
        }

        #endregion
    }
}
