namespace WebChat.Data.Models.Chat
{
    #region Using

    using Storage.Identity;

    #endregion

    public class UsersInDialogsModel
    {
        public int DialogId { get; set; }
        public long UserId {get;set;}
        public virtual DialogModel Dialog { get; set; }
        public virtual UserModel User { get; set; }
    }
}
