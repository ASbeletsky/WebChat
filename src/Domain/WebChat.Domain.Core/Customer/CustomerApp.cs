namespace WebChat.Business.Core.Customer
{
    #region Using


    #endregion

    public class CustomerApplication
    {
        #region Constructors

        public CustomerApplication(string websiteUrl, string subjectScope, string contactEmail, long ownerId)
        {
        }

        #endregion

        public int Id 
        {
            get;
        }

        public string AppKey 
        {
            get;
        }

        public string WebsiteUrl 
        {
            get;
        }

        public string SubjectScope 
        {
            get;
        }

        public string ContactEmail 
        {
            get;           
        }

        public Customer Owner 
        {
            get;
        }

        //public IEnumerable<User> Clients 
        //{
        //    get
        //    {
        //        var clients = from userInApp in UnitOfWork.UsersInApplication.All
        //                      join userModel in UnitOfWork.Users.All on userInApp.UserId equals userModel.Id
        //                      where userInApp.AppId == this.Id &&  userModel.Roles.Any(role => role.RoleId == (int) Roles.Client)
        //                      select new Client(userModel);

        //        return clients;
        //    }
        //}

        //public IEnumerable<Agent> Agents 
        //{
        //    get
        //    {
        //        var agents =  from userInApp in UnitOfWork.UsersInApplication.All
        //                      join userModel in UnitOfWork.Users.All on userInApp.UserId equals userModel.Id
        //                      where userInApp.AppId == this.Id && userModel.Roles.Any(role => role.RoleId == (int)Roles.Agent)
        //                      select new Agent(userModel);

        //        return agents;
        //    }
        //}

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
