using WebChat.Data.Storage.Identity;

namespace WebChat.Data.Models.Application
{
    #region Using

    #endregion

    public class UsersInAppsModel
    {
        public UsersInAppsModel() { }
        public UsersInAppsModel(long userId, int appId)
        {
            this.UserId = userId;
            this.AppId = appId;
        }
        public long UserId
        {
            get;
            set;
        }  

        public int AppId
        {
            get;
            set;
        }

        public virtual UserModel User
        {
            get;
            set;
        }

        public virtual ApplicationModel App
        {
            get;
            set;
        }
    }
}
