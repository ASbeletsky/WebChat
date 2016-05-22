namespace WebChat.Infrastructure.Data.Storage.Factories
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using WebChat.Domain.Core.Customer;
    using WebChat.Infrastructure.Data.Models.Application;
    using WebChat.Infrastructure.Services.Interfaces;

    #endregion

    public class ApplicationFactory : FactoryBase
    {
        #region Constructors

        public ApplicationFactory() : base()
        {
        }

        public ApplicationFactory(IEntityConverter converter) : base(converter)
        {
        }

        #endregion

        public virtual Application RestoreApplicationFromModel(ApplicationModel appModel)
        {
            return new Application(appModel.WebsiteUrl, appModel.SubjectScope, appModel.ContactEmail, appModel.OwnerId);
        }

        public virtual IEnumerable<Application> RestoreApplicationsFromModels(IEnumerable<ApplicationModel> appModels)
        {
            return appModels.Select(app => new Application(app.WebsiteUrl, app.SubjectScope, app.ContactEmail, app.OwnerId));
        }
    }
}
