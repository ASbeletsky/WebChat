namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region

    using WebChat.Domain.Core.Identity;

    #endregion
    public interface IClientRepository : IRepository<Client, long>
    {
    }
}
