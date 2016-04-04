namespace WebChat.Services.Interfaces.Command
{
    #region Using

    using System.Threading.Tasks;

    #endregion
    public interface ICommandHandler<in TParameter> where TParameter : ICommand
    {
        Task<ICommandResult> Execute(TParameter command);
    }
}
