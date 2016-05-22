namespace WebChat.Infrastructure.CQRS.Queries
{
    #region Using

    using WebChat.Infrastructure.Data.Interfaces;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public abstract class BaseQueryHandler
    {
        public BaseQueryHandler(IUnitOfWork uow, IEntityConverter converter)
        {
            UnitOfWork = uow;
            EntityConverter = converter;
        }
        protected IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }

        protected IEntityConverter EntityConverter
        {
            get;
            private set;
        }
    }
}
