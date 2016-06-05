namespace WebChat.Data.Models.Application
{
    #region Using

    using System.Collections.Generic;
    using Storage.Identity;
    using System.Linq;

    #endregion

    public class ApplicationModel
    {
        public int Id { get; set; }
        public string ContactPhone { get; set; }

        public long CustomerId { get; set; }

        public string WebsiteUrl { get; set; }

        public string Occupation { get; set; }

        public string ContactEmail { get; set; }
        public string Script { get; set; }

        public virtual UserModel Customer { get; set; }

        public virtual ICollection<UsersInAppsModel> UsersShortInfo { get; set; }

        public IEnumerable<UserModel> Users
        {
            get
            {
                return this.UsersShortInfo.Select(userInfo => userInfo.User);
            }
        }
    }
}
