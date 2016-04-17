namespace WebChat.Infrastructure.CQRS.Commands
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using WebChat.Infrastructure.CQRS.Interfaces;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public class CommandDispatcher : ICommandDispatcher
    {
        IDependencyContainer container;

        public CommandDispatcher(IDependencyContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public async Task<CommandResult> Execute<TParameter>(TParameter command) where TParameter : ICommand
        {
            // Find the appropriate handler to call from those registered with Ninject based on the type parameters  
            var handler = container.GetService<ICommandHandler<TParameter>>();
            return await handler.Execute(command);
        }
    }
}
