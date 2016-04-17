namespace WebChat.Infrastructure.CQRS.Interfaces
{
    #region Using

    using System.Threading.Tasks;

    #endregion
    public interface ICommandDispatcher
    {
        Task<CommandResult> Execute<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
