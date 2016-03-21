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
         [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid e-mail.Please try again.")]
         [Display(Name = "Адрес эл. почты")]
         public string Email { get; set; }

         [Required]
         [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
         [DataType(DataType.Password)]
         [Display(Name = "Пароль")]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Подтверждение пароля")]
         [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
         public string ConfirmPassword { get; set; }
        
    }
}
