using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Domain.Core.Identity;

namespace WebChat.Business.Core.Identity
{
    public class Agent : User
    {
        public Agent(long agentId) : base(agentId)
        {

        }

        public Agent(UserModel model) : base(model)
        {

        }
    }
}
