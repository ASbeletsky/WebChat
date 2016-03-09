using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.DataAccess.Concrete.DataBase.Statistic_Entities
{
    public class DialogsPerDay
    {
        public int DialogsCount { get; set; }
        public DateTime CurrectDate { get; set; }
    }
}
