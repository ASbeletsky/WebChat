using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.DataAccess.Concrete.DataBase.Statistic_Entities
{
    public class ChatDurationPerDay
    {
        public TimeSpan ChatDuration { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
