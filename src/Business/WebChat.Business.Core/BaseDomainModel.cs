namespace WebChat.BusinessLogic.DomainModels
{
    #region Using

    using Ninject;
    using Services.Interfaces;
    using System;

    #endregion

    /// <summary>
    /// Represents a bridge base class of all domain models
    /// </summary>
    public abstract class BaseDomainModel : IDisposable
    {
        private readonly IEntityConverter converter;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="BaseDomainModel" /> class
        ///// </summary>
        //public BaseDomainModel()
        //{
        //    this.converter = this.Factory.GetService<IEntityConverter>();
        //}

        [Inject]
        public IEntityConverter Converter
        {
            get;
        }

        //public IDependencyContainer Factory
        //{
        //    get
        //    {
        //       return ApplicationBootstrap.GetModelContainer();
        //    }
        //}

        #region IDisposable Members
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseDomainModel" /> class
        /// </summary>
        ~BaseDomainModel()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Frees managed objects
        /// </summary>
        /// <param name="disposeManaged">Flag: Has Dispose already been called?</param>
        protected virtual void Dispose(bool disposeManaged)
        {
        }

        #endregion IDisposable Members
    }
}
