using WebChat.Infrastructure.Services.Interfaces;

namespace WebChat.Infrastructure.Data.Storage.Factories
{
    public abstract class FactoryBase
    {
        private IEntityConverter converter;

        #region Constructors
        public FactoryBase() : this(DependencyResolver.Current.GetService<IEntityConverter>())
        {
        }

        public FactoryBase(IEntityConverter converter)
        {
            this.converter = converter;
        }

        #endregion

        protected IEntityConverter Converter
        {
            get { return this.converter; }
        }
    }
}
