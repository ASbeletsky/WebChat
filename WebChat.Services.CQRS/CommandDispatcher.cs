using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.Services.Interfaces;
using WebChat.Services.Interfaces.Command;

namespace WebChat.Services.CQRS
{
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

        public async Task<ICommandResult> Dispatch<TParameter>(TParameter command) where TParameter : ICommand
        {
            // Find the appropriate handler to call from those registered with Ninject based on the type parameters  
            var handler = container.GetService<ICommandHandler<TParameter>>();
            return await handler.Execute(command);
        }
    }
}
