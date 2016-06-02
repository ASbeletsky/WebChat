using System.ComponentModel;

namespace WebChat.WebUI.ViewModels.Application
{
    public class AppUsersAndChatsInfoViewModel
    {
        [DisplayName("Операторов")]
        public int AgentsCount { get; set; }

        [DisplayName("Пользователей")]
        public int ClientsCount { get; set; }

        [DisplayName("Диалогов")]
        public int DialogsCount { get; set; }

        [DisplayName("Сообщений")]
        public int MessagesCount { get; set; }
    }
}
