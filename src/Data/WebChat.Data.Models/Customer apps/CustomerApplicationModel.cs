namespace WebChat.Data.Models.Application
{
    #region Using

    using System.Collections.Generic;
    using Storage.Identity;

    #endregion

    public class CustomerApplicationModel
    {
        public int Id { get; set; }
            
        public long OwnerId { get; set; }

        public string WebsiteUrl { get; set; }

        public string SubjectScope { get; set; }

        public string ContactEmail { get; set; }

        public virtual UserModel Owner { get; set; }

        public virtual ICollection<UserModel> RelatedUsers { get; set; }
    }
}
