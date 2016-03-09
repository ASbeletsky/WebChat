using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.WebUI.Models.Statistic
{
    public class AppDateTimeStatisticSelector
    {
        public int appId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
