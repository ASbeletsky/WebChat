namespace WebChat.Data.Storage.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Interfaces.Repositories;
    using Interfaces;
    using Identity;

    #endregion

    public class UsersInRolesRepository : IUsersInRolesRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public UsersInRolesRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public UsersInRolesModel GetById(IComplexKey<long, int> id)
        {
            return _context.UsersInRoles.Find(id);
        }

        public UsersInRolesModel Find(Func<UsersInRolesModel, bool> predicate)
        {
            return _context.UsersInRoles.FirstOrDefault(predicate);
        }

        public IEnumerable<UsersInRolesModel> All
        {
            get
            {
                return _context.UsersInRoles;
            }
        }

        public void Create(UsersInRolesModel item)
        {
            _context.UsersInRoles.Add(item);
        }

        public void Update(UsersInRolesModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(IComplexKey<long, int> id)
        {
            var recordToDelete = _context.UsersInRoles.Find(id.Key1, id.Key2);
            if (recordToDelete != null)
            {
                _context.UsersInRoles.Remove(recordToDelete);
            }
        }

        #endregion

        #region IUsersInAppsRepository Members


        #endregion
    }
}
