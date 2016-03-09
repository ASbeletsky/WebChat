using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat.Entities;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.UnitTest.Balancers
{
    [TestClass]
    public class BalancerByWorkingTimeTest
    {
        private List<ChatAgent> agents;

        [TestInitialize]
        public void Init()
        {
            agents = new List<ChatAgent>();
            agents.Add(new ChatAgent { AsUser = new AppUser() { } });
        }
        [TestMethod]
        public void Can_Get_Free_Agent()
        {

        }
    }
}
