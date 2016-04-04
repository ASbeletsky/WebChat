namespace WebChat.Services.Interfaces
{   
    #region Using

    using System.Threading.Tasks;

    #endregion

    public interface IQueryDispatcher
    {
        Task<TResult> Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult;
    }
}
