namespace WebChat.Data.Models.Application
{
    #region Using

    using System.Collections.Generic;
    using Storage.Identity;

    #endregion

    public class ApplicationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public long CustomerId { get; set; }

        public string WebsiteUrl { get; set; }

        public string Occupation { get; set; }

        public string ContactEmail { get; set; }

        public virtual UserModel Customer { get; set; }

        public virtual ICollection<UsersInAppsModel> RelatedUsers { get; set; }
    }
}
