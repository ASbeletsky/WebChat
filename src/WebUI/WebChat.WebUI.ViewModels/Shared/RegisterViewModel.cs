namespace WebChat.WebUI.ViewModels.Shared
{
    #region Using

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class RegisterViewModel
    {
         [Required]
         [DataType(DataType.Text)]
         [Display(Name = "Ваше имя")]
         public string Name { get; set; }

         [Required]
         [EmailAddress]
         [Display(Name = "Адрес эл. почты")]
         public string Email { get; set; }

         [Required]
         [DataType(DataType.Password)]
         [Display(Name = "Пароль")]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Подтверждение")]
         [Compare("Password", ErrorMessage = "Пароли не совпадают")]
         public string ConfirmPassword { get; set; }
        
    }
}
