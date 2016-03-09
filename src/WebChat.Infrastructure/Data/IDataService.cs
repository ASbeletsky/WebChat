namespace WebChat.DataAccess.Data
{
    #region Using

    using System;
    using Models.Entities.Chat;
    using WebChat.Models.Entities.CustomerApps;
    using WebChat.Models.Entities.Identity;
    using System.Threading.Tasks;

    #endregion

    public interface IDataService : IDisposable
    {
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<CustomerApplication> CustomerApplications { get; }
        IRepository<AppUser> Users { get; }
        Task<TResult> ExecuteCommand<TResult>(string command, params object[] parameters);
        void Save();
    }
}
