namespace WebChat.Business.Core.User
{
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Domain.Core.Application;

    #endregion

    public abstract class User
    {
        #region Private Members

        private string _photoSource;

        #endregion

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        #region Public Properties
        public long Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string UserName
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }

        public DateTime RegistrationDate
        {
            get;
            private set;
        }

        public ICollection<CustomerApplicationModel> RelatedApplications
        {
            get;
            private set;
        }

        public string PhotoSource
        {
            get
            {
                if (string.IsNullOrEmpty(_photoSource))
                    return "../../../Content/Images/default-user-image.png";
                else
                    return _photoSource;
            }
            private set
            {
                this._photoSource = value;
            }
        }

        #endregion
    }
}
