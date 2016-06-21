using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using WebChat.Business.Chat.Entities;
using WebChat.BusinessLogic.Chat.Balancers;
using WebChat.Data.Models.Chat;

namespace WebChat.UnitTest.Balancers
{
    [TestClass]
    public class BalancerByWorkingTimeTest
    {
        BalancerByWorkingTime balancer;
        private List<ChatAgent> agents;
        private List<MessageModel> messages;

        [TestInitialize]
        public void Init()
        {       
            balancer = new BalancerByWorkingTime(agents);
        }


        [TestMethod]
        public void Can_Get_Free_Agent()
        {
             
        }
    }
}
