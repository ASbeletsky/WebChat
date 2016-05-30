namespace WebChat.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Models.Chat;
    using Interfaces.Repositories;
    using Interfaces;
    #endregion

    public class UsersInDialogsRepository : IUsersInDialogsRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public UsersInDialogsRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public UsersInDialogsModel GetById(IComplexKey<long, int> id)
        {
            return _context.UsersInDialogs.Find(id);
        }

        public UsersInDialogsModel Find(Func<UsersInDialogsModel, bool> predicate)
        {
            return _context.UsersInDialogs.FirstOrDefault(predicate);
        }

        public IEnumerable<UsersInDialogsModel> All
        {
            get
            {
                return _context.UsersInDialogs;
            }
        }

        public void Create(UsersInDialogsModel item)
        {
            _context.UsersInDialogs.Add(item);
        }

        public void Update(UsersInDialogsModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(IComplexKey<long, int> id)
        {
            var recordToDelete = _context.UsersInDialogs.Find(id.Key1, id.Key2);
            if (recordToDelete != null)
            {
                _context.UsersInDialogs.Remove(recordToDelete);
            }
        }

        #endregion

        #region IUsersInAppsRepository Members

        public void DeleteUserFromAllApps(long userId)
        {
            var appsWithCurrentUser = _context.UsersInApplications.Where(x => x.UserId == userId);
            if (appsWithCurrentUser.Any())
            {
                _context.UsersInApplications.RemoveRange(appsWithCurrentUser);
            }
        }

        #endregion
    }
}
