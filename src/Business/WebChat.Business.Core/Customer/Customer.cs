namespace WebChat.Business.Core.Customer
{
    #region Using

    using Ninject;
    using System.Collections.Generic;
    using WebChat.Business.Core.Identity;
    using WebChat.Domain.Core.Application;
    using WebChat.Domain.Interfaces.Repositories;

    #endregion
    public class Customer : User
    {

     #region Private Members

        [Inject]
        private ICustomerAppRepository CustomerAppRepository 
        {
            get;
            set;
        }

        private IEnumerable<CustomerApplication> Applications 
        {
            get
            {
                var myApps = CustomerAppRepository.GetApplicationsByOwner(this.Id);
                return Converter.Convert<IEnumerable<CustomerApplicationModel>, IEnumerable<CustomerApplication>>(myApps);
            }
            set
            {
                Applications = value;
            }
        }

    #endregion

     #region Constructors

        public Customer(string name, string email) : base(name, email)
        {

        }

    #endregion

    #region Properties

        public IReadOnlyCollection<CustomerApplication> MyApplications
        {
            get
            {
                return Applications as IReadOnlyCollection<CustomerApplication>;
            }
        }

        public void AddApplication(CustomerApplication application)
        {
            var app = Converter.Convert<CustomerApplication, CustomerApplicationModel>(application);
            CustomerAppRepository.Create(app);
        }

    #endregion

    }
}
