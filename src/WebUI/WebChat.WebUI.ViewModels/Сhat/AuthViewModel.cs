//using Microsoft.Owin.Security;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WebChat.WebUI.Models.Сhat
//{
//    public class AuthViewModel
//    {
//        [Required(ErrorMessage = "Укажите свое имя")]
//        [Display(Name = "Имя:")]
//        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 20 символов")]
//        public string UserName { get; set; }

//        [Display(Name = "Почта:")]
//        [DataType(DataType.EmailAddress)]
//        public string Email { get; set; }
//        public IEnumerable<ExternalProviderViewModel> loginProviders { get; set; }
//    }
//}
