namespace WebChat.WebUI.ViewModels.Customer
{
    #region Using

    using WebChat.WebUI.ViewModels.Shared;

    #endregion

    public class RegisterCustomerAndFirstAppViewModel
    {
        public RegisterCustomerAndFirstAppViewModel()
        {
            this.App = new ApplicationViewModel();
            this.Customer = new RegisterViewModel();
        }

        public ApplicationViewModel App
        {
            get;
            set;
        }

        public RegisterViewModel Customer
        {
            get;
            set;
        }
    }
}
