using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.DataBase;
using WebChat.DataAccess.Concrete.DTO;
using WebChat.DataAccess.Concrete.Entities.Chat;

namespace WebChat.DataAccess.Concrete.Repositories
{
    public class DialogRepository : IRepository<Dialog>, IAmountOfDialogs
    {
        private readonly WebChatDbContext _context;
        private static string numberOfDialogsByRole;
        private string NumberOfDialogsByRole
        {
            get
            {
                if (numberOfDialogsByRole == null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("SELECT chatUser.Name AS AgentName, count(dialog.Id) AS AmountOfDialogs ");
                    builder.Append("FROM Role chatRole ");
                    builder.Append("JOIN UserRole userChatRole ");
                    builder.Append("ON userChatRole.RoleId = chatRole.Id ");
                    builder.Append("JOIN [User] chatUser ");
                    builder.Append("ON userChatRole.UserId = chatUser.Id ");
                    builder.Append("LEFT JOIN UserDialog userDialog ");
                    builder.Append("ON chatUser.Id = userDialog.User_Id ");
                    builder.Append("LEFT JOIN Dialog dialog ");
                    builder.Append("ON userDialog.Dialog_Id = dialog.Id ");
                    builder.Append("WHERE chatRole.Name = @p0 ");
                    builder.Append("GROUP BY chatUser.Name, chatRole.Name");
                    numberOfDialogsByRole = builder.ToString();
                }
                return numberOfDialogsByRole;
            }
        }

        public DialogRepository(WebChatDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Dialog> All
        {
            get { return _context.Dialog.AsQueryable(); }
        }

        public void Create(Dialog item)
        {
            var userIds = item.Users.Select(u => u.Id).ToArray();
            item.Users = null;
            _context.Dialog.Add(item);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            _context.Database.ExecuteSqlCommand("insert into UserDialog values ({0}, {1})", userIds[0],item.Id);
            if(userIds[0] != userIds[1])
                _context.Database.ExecuteSqlCommand("insert into UserDialog values ({0}, {1})", userIds[1], item.Id);          
        }

        public void Delete(dynamic Id)
        {
            int id = (int)Id;
            var dialogForDelete = _context.Dialog.Find(id);
            _context.Dialog.Remove(dialogForDelete);
            _context.SaveChanges();
        }

        public Dialog Find(Func<Dialog, bool> predicate)
        {
            return _context.Dialog.Where(predicate).AsQueryable().FirstOrDefault();
        }

        public Dialog GetById(dynamic Id)
        {
            int id = (int) Id;
            return _context.Dialog.Find(id);
        }

        public void Update(Dialog item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<AgentsDialogsDataDTO> getAgentsAmountOfDialogData(string role)
        {
            return _context.Database.SqlQuery<AgentsDialogsDataDTO>(NumberOfDialogsByRole, role);
        }
    }
}
