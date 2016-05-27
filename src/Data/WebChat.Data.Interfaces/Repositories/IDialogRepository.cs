namespace WebChat.Domain.Interfaces.Repositories
{
    #region Using

    using Data.Models.Chat;
    using System.Collections.Generic;
    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
    }
}
