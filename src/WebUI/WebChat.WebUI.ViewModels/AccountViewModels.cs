//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using WebChat.DataAccess.Concrete.Entities.Customer_apps;

//namespace WebChat.WebUI.Models
//{
//    public class ExternalLoginConfirmationViewModel
//    {
//        [Required]
//        [Display(Name = "Адрес электронной почты")]
//        public string Email { get; set; }
//    }

//    public class ExternalLoginListViewModel
//    {
//        public string ReturnUrl { get; set; }
//    }

//    public class SendCodeViewModel
//    {
//        public string SelectedProvider { get; set; }
//        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
//        public string ReturnUrl { get; set; }
//        public bool RememberMe { get; set; }
//    }

//    public class VerifyCodeViewModel
//    {
//        [Required]
//        public string Provider { get; set; }

//        [Required]
//        [Display(Name = "Код")]
//        public string Code { get; set; }
//        public string ReturnUrl { get; set; }

//        [Display(Name = "Запомнить браузер?")]
//        public bool RememberBrowser { get; set; }

//        public bool RememberMe { get; set; }
//    }

//    public class ForgotViewModel
//    {
//        [Required]
//        [Display(Name = "Адрес электронной почты")]
//        public string Email { get; set; }
//    }

//    public class LoginViewModel
//    {
//        [Required]
//        [Display(Name = "Email")]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [Display(Name = "Remember me?")]
//        public bool RememberMe { get; set; }
//    }

    


//    public class RegisterOperatorViewModel
//    {
//        public RegisterOperatorViewModel()
//        {
//            this.CustomerApps = new List<CustomerApplication>();
//            this.SelectedApps = new List<int>();
//        }
//        [Required]
//        [DataType(DataType.Text)]
//        [Display(Name = "Имя")]
//        public string Name { get; set; }

//        [Required]
//        [EmailAddress]
//        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid e-mail.Please try again.")]
//        [Display(Name = "Адрес эл. почты")]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Пароль")]
//        public string Password { get; set; }

//        [DataType(DataType.PhoneNumber)]
//        [Display(Name = "Моб. телефон")]
//        public string Phone { get; set; }

//        [DataType(DataType.Text)]
//        [Display(Name = "Ccылка на фото")]
//        public string PhotoSrc { get; set; }

//        [Required]
//        public List<int> SelectedApps { get; set; }
//        public virtual List<CustomerApplication> CustomerApps { get; set; }
//    }

//    public class ResetPasswordViewModel
//    {
//        [Required]
//        [EmailAddress]
//        [Display(Name = "Адрес электронной почты")]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Пароль")]
//        public string Password { get; set; }

//        [DataType(DataType.Password)]
//        [Display(Name = "Подтверждение пароля")]
//        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
//        public string ConfirmPassword { get; set; }

//        public string Code { get; set; }
//    }

//    public class ForgotPasswordViewModel
//    {
//        [Required]
//        [EmailAddress]
//        [Display(Name = "Почта")]
//        public string Email { get; set; }
//    }
//}
