namespace WebChat.Infrastructure.CQRS.Commands.CustomerApp
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using Data.Interfaces;
    using Data.Interfaces.Repositories;
    using Data.Models.Application;
    using Interfaces;
    using Services.Interfaces;

    #endregion

    public class AddOrEditCustomerAppCommandHandler : BaseCommandHandler, ICommandHandler<AddOrEditCustomerAppCommand>
    {
        private IApplicationRepository apps;
        private IEntityConverter converter;
        public AddOrEditCustomerAppCommandHandler(IUnitOfWork uow, IEntityConverter converter) : base(uow, converter)
        {
            this.apps = uow.Applications;
        }
        public async Task<CommandResult> Execute(AddOrEditCustomerAppCommand command)
        {
            var result = new CommandResult();
            ApplicationModel applicationModel = null;
            bool isNewApp = command.AppId > 0;

            try
            {
                if (isNewApp)
                {
                    applicationModel = converter.Convert<AddOrEditCustomerAppCommand, ApplicationModel>(command);
                    apps.Create(applicationModel);
                    result.Success = true;
                    result.Message = "Ваше приложение успешно создано.";
                }
                else
                {
                    applicationModel = await apps.GetByIdAsync(command.AppId);
                    if (applicationModel != null)
                    {
                        converter.MapToExisting(command, ref applicationModel);
                        apps.Update(applicationModel);
                        result.Success = true;
                        result.Message = "Ваше приложение успешно обновлено.";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = string.Format("Приложение номер {0} не найдено", command.AppId);
                    }
                }

                UnitOfWork.Save();
                result.Data = applicationModel.Id;
            }
            catch(Exception error)
            {
                result.Success = false;
                result.Message = "Error. " + error.Message;               
            }

            return result;
        }
    }
}
