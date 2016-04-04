namespace WebChat.Services.CQRS
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using WebChat.Services.Interfaces;

    #endregion

    public class QueryDispatcher : IQueryDispatcher
    {
        IDependencyContainer container;

        public QueryDispatcher(IDependencyContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public async Task<TResult> Dispatch<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = container.GetService<IQueryHandler<TParameter, TResult>>();
            return await handler.Retrieve(query);
        }
    }
}
