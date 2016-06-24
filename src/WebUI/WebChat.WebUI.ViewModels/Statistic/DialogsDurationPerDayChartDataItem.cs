using System;

namespace WebChat.WebUI.ViewModels.Statistic
{
    public class DialogsDurationPerDayChartDataItem
    {
        public TimeSpan ChatDuration { get; set; }
        public DateTime CurrentDate { get; set; }

        public int DurationInMinutes
        {
            get
            {
                return this.ChatDuration.Minutes;
            }
        }

        public string FormatedDate
        {
            get
            {
                return String.Format("{0:yyyy-MM-dd}", this.CurrentDate);
            }
        }
    }
}
