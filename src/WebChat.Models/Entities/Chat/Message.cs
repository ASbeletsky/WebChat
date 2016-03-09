namespace WebChat.Models.Entities.Chat
{
    #region Using

    using System;
    using WebChat.Models.Entities.Identity;

    #endregion

    public class Message
    {
        public long Id { get; set; }
        public int Dialog_id { get; set; }
        public string Text { get; set; }
        public DateTime SendedAt { get; set; }
        public long Sender_id { get; set; }
        public virtual Dialog Dialog { get; set; }
        public virtual AppUser Sender { get; set; }
    }
}
