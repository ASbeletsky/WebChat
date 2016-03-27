namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using WebChat.Domain.Core.Chat;
    using Core.Identity;
    using System.Collections;
    using System.Collections.Generic;
    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
        IEnumerable<DialogModel> GetApplicationDialogs(int appId);
    }
}
