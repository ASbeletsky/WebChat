namespace WebChat.WebUI.ViewModels.Operator
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebChat.Data.Models.Application;

    #endregion

    public class RegisterOperatorViewModel
    {
        public RegisterOperatorViewModel()
        {
            this.CustomerApps = new List<CustomerApplicationModel>();
            this.SelectedApps = new List<int>();
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
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

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Моб. телефон")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Ccылка на фото")]
        public string PhotoSrc { get; set; }

        [Required]
        public IEnumerable<int> SelectedApps { get; set; }
        public IEnumerable<CustomerApplicationModel> CustomerApps { get; set; }
    }
}
