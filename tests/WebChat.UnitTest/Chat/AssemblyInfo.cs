using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebChat.BusinessLogic.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat.Entities;
using WebChat.BusinessLogic.Managers;

namespace WebChat.BusinessLogic.Chat.Tests
{

    [TestClass()]
    public class AssemblyInfo
    {
        [TestMethod()]
        public void FreeAgentTest()
        {
            //этот тест работать не должен, он для хардкоженого балансировщика

            AgentManager agentManager = new AgentManager();

            List<ChatAgent> avableAgents = new List<ChatAgent>();
            ChatAgent chatAgent1 = new ChatAgent();
            ChatAgent chatAgent2 = new ChatAgent();
            ChatAgent chatAgent3 = new ChatAgent();
            avableAgents.Add(chatAgent1);
            avableAgents.Add(chatAgent2);
            avableAgents.Add(chatAgent3);

            ChatAgent foundAgent = agentManager.FreeAgent();
            
            Assert.AreEqual(chatAgent3, foundAgent);
        }
    }
}