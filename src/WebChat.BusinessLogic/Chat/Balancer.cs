using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat.Entities;

namespace WebChat.BusinessLogic.Chat
{
    class Balancer
    {
        /// <summary>
        /// Getting free agent by the time of last message
        /// </summary>
        /// <param name="agents">List of agents where method choose the most free</param>
        /// <returns>The most free agent order by last message time</returns>
        public ChatAgent GetFreeAgentByLastMessage(IEnumerable<ChatAgent> agents)
        {
            List<ChatAgent> sortedAgents = agents.OrderBy(o => o.LastMessageTime).ToList();

            return sortedAgents.Last();
        }

        /// <summary>
        /// Getting free agent by worktime
        /// </summary>
        /// <param name="agents">List of agents where method choose the most free</param>
        /// <returns>The most free agent order by full work time (minutes)</returns>
        public ChatAgent GetFreeAgentByWorkTime(IEnumerable<ChatAgent> agents)
        {
            foreach (var agent in agents)
            {
                TimeSpan? dialogWorkTime = null;

                //Dialog.AsStoredDialog AsStoredDialog - был удален из ChatAgent так как каждый ChatAgent
                //может отслежиться разными WebChatDbContext и запись/чтение из бд невозможны
                //поэтому вытаскивать все нужно через 1 обьект UnitOfWork

                //foreach (var Dialog in agent.Dialogs)
                //{
                //    dialogWorkTime += Dialog.AsStoredDialog.Value.StartedAt - Dialog.AsStoredDialog.Value.Messages.Last().SendedAt; //выбрать последнее сообщение
                //}
                agent.DialogWorkTime = dialogWorkTime;
            }

            TimeSpan? MinTime = agents.Select(agent => agent.DialogWorkTime).Min();
            return agents.FirstOrDefault(agent => agent.DialogWorkTime == MinTime);
        }
    }
}
