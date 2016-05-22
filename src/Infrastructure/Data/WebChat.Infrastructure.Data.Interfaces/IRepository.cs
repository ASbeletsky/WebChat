namespace WebChat.Infrastructure.Data.Interfaces
{
    #region Using

    using Domain.Interfaces;
    using System.Collections.Generic;

    #endregion

    public interface IRepository<TAggregateRoot, TKey> where TAggregateRoot : AggregateRoot<TKey> where TKey: struct
    {
        TAggregateRoot GetById(TKey id);
        IEnumerable<TAggregateRoot> All { get; }
        void Create(TAggregateRoot item);
        void Update(TAggregateRoot item);
        void Delete(TKey id);
    }
}
