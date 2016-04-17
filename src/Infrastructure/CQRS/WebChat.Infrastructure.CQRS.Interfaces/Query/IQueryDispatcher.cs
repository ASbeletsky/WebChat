namespace WebChat.Infrastructure.CQRS.Interfaces
{   
    #region Using

    using System.Threading.Tasks;

    #endregion

    public interface IQueryDispatcher
    {
        Task<TResult> Execute<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult;
    }
}
