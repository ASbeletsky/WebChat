using System;
using System.ComponentModel.DataAnnotations;

namespace WebChat.WebUI.ViewModels.Statistic
{
    public class AppStatisticTimePeriodViewModel
    {
        public int AppId { get; set; }
        [Display(Name = "C")]
        public DateTime StartDate { get; set; }
        [Display(Name = "По")]
        public DateTime EndDate { get; set; }
    }
}
