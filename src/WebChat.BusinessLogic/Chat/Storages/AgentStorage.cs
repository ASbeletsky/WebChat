using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Abstract;
using WebChat.BusinessLogic.Chat.Entities;

namespace WebChat.BusinessLogic.Chat.Storages
{
    public class AgentsStorage : IChatItemRepository<ChatAgent>
    {
        private static AgentsStorage instance = null;
        private static readonly object singleton_lock = new object();
        private static readonly object add_agent_lock = new object();
        public static AgentsStorage Instance
        {
            get
            {
                lock (singleton_lock)
                {
                    if (instance == null)
                        instance = new AgentsStorage();
                    return instance;
                }
            }
        }

        private int _agentsCounter = 0;
        public BlockingCollection<ChatAgent> _agents { get; set; }

        public AgentsStorage()
        {
            _agents = new BlockingCollection<ChatAgent>();
        }

        /// <summary>
        /// Returns all agents from collection
        /// </summary>
        public IEnumerable<ChatAgent> All
        {
            get
            {
                return _agents.AsEnumerable();
            }
        }
        /// <summary>
        /// Returns agent with id passed in param
        /// </summary>
        /// <param name="Id">Id of agent to be returned</param>
        /// <returns>Agent by id</returns>
        /// <exception>NullReferenceException</exception>
        public ChatAgent GetById(int Id)
        {
            return _agents.FirstOrDefault(d => d.Id == Id);
        }
        /// <summary>
        /// Returns first agent found by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Agent found by predicate</returns>
        /// <exception>NullReferenceException</exception>
        public ChatAgent Find(Func<ChatAgent, bool> predicate)
        {
           return _agents.FirstOrDefault(predicate);
        }

        public List<ChatAgent> FindMany(Func<ChatAgent, bool> predicate)
        {
            return _agents.Where(predicate).ToList();
        }
        /// <summary>
        /// Adds agent passed in variable to storage
        /// </summary>
        /// <param name="item">Instance of agent to be added</param>
        /// <returns>True if added, false if not</returns>
        public bool Add(ChatAgent item)
        {
            lock(add_agent_lock)
            {
                if (this.GetById(item.Id) == null)
                {
                    item.Id = _agentsCounter++;
                    return _agents.TryAdd(item, 2000);
                }
                return false;
            }
        }
        /// <summary>
        /// Removes the agent passed in variable
        /// </summary>
        /// <param name="item">Instance of variable</param>
        /// <returns>True if added, false if not</returns>
        public bool Remove(ChatAgent item)
        {
            return false;
        }
    }
}
