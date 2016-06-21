namespace WebChat.Data.Interfaces.Repositories
{
    #region Using

    using Data.Models.Chat;
    using System.Collections.Generic;
    using WebUI.ViewModels.Сhat;
    #endregion

    public interface IDialogRepository : IRepository<DialogModel, int>
    {
        IEnumerable<MessageViewModel> GetMessages(int dialogId);
    }
}
