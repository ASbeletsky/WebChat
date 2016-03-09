using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.DataAccess.Concrete.DataBase.Statistic_Entities
{
    public class MessagesByMonth
    {
        public int MonthNumber { get; set; }
        public int? MessagesInCurrectMoth { get; set; }
        public int? MessagesInPreviosMonth { get; set; }
    }
}
