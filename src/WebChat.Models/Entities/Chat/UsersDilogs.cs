namespace WebChat.Models.Entities.Chat
{
    #region Using

    using WebChat.Models.Entities.Identity;

    #endregion

    /// <summary>
    /// Вспомогатальная таблица многие ко многим
    /// </summary>
    public class UsersDialogs
    {
        public int Dialog_Id { get; set; }
        public long User_Id {get;set;}
        public virtual Dialog Dialog { get; set; }
        public virtual AppUser User { get; set; }
    }
}
