namespace WebChat.Business.Chat.Entities
{
    public abstract class ChatMember
    {
        public ChatMember(long userId, int appId)
        {
            this.UserId = userId;
            this.AppId = appId;
        }
        public long UserId { get; set; }

        public string Name { get; set; }

        public string HubUserId
        {
            get
            {
                return UserId.ToString();
            }
        }

        

        public int AppId { get; set; }
    }
}
