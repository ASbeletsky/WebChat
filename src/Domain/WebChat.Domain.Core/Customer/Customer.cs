namespace WebChat.Business.Core.Customer
{
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Business.Core.Identity;

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

        public IReadOnlyCollection<CustomerApplication> getApplications()
        {
            throw new NotImplementedException();
        }

        public void AddApplication(CustomerApplication application)
        {
            throw new NotImplementedException();
        }

    #endregion

    }
}
