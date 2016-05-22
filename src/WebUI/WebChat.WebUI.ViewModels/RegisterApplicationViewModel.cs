using System.ComponentModel.DataAnnotations;

namespace WebChat.WebUI.ViewModels
{
    public class RegisterApplicationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "URL сайта")]
        public string WebsiteUrl { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Тематика, направление")]
        public string SubjectScope { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Контактный email для этого сайта")]
        public string ContactEmail { get; set; }

    }
}
