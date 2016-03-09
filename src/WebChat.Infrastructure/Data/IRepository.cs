namespace WebChat.DataAccess.Data
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> All { get; }
        T GetById(dynamic Id);
        T Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(dynamic Id);
    }
}
