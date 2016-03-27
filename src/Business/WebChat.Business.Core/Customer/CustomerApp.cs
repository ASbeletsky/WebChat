namespace WebChat.Business.Core.Customer
{
    #region Using

    using Ninject;
    using System.Collections.Generic;
    using System.Linq;
    using WebChat.Business.Core.Identity;
    using WebChat.BusinessLogic.Chat.Entities;
    using WebChat.Domain.Core.Application;
    using WebChat.Domain.Core.Chat;
    using WebChat.Domain.Core.Identity;
    using WebChat.Domain.Interfaces;
    using WebChat.Services.Interfaces;

    #endregion

    public class CustomerApplication
    {

    #region Private Members

        private CustomerApplicationModel AppModel { get; set; }

        [Inject]
        private IUnitOfWork UnitOfWork { get; set; }

        [Inject]
        protected IEntityConverter Converter { get; set; }

        #endregion

        #region Constructors

        public CustomerApplication(string websiteUrl, string subjectScope, string contactEmail, long ownerId)
        {
            this.AppModel.WebsiteUrl = websiteUrl;
            this.AppModel.SubjectScope = subjectScope;
            this.AppModel.ContactEmail = contactEmail;
            this.AppModel.OwnerId = ownerId;
        }

        #endregion

        public int Id 
        {
            get
            {
                return this.AppModel.Id;
            }
        }

        public string AppKey 
        {
            get
            {
                return this.AppModel.AppKey;
            }
        }

        public string WebsiteUrl 
        {
            get
            {
                return this.AppModel.WebsiteUrl;
            }
            set
            {
                this.AppModel.WebsiteUrl = value;
            }
        }

        public string SubjectScope 
        {
            get
            {
                return this.AppModel.SubjectScope;
            }
            set
            {
                this.AppModel.SubjectScope = value;
            }
        }

        public string ContactEmail 
        {
            get
            {
                return this.AppModel.ContactEmail;
            }
            set
            {
                this.AppModel.ContactEmail = value;
            }
        }

        public Customer Owner 
        {
            get
            {
                return Converter.Convert<UserModel, Customer>(this.AppModel.Owner);
            }
        }

        public IEnumerable<User> Clients 
        {
            get
            {
                var clients = from userInApp in UnitOfWork.UsersInApplication.All
                              join userModel in UnitOfWork.Users.All on userInApp.UserId equals userModel.Id
                              where userInApp.AppId == this.Id &&  userModel.Roles.Any(role => role.RoleId == (int) Roles.Client)
                              select new Client(userModel);

                return clients;
            }
        }

        public IEnumerable<Agent> Agents 
        {
            get
            {
                var agents =  from userInApp in UnitOfWork.UsersInApplication.All
                              join userModel in UnitOfWork.Users.All on userInApp.UserId equals userModel.Id
                              where userInApp.AppId == this.Id && userModel.Roles.Any(role => role.RoleId == (int)Roles.Agent)
                              select new Agent(userModel);

                return agents;
            }
        }

        public IEnumerable<Dialog> Dialogs
        {
            get
            {
                var dialogs = UnitOfWork.Dialogs.GetApplicationDialogs(this.Id);
                return Converter.Convert<IEnumerable<DialogModel>, IEnumerable<Dialog>>(dialogs);
            }
        }

    }
}
