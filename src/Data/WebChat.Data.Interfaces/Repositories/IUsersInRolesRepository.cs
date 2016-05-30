using WebChat.Data.Storage.Identity;

namespace WebChat.Data.Interfaces.Repositories
{
    public interface IUsersInRolesRepository : IRepository<UsersInRolesModel, IComplexKey<long, int>>
    {
    }
}
