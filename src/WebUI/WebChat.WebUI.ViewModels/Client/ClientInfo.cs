using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.WebUI.ViewModels.Client
{
    public class ClientInfo
    {
        public string Email { get; set; }
        public string Address { get; set; }
        public string[] SocialNetworkLinks { get; set; }
        public string UserType { get; set; }
        public int DialogsCount { get; set; }

        public DateTime? LastDialogDate { get; set; }

        public string LastFormatedDateString
        {
            get
            {
                return this.LastDialogDate.HasValue ? this.LastDialogDate.Value.ToShortDateString() : null;
            }
        }
    }
}
