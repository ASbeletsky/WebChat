namespace WebChat.Data.Interfaces
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public interface IRepository<TModel, TKey> where TModel : class
    {
        TModel GetById(TKey id);
        TModel Find(Func<TModel, bool> predicate);
        IEnumerable<TModel> All { get; }
        void Create(TModel item);
        void Update(TModel item);
        void Delete(TKey id);
    }
}
