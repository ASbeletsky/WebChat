namespace WebChat.BusinessLogic.Chat.Balancers
{
    #region Using

    using System.Collections.Generic;
    using WebChat.Business.Chat.Entities;
    using WebChat.Data.Interfaces;
    using WebChat.Services.Interfaces;

    #endregion

    public abstract class Balancer
    {
        private static Balancer _instance;
        private static object locker = new object();

        public Balancer()
        {
            this.Stotage = DependencyContainer.Current.GetService<IDataStorage>();
        }
        protected IDataStorage Stotage
        {
            get;
            private set;
        }
        protected IEnumerable<ChatAgent> Agents { get; }
        protected Balancer(IEnumerable<ChatAgent> agents)
        {
            Agents = agents;
        }
        public abstract ChatAgent GetFreeAgent();

        public static Balancer GetInstance(BalancerType balanceBy, IEnumerable<ChatAgent> agents)
        {
            lock (locker)
            {
                if (_instance == null)
                    switch (balanceBy)
                    {
                        case BalancerType.ChatIntensity: _instance = new BalancerByChatIntensity(agents); break;
                        case BalancerType.WorkingTime: _instance = new BalancerByWorkingTime(agents); break;
                    }

                return _instance;
            }
        }
    }
}
