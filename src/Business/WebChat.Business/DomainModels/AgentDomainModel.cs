namespace WebChat.Business.DomainModels
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using WebChat.Business.Chat.Entities;
    using WebChat.Business.Chat.Stotages;
    using WebChat.BusinessLogic.Chat.Balancers;

    #endregion

    public class AgentDomainModel
    {
        private static AgentStorage Agents { get; set; }

        static AgentDomainModel()
        {
            Agents = AgentStorage.Instance;
        }
        /// <summary>
        /// Returns all agents from AgentsStorage
        /// </summary>
        public IEnumerable<ChatAgent> All
        {
            get
            {
                return Agents.All.AsEnumerable();
            }
        }

        public ChatAgent GetByUserId(string userId)
        {
            int id = int.Parse(userId);
            return Agents.Find(a => a.UserId == id);
        }


        public bool IsOnline(string userId)
        {
            long id = long.Parse(userId);
            return IsOnline(id);
        }
        public bool IsOnline(long userId)
        {
            return Agents.All.Any(a => a.UserId == userId);
        }

        /// <summary>
        /// Returns the most free agent. Algorithm of choosing the most free agent depends on Balancer.cs config
        /// </summary>
        /// <returns>The most free agent</returns>
        public ChatAgent FreeAgent(int appId)
        {
            var agents = Agents.All.Where(agent => agent.AppId == appId);
            if (agents.Any())
            {
                Balancer balancer = Balancer.GetInstance(BalancerType.WorkingTime, agents);
                return balancer.GetFreeAgent();
            }
            return null;          
        }

        /// <summary>
        /// Adds the agent passed in parameter to AgentsStorage
        /// </summary>
        /// <param name="agent">Instance of object to be added</param>
        public void ConnectAgent(ChatAgent agent)
        {
            Agents.Add(agent);
        }
        /// <summary>
        /// Removes the agent passed in parameter from AgentsStorage
        /// </summary>
        /// <param name="agent">Instance of object to be removed</param>
        public void Remove(ChatAgent agent)
        {
            Agents.Remove(agent); //не реализован на уровне ниже
        }
    }
}
