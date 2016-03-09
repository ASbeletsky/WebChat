using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.DTO;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.DataAccess.Concrete.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly WebChatDbContext _context;
        public MessageRepository(WebChatDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Message> All
        {
            get { return _context.Message.AsQueryable(); }
        }

        public void Create(Message item)
        {
            _context.Message.Add(item);
            _context.SaveChangesAsync();
        }

        public void Delete(dynamic Id)
        {
            long id = (long) Id;
            var messageForDelete = _context.Message.Find(id);
            _context.Message.Remove(messageForDelete);
            _context.SaveChanges();
        }

        public Message Find(Func<Message, bool> predicate)
        {
            return _context.Message.Where(predicate).AsQueryable().FirstOrDefault();
        }

        public Message GetById(dynamic Id)
        {
            long id = (long)Id;
            return _context.Message.Find(id);
        }

        public void Update(Message item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<StoredMessageDTO> GetAllMessagesInDialog(int dialogId)
        {
            return _context.Database.SqlQuery<StoredMessageDTO>("SELECT m.Dialog_id as DialogId, u.Name as UserName, m.Text as Text, m.SendedAt as Time FROM Message m JOIN Dialog d ON m.Dialog_id = d.Id JOIN[User] u ON m.Sender_id = u.Id WHERE d.Id = @p0 ORDER BY m.SendedAt",
                                                                dialogId).ToList();
        }

        public double AverageMessageCountInMinuteForDialog(long userId, int dialogId)
        {
            var result = _context.Database.SqlQuery<double>("Select dbo.AverageMessageCountInMinuteForDialog(@p0,@p1)",
                                                            userId,
                                                            dialogId
                                                        ).First();
            return result;
        }
    }
}
