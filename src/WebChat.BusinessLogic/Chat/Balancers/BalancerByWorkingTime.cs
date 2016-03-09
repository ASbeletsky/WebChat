using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat.Entities;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.BusinessLogic.Chat.Balancers
{
    /// <summary>
    /// Получает агента который меньше всего превел время в чате
    /// </summary>
    public class BalancerByWorkingTime : Balancer
    {
        private Dictionary<ChatAgent, TimeSpan> AgentsWorkingTime;
        
        public BalancerByWorkingTime(IEnumerable<ChatAgent> agents)
            : base(agents)
        {
            AgentsWorkingTime = new Dictionary<ChatAgent, TimeSpan>();
        }
        
        private IEnumerable<Message> getAgentMessagesToday(ChatAgent agent)
        {
            return UnitOfWork.Messages.All.Where
             (
                m => m.Sender_id == agent.AsUser.Id &&
                m.SendedAt.Date == DateTime.Today
             );
        }

        private DateTime getStartWorkTime(ChatAgent agent)
        {
            DateTime firstMessageTime = new DateTime();
            var messages = getAgentMessagesToday(agent); 
            if(messages.Count() > 0)
                firstMessageTime = messages.Min(m => m.SendedAt);            
            return firstMessageTime;
        }

        private DateTime getLastMessageTime(ChatAgent agent)
        {
            DateTime lastMessageTime = new DateTime();
            var messages = getAgentMessagesToday(agent);
            if (messages.Count() > 0)
                lastMessageTime = messages.Max(m => m.SendedAt);
            else
                lastMessageTime = getStartWorkTime(agent);
            return lastMessageTime;
        }

        private TimeSpan getTotalTime(ChatAgent agent)
        {
           return getLastMessageTime(agent) - getStartWorkTime(agent);
        }

        private TimeSpan getBusyTime(ChatAgent agent)
        {
            TimeSpan BusyTimeCounter = new TimeSpan(0, 0, 0);
            foreach (var dialogId in agent.Dialogs.Select(d => d.StoredDialogId))
            {
                var dialog = UnitOfWork.Dialogs.GetById(dialogId);
                BusyTimeCounter += dialog.ClosedAt - dialog.StartedAt;                                       
            }
            return BusyTimeCounter;
        }
        public override ChatAgent GetFreeAgent()
        {
            foreach (var agent in Agents)
            {
                var agentWorkTime = getTotalTime(agent) - getBusyTime(agent);
                if (!AgentsWorkingTime.ContainsKey(agent))
                    AgentsWorkingTime.Add(agent, agentWorkTime);
                else
                    AgentsWorkingTime[agent] = agentWorkTime;
            }

            var AgentWithMinimalWorkingTime = AgentsWorkingTime.Aggregate((agent1, agent2) =>
            {
                if (agent1.Value < agent2.Value)
                    return agent1;
                else
                    return agent2;
            }).Key;

            return AgentWithMinimalWorkingTime;
        }
    }
}
