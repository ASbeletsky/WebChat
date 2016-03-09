using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.DataAccess.Concrete.DTO
{
    public class StoredMessageDTO
    {
        public int DialogId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
