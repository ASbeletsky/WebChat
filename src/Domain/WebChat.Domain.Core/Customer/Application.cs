namespace WebChat.Domain.Core.Customer
{
    #region Using

    using System.Collections.Generic;
    using WebChat.Domain.Core.Identity;
    using WebChat.Domain.Interfaces;
    using WebChat.Infrastructure.CQRS.Queries.Application;


    #endregion

    public class Application : AggregateRoot<int>
    {
        private long ownerId;

        #region Constructors

        public Application(string websiteUrl, string subjectScope, string contactEmail, long ownerId)
        {
            this.WebsiteUrl = websiteUrl;
            this.SubjectScope = subjectScope;
            this.ContactEmail = contactEmail;
            this.ownerId = ownerId;
        }

        #endregion

        public string WebsiteUrl { get; private set; }

        public string SubjectScope { get; private set; }

        public string ContactEmail { get; private set; }

        public Customer GetOwner()
        {
            var getOwnerQuery = new GetCustomerQuery(customerId: this.ownerId);
            var owner = Queries.RunQuery<GetCustomerQuery, Customer>(getOwnerQuery);
            return owner;
        }

        public IEnumerable<Client> GetClients()
        {           
            var getClientsQuery = new GetApplicationClientsQuery(applicationId: this.Id);
            var clients = Queries.RunQuery<GetApplicationClientsQuery, IEnumerable<Client>>(getClientsQuery);
            return clients;
        }

        public IEnumerable<Agent> GetAgents()
        {
            var getAgentsQuery = new GetApplicationAgentsQuery(applicationId: this.Id);
            var agents = Queries.RunQuery<GetApplicationAgentsQuery, IEnumerable<Agent>>(getAgentsQuery);
            return agents;
        }



        //public IEnumerable<Dialog> Dialogs
        //{
        //    get
        //    {
        //        var dialogs = UnitOfWork.Dialogs.GetApplicationDialogs(this.Id);
        //        return Converter.Convert<IEnumerable<DialogModel>, IEnumerable<Dialog>>(dialogs);
        //    }
        //}

    }
}
