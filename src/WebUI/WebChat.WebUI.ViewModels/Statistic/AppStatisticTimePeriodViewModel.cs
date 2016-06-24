using System;
using System.ComponentModel.DataAnnotations;

namespace WebChat.WebUI.ViewModels.Statistic
{
    public class AppStatisticTimePeriodViewModel
    {
        public int AppId { get; set; }

        [Display(Name = "Показать с")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "до")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
    }
}
