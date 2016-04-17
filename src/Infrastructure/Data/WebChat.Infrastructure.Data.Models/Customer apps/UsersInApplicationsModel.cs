namespace WebChat.Infrastructure.Data.Models.Application
{
    #region Using

    #endregion

    public class UsersInAppsModel
    {
        private UsersInAppsModel() { }
        public UsersInAppsModel(long userId, int appId)
        {
            this.UserId = userId;
            this.AppId = appId;
        }
        public long UserId
        {
            get;
            private set;
        }  

        public int AppId
        {
            get;
            private set;
        }

    }
}
