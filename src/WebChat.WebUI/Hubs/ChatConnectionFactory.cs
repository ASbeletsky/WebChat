using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChat.WebUI.Hubs
{
    public class ChatConnectionFactory : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.User != null && request.User.Identity != null)
            {
                return request.User.Identity.GetUserId<long>().ToString();
            }

            return null;
        }
    }
}
