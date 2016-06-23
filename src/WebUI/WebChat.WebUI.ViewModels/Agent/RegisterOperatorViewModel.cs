namespace WebChat.WebUI.ViewModels.Agent
{
    #region Using

    using Shared;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebChat.Data.Models.Application;

    #endregion

    public class RegisterAgentViewModel : RegisterViewModel
    {
        public RegisterAgentViewModel()
        { 
            this.SelectedApps = new List<int>();
        }
      
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Моб. телефон")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Ccылка на фото")]
        public string PhotoSrc { get; set; }

        [Required]
        public IEnumerable<int> SelectedApps { get; set; }
    }
}
