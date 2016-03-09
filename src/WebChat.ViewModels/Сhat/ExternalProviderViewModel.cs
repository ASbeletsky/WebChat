using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.WebUI.Models.Сhat
{
    public class ExternalProviderViewModel
    {
        public AuthenticationDescription Provider { get; set; }
        public string IconClass { get; set; }
        public string ReferenceClass { get; set; }        
    }
}
