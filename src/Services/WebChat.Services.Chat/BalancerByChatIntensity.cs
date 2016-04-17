//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Chat.Entities;
//using WebChat.DataAccess.Abstract;
//using WebChat.DataAccess.Concrete.Entities.Chat;
//using WebChat.DataAccess.Concrete.Entities.Identity;

//namespace WebChat.Services.Chat
//{
//    /// <summary>
//    /// Получает агента который отправлял меньше всего сообщений в минуту
//    /// </summary>
//    public class BalancerByChatIntensity : Balancer
//    {
//        private Dictionary<ChatAgent, double> AgentsIntensity;
//        public BalancerByChatIntensity(IEnumerable<ChatAgent> agents)
//            : base(agents)
//        {
//            AgentsIntensity = new Dictionary<ChatAgent, double>();
//        }

//        public override ChatAgent GetFreeAgent()
//        {
//            foreach (var agent in Agents)
//            {
//                var intensity = getDialogsIntensity(agent);
//                AgentsIntensity.Add(agent, intensity);
//            }

//            var AgentWithMinimalChatIntensity = AgentsIntensity.Aggregate((agent1, agent2) =>
//            {
//                if (agent1.Value < agent2.Value)
//                    return agent1;
//                else
//                    return agent2;
//            }).Key;

//            return AgentWithMinimalChatIntensity;
//        }

//        /// <summary>
//        /// Возвращает среднее количество сообщений в минуту для всех диалогов оператора
//        /// </summary>
//        private double getDialogsIntensity(ChatAgent agent)
//        {
//            int dialogsCount = agent.Dialogs.Count();
//            int[] agentStoredDialogsIds = agent.Dialogs.Select(d => d.StoredDialogId).ToArray();

//            double counter = 0;
//            foreach (var dialogId in agentStoredDialogsIds)
//            {
//                counter =+ UnitOfWork.Messages.AverageMessageCountInMinuteForDialog(agent.AsUser.Id, dialogId);
//            }

//            return counter / dialogsCount;
//        }              
//    }
//}
