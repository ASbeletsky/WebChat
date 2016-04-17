namespace WebChat.Infrastructure.Data.Interfaces.Repositories
{
    #region Using

    using Models.Chat;
    using System.Collections.Generic;

    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
        IEnumerable<DialogModel> GetApplicationDialogs(int appId);
    }
}
