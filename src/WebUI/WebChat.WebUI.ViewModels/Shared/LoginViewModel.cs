using System.ComponentModel.DataAnnotations;

namespace WebChat.WebUI.ViewModels.Shared
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адрес эл. почты")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
    }
}
