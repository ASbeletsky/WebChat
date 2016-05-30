namespace WebChat.Data.Interfaces
{
   
    #region Using

    using System;
    using Data.Interfaces.Repositories;

    #endregion

    public interface IDataStorage : IDisposable
    {
        IUserRepository Users { get; }
        ICustomerAppRepository Applications { get; }
        IUsersInAppsRepository UsersInApplication { get; }       
        IMessageRepository Messages { get; }
        IDialogRepository Dialogs { get; }
        IUsersInDialogsRepository UsersInDialogs { get; }
        IUsersInRolesRepository UsersInRoles { get; }
        void Save();
    }
}
