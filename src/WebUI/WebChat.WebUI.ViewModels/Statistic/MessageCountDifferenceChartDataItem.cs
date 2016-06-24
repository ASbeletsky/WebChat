using System;
using System.Globalization;

namespace WebChat.WebUI.ViewModels.Statistic
{
    public class MessageCountDifferenceChartDataItem
    {
        public int? MessagesInCurrectMoth { get; set; }
        public int? MessagesInPreviosMonth { get; set; }

        public int MessagesInCurrect
        {
            get
            {
                return this.MessagesInCurrectMoth ?? 0;
            }
        }

        public int MessagesInPrevios
        {
            get
            {
                return this.MessagesInPreviosMonth ?? 0;
            }
        }
        
        public string Today
        {
            get
            {
                return string.Format("{0:yyyy-MM}", DateTime.Today);
            }
        }
    }
}
