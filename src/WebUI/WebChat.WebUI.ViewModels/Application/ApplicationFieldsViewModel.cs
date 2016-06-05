using System.ComponentModel.DataAnnotations;

namespace WebChat.WebUI.ViewModels.Application
{
    public class ApplicationFieldsViewModel
    {
        public int Id { get; set; }
        public long CustomerId { get; set; }

        [Required]
        [Display(Name = "URL сайта")]
        [DataType(DataType.Url)]
        public string WebsiteUrl { get; set; }
        
        [Display(Name = "Тематика, направление")]
        public string Occupation { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Контактный email")]
        public string ContactEmail { get; set; }

        [Display(Name = "Контактный телефон")]
        public string ContactPhone { get; set; }
    }
}
