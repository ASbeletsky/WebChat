namespace WebChat.Infrastructure.CQRS.Commands.CustomerApp
{
    #region Using

    using WebChat.Infrastructure.CQRS.Interfaces;

    #endregion

    public class AddOrEditCustomerAppCommand : ICommand
    {
        public AddOrEditCustomerAppCommand(int appId)
        {
            AppId = appId;
        }
        public int AppId
        {
            get;
            private set;
        }
    }
}
