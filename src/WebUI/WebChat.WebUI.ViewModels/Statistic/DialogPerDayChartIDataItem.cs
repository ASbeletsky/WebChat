using System;

namespace WebChat.WebUI.ViewModels.Statistic
{
    public class DialogPerDayChartIDataItem
    {
        public int DialogsCount { get; set; }
        public DateTime CurrentDate { get; set; }

        public string FormatedDate
        {
            get
            {
                return String.Format("{0:yyyy-MM-dd}", this.CurrentDate);
            }
        }
    }
}
