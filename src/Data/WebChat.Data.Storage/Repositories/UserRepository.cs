namespace WebChat.Data.Storage.Repositories
{
    #region Using

    using Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using WebChat.Data.Interfaces.Repositories;

    #endregion

    public class UserRepository : IUserRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext _context;

        #endregion

        #region Constructors
        public UserRepository(WebChatDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IRepository Members

        public UserModel GetById(long id)
        {
            return _context.Users.Find(id);
        }

        public UserModel Find(Func<UserModel, bool> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<UserModel> All
        {
            get
            {
                return _context.Users;
            }
        }

        public void Create(UserModel item)
        {
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void Update(UserModel item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var recordForDelete = _context.Users.Find(id);
            if (recordForDelete != null)
            {
                _context.Users.Remove(recordForDelete);
                _context.SaveChanges();
            }
        }

        #endregion

        #region IUserRepository Members
        public IEnumerable<UserModel> GetUsersInRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == roleName);
            if (role != null)
                return role.Users as IEnumerable<UserModel>;
            else
                throw new ArgumentException("Role \"{0}\" doesn't found", roleName);
        }

        #endregion
    }
}
