namespace WebChat.Infrastructure.CQRS.Interfaces
{
    #region Using

    using System.Threading.Tasks;

    #endregion
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        Task<CommandResult> Execute(TParameter command);
    }
}
