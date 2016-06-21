namespace WebChat.BusinessLogic.Chat.Balancers
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebChat.Business.Chat.Entities;
    using WebChat.Data.Models.Chat;

    #endregion

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

        private TimeSpan getAgentTimeInDialogs(ChatAgent agent, IEnumerable<DialogModel> agentDialogs)
        {          
            TimeSpan timeInDialogs = new TimeSpan(0, 0, 0);
            foreach (var dialog in agentDialogs)
            {
                timeInDialogs += dialog.ClosedAt - dialog.StartedAt;
            }

            return timeInDialogs;
        }
        public override ChatAgent GetFreeAgent()
        {
            return FindFreeAgent(Agents, Stotage.Messages.All);
        }

        public ChatAgent FindFreeAgent(IEnumerable<ChatAgent> agents, IEnumerable<MessageModel> messages)
        {
            foreach (var agent in agents)
            {
                var dialogsIds = agent.Dialogs.Select(d => d.StoredDialogId);
                var agentDialogs = Stotage.Dialogs.All.Where(dialog => dialogsIds.Contains(dialog.Id) && dialog.IsClosed && dialog.ClosedAt.Date == DateTime.Today);

                var agentStrartWorkAt = agentDialogs.Any()? agentDialogs.First().StartedAt : DateTime.Now;
                var agentOnlineTime = DateTime.Now - agentStrartWorkAt;
                var agentTimeInDialogs = getAgentTimeInDialogs(agent, agentDialogs);
                var agentTimeWithoutDialog = agentOnlineTime - agentTimeInDialogs;

                if (!AgentsWorkingTime.ContainsKey(agent))
                    AgentsWorkingTime.Add(agent, agentTimeWithoutDialog);
                else
                    AgentsWorkingTime[agent] = agentTimeWithoutDialog;
            }

            var AgentWithMinimalWorkingTime = Enumerable.Max(AgentsWorkingTime).Key;

            return AgentWithMinimalWorkingTime;
        }
    }      
}
