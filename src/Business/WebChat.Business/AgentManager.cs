//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Chat.Balancers;
//using WebChat.BusinessLogic.Chat.Entities;
//using WebChat.BusinessLogic.Chat.Storages;
//using WebChat.DataAccess.Concrete;
//using WebChat.DataAccess.Concrete.Entities.Identity;

//namespace WebChat.BusinessLogic.Managers
//{
//    /// <summary>
//    /// Managing agents
//    /// </summary>
//    public class AgentManager
//    {
//        private static AgentsStorage AgentsStorage { get; set; }

//        static AgentManager()
//        {
//            AgentsStorage = AgentsStorage.Instance;
//        }
//        /// <summary>
//        /// Returns all agents from AgentsStorage
//        /// </summary>
//        public IEnumerable<ChatAgent> All
//        {
//            get
//            {
//                return AgentsStorage.All.AsEnumerable();
//            }
//        } 

//        public ChatAgent GetByUserId(string userId)
//        {
//            int id = int.Parse(userId);
//            return AgentsStorage.Find(a => a.AsUser.Id == id);
//        }


//        public bool IsOnline(string userId)
//        {
//            long id = long.Parse(userId);
//            return IsOnline(id);
//        }
//        public bool IsOnline(long userId)
//        {
//            return AgentsStorage.All.Any(a => a.AsUser.Id == userId);
//        }

//        /// <summary>
//        /// Returns the most free agent. Algorithm of choosing the most free agent depends on Balancer.cs config
//        /// </summary>
//        /// <returns>The most free agent</returns>
//        public ChatAgent FreeAgent()
//        {
//            var agents = AgentsStorage.All;
//            if (agents.Count() > 0)
//            {
//                Balancer balancer = Balancer.GetInstance(BalancerType.WorkingTime, agents);
//                return balancer.GetFreeAgent();
//            }
//            return null;   

//            //ChatAgent freeAgent = balancer.GetFreeAgentByWorkTime(AgentsStorage.All);
//            //return freeAgent;           
//        }
//        /// <summary>
//        /// Adds the agent passed in parameter to AgentsStorage
//        /// </summary>
//        /// <param name="agent">Instance of object to be added</param>
//        public void ConnectAgent(ChatAgent agent)
//        {           
//                AgentsStorage.Add(agent);
//        }
//        /// <summary>
//        /// Removes the agent passed in parameter from AgentsStorage
//        /// </summary>
//        /// <param name="agent">Instance of object to be removed</param>
//        public void Remove(ChatAgent agent)
//        {
//            AgentsStorage.Remove(agent); //не реализован на уровне ниже
//        }
//    }
//}
