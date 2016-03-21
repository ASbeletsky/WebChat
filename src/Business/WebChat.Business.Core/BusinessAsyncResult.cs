namespace WebChat.Business.Core
{

    #region Using

    using System;
    using System.Collections;

    #endregion
    public class BusinessLogicException : Exception
    {
        private IDictionary _errors;

        public BusinessLogicException() : base() { }
        public BusinessLogicException(IDictionary errors) : this()
        {
            this._errors = errors;
        }

        public BusinessLogicException(string message) : base(message) { }
        public BusinessLogicException(string message, IDictionary errors) : this(message)
        {
            this._errors = errors;
        }

        public BusinessLogicException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public override IDictionary Data
        {
            get
            {
                return _errors;
            }
        }
    }
}
