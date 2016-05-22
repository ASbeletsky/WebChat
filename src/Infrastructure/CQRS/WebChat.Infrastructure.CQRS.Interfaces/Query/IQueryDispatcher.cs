namespace WebChat.Infrastructure.CQRS.Interfaces
{   
    #region Using

    using System.Threading.Tasks;

    #endregion

    public interface IQueryStorage
    {
        TResult RunQuery<TParameter, TResult>(TParameter query)
            where TParameter : IQuery;
    }
}
