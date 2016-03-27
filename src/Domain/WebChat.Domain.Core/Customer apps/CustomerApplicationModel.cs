﻿namespace WebChat.Domain.Core.Application
{ 
#region Using

using System.Collections.Generic;
using Identity;

#endregion

public class CustomerApplicationModel
    {
        private CustomerApplicationModel()
        {
            this.RelatedUsers = new HashSet<UserModel>();
        }

        public CustomerApplicationModel(string appKey, long ownerId, string websiteUrl, string contactEmail) : this()
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
