namespace WebChat.WebUI.ViewModels.Customer
{
    
    #region Using

    using System.ComponentModel.DataAnnotations;
    using Infrastructure.CQRS.Interfaces;

    #endregion

    public class ApplicationViewModel : IQueryResult
    {
        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Url адрес")]
        public string WebSiteUrl { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Сфера дейтельности")]
        public string Occupation { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Контактный email")]
        public string ContactEmail { get; set; }

    }
}
