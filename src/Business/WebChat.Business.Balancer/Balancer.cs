//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebChat.BusinessLogic.Chat.Entities;
//using WebChat.DataAccess.Abstract;
//using WebChat.DataAccess.Concrete;
//using WebChat.DataAccess.Concrete.Entities.Identity;

//namespace WebChat.BusinessLogic.Chat.Balancers
//{
//    public abstract class Balancer
//    {
//        private static  Balancer _instance;
//        private static object locker = new object();

//        private IUnitOfWork _unitOfWork;
//        protected IUnitOfWork UnitOfWork
//        {
//            get
//            {
//                if (_unitOfWork == null)
//                    _unitOfWork = new EfUnitOfWork();
//                return _unitOfWork;
//            }
//        }
//        protected IEnumerable<ChatAgent> Agents { get; }
//        protected Balancer(IEnumerable<ChatAgent> agents)
//        {
//            Agents = agents;
//        }
//        public abstract ChatAgent GetFreeAgent();

//        public static Balancer GetInstance(BalancerType balanceBy, IEnumerable<ChatAgent> agents)
//        {
//            lock (locker)
//            {
//                if (_instance == null)
//                    switch(balanceBy)
//                    {
//                        case BalancerType.ChatIntensity: _instance = new BalancerByChatIntensity(agents); break;
//                        case BalancerType.WorkingTime: _instance = new BalancerByWorkingTime(agents); break;
//                    }
                       
//                return _instance;
//            }
//        }
//    }
//}
