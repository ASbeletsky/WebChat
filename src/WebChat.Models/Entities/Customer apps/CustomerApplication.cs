namespace WebChat.Models.Entities.CustomerApps
{
    #region Using

    using System.Collections.Generic;
    using WebChat.Models.Entities.Identity;

    #endregion

    public class CustomerApplication
    {
        public CustomerApplication()
        {
            RelatedUsers = new HashSet<AppUser>();
        }
        public int Id { get; set; }       
        public string AppKey { get; set; }
        public long OwnerUser_Id { get; set; }
        public string WebsiteUrl { get; set; }
        public string SubjectScope { get; set; }
        public string ContactEmail { get; set; }
        public virtual AppUser Owner { get; set; }
        public virtual ICollection<AppUser> RelatedUsers { get; set; }
    }
}
