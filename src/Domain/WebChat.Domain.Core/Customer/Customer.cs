namespace WebChat.Domain.Core.Customer
{
    using Infrastructure.CQRS.Interfaces;
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Domain.Core.Identity;

    #endregion
    public class Customer : User
    {
        private string name;
        private string email;

        public Customer(string name, string email)
        {
            this.name = name;
            this.email = email;
        }

        #region Properties

        public IReadOnlyCollection<Application> getApplications()
        {
            throw new NotImplementedException();
        }

        public void AddApplication(Application application)
        {
            throw new NotImplementedException();
        }

    #endregion

    }
}
