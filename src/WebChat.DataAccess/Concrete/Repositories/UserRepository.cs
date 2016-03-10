namespace WebChat.DataAccess.Concrete.Repositories
{
    #region 

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebChat.DataAccess.Managers;
    using WebChat.Infrastructure.Data;
    using WebChat.Models.Entities.Identity;

    #endregion

    public class UserRepository : IRepository<AppUser>
    {
        private AppUserManager _userManager;
        private WebChatDbContext _context;
        public UserRepository(WebChatDbContext context)
        {
            _context = context;
            _userManager = new AppUserManager(new UserStore<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim>(_context));
        }

        public UserManager<AppUser, long> GetUserManager()
        {
            return _userManager;
        }

        public IEnumerable<AppUser> All
        {
            get { return _userManager.Users; }
        }

        public IEnumerable<AppUser> GetUsersInRole(string roleName)
        {
            var result = from role in _context.Roles
                         where role.Name == roleName
                         join userRole in _context.UserRoles
                            on role.Id equals userRole.RoleId
                         join user in _context.Users
                            on userRole.UserId equals user.Id
                         select user;
            return result;
        }

        public async Task<IdentityResult> CreateAsync(AppUser user)
        {
            var result = await _userManager.CreateAsync(user);
            return result;
        }

        public void Delete(int id)
        {
            AppUser userForDelete = _userManager.FindById(id);
            if (userForDelete != null)
                _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserApp WHERE UserId = @p0", id);

            _context.SaveChanges();
        }

        public void DeleteFromApp(int id, int appId)
        {
            AppUser userForDelete = _userManager.FindById(id);
            if (userForDelete != null)
                _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserApp WHERE UserId = @p0 AND AppId = @p1", id, appId);
                _context.SaveChanges();
        }

        public AppUser Find(Func<AppUser, bool> predicate)
        {
            AppUser current = _userManager.Users.FirstOrDefault(predicate);
            return current;
        }

        public AppUser GetById(long id)
        {
            return _userManager.FindById(id);
        }

        public void Update(AppUser user)
        {
            AppUser sameDataPerson = _userManager.Users.FirstOrDefault(o => o.Email == user.Email);
            if (sameDataPerson == null)
            {
                _userManager.Update(user);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }

        public async Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public AppUser GetById(dynamic id)
        {
            long userId = (long)id;
        }

        public void Create(AppUser item)
        {
            throw new NotImplementedException();
        }

        public void Delete(dynamic id)
        {
            throw new NotImplementedException();
        }
    }
}
