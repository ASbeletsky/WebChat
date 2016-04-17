namespace WebChat.Infrastructure.CQRS.Queries
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using Interfaces;
    using Services.Interfaces;

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

        public async Task<TResult> Execute<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
            where TResult : IQueryResult
        {
            var handler = container.GetService<IQueryHandler<TParameter, TResult>>();
            return await handler.Retrieve(query);
        }
    }
}
