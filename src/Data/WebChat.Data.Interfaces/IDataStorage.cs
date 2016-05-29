namespace WebChat.Domain.Interfaces
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion

    public interface IDataStorage : IDisposable
    {
        IUserRepository Users { get; }
        ICustomerAppRepository Applications { get; }
        IUsersInAppsRepository UsersInApplication { get; }       
        IMessageRepository Messages { get; }
        IDialogRepository Dialogs { get; }
        void Save();
    }
}
