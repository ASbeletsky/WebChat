namespace WebChat.Infrastructure.CQRS.Queries
{
    #region Using

    using System;
    using Interfaces;
    using Services.Interfaces;

    #endregion

    public class QueryStorage : IQueryStorage
    {
        IDependencyResolver container;

        public QueryStorage(IDependencyResolver container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public TResult RunQuery<TParameter, TResult>(TParameter query)
            where TParameter : IQuery
        {
            var handler = container.GetService<IQueryHandler<TParameter, TResult>>();
            return handler.Execute(query);
        }
    }
}
