//namespace WebChat.Business.Core
//{
//    #region Using

//    using System;
//    using WebChat.Services.Interfaces;

//    #endregion

//    /// <summary>
//    /// Represents a bridge base class of all domain models
//    /// </summary>
//    public abstract class BaseDomainModel : IDisposable
//    {
//        /// <summary>
//        /// Contains entity service
//        /// </summary>
//        public readonly IEntityConverter EntService = default(IEntityConverter);

//        /// <summary>
//        /// Initializes a new instance of the <see cref="BaseDomainModel" /> class
//        /// </summary>
//        public BaseDomainModel(IRequestContext context)
//        {
//            this.EntService = context.Factory.Get<IEntityConverter>();
//        }

//        #region IDisposable Members
//        /// <summary>
//        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
//        /// </summary>
//        public void Dispose()
//        {
//            this.Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        /// Finalizes an instance of the <see cref="BaseDomainModel" /> class
//        /// </summary>
//        ~BaseDomainModel()
//        {
//            this.Dispose(false);
//        }

//        /// <summary>
//        /// Frees managed objects
//        /// </summary>
//        /// <param name="disposeManaged">Flag: Has Dispose already been called?</param>
//        protected virtual void Dispose(bool disposeManaged)
//        {
//        }

//        #endregion IDisposable Members
//    }
//}