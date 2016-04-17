namespace WebChat.Infrastructure.CQRS.Commands
{    
    #region Using

    using WebChat.Infrastructure.Services.Interfaces;
    using Data.Interfaces;

    #endregion

    public abstract class BaseCommandHandler
    {
        public BaseCommandHandler(IUnitOfWork uow, IEntityConverter converter)
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
