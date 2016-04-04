namespace WebChat.Services.Interfaces.Command
{
    #region Using

    using System.Threading.Tasks;

    #endregion
    public interface ICommandDispatcher
    {
        Task<ICommandResult> Dispatch<TParameter>(TParameter command) where TParameter : ICommand;
    }
}
