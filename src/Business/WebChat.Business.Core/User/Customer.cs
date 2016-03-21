using System.Collections.Generic;
using WebChat.Domain.Core.Application;

namespace WebChat.Business.Core.User
{
    #region Using

    #endregion
    public class Customer : User
    {
        public Customer(string name, string email) : base(name, email)
        {

        }
        #region Properties

        public ICollection<CustomerApplicationModel> myApplications
        {
            get;
            private set;
        }

        #endregion
    }
}
