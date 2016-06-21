namespace WebChat.Business.Interfaces
{
    #region Using

    using System;
    using System.Collections.Generic;

    #endregion 
    public interface IChatItemRepository<T> where T : class
    {
        IEnumerable<T> All { get; }
        T GetById(int Id);
        T Find(Func<T, bool> predicate);
        bool Add(T item);
    }
}

