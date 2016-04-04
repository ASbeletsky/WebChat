namespace WebChat.Services.Interfaces
{
    #region Using

    using System.Threading.Tasks;

    #endregion

    public interface IQueryHandler<TParameter, TResult>
       where TResult : IQueryResult
       where TParameter : IQuery
    {
        Task<TResult> Retrieve(TParameter query);
    }
}
