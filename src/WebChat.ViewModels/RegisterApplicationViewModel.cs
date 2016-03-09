using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.WebUI.Models
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
