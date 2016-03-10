namespace WebChat.Infrastructure.Data
{
    #region Using

    using System;
    using Models.Entities.Chat;
    using WebChat.Models.Entities.CustomerApps;
    using WebChat.Models.Entities.Identity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    #endregion

    public interface IDataService : IDisposable
    {
        IRepository<Message> Messages { get; }
        IRepository<Dialog> Dialogs { get; }
        IRepository<CustomerApplication> CustomerApplications { get; }
        IRepository<AppUser> Users { get; }
        IEnumerable<TResult> ExecuteCollectionQuery<TResult>(string collectionQuery, params object[] parameters);
        void Save();
    }
}
