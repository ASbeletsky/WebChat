namespace WebChat.Domain.Interfaces
{
    #region Using

    using Infrastructure.CQRS.Interfaces;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public abstract class AggregateRoot<TKey>
        : Entity<TKey> where TKey: struct
    {
        protected IQueryStorage Queries { get; private set; }

        protected AggregateRoot()
        {
            this.Queries = DependencyResolver.Current.GetService<IQueryStorage>();
        }        
    }
}
