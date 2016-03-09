using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat;

namespace WebChat.BusinessLogic.Abstract
{
    public interface IChatItemRepository<T> where T : class
    {
        IEnumerable<T> All { get; }
        T GetById(int Id);
        T Find(Func<T, bool> predicate);
        bool Add(T item);
        //void Update(T item);
        //void Delete(dynamic Id);
    }
}
