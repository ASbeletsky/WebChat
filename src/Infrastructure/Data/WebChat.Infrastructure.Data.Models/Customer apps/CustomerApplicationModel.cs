namespace WebChat.Infrastructure.Data.Models.Application
{ 
#region Using

using System.Collections.Generic;
using Identity;

#endregion

public class ApplicationModel
    {
        private ApplicationModel()
        {
            this.RelatedUsers = new HashSet<UserModel>();
        }

        public ApplicationModel(string appKey, long ownerId, string websiteUrl, string contactEmail) : this()
        {
            this.AppKey = appKey;
            this.OwnerId = ownerId;
            this.WebsiteUrl = websiteUrl;
            this.ContactEmail = contactEmail;
        }

        public int Id
        {
            get;
            private set;
        }   
            
        public string AppKey
        {
           get;
           private set;
        }

        public long OwnerId
        {
            get;
            set;
        }

        public string WebsiteUrl
        {
            get;
            set;
        }

        public string SubjectScope
        {
            get;
            set;
        }

        public string ContactEmail
        {
            get;
            set;
        }

        public virtual UserModel Owner
        {
            get;
            private set;
        }

        public virtual ICollection<UserModel> RelatedUsers
        {
            get;
            private set;
        }
    }
}
