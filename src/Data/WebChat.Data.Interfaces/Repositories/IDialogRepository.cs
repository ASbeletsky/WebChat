namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using Data.Models.Chat;
    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
    }
}
