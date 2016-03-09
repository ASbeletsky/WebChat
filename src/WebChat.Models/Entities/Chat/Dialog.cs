namespace WebChat.Models.Entities.Chat
{
    #region Using

    using System;
    using System.Collections.Generic;
    using WebChat.Models.Entities.Identity;

    #endregion

    public class Dialog
    {
        public Dialog()
        {
            Messages = new HashSet<Message>();
            Users = new HashSet<AppUser>();
        }

        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
    }
}
