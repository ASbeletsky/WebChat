namespace WebChat.WebUI.ViewModels.Customer
{
    #region Using

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class ApplicationViewModel
    {
        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Url адрес сайта")]
        public string WebSiteUrl { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The field must be maximum 256 characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Сфера дейтельности сайта")]
        public string Occupation { get; set; }

    }
}
