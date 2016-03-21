namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using WebChat.Domain.Core.Chat;
    using Core.Identity;

    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
    }
}
