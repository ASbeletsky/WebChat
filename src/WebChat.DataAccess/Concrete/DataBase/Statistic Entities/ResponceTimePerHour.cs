using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.DataAccess.Concrete.DataBase.Statistic_Entities
{
    public class ResponceTimePerHour
    {
        public int Hour { get; set; }
        public TimeSpan ResponceTime { get; set; }
    }
}
