namespace WebChat.Infrastructure.CQRS.Interfaces
{
    #region Using

    using System.Threading.Tasks;

    #endregion

    public interface IQueryHandler<TParameter, TResult>
       where TParameter : IQuery
    {
        TResult Execute(TParameter query);
    }
}
