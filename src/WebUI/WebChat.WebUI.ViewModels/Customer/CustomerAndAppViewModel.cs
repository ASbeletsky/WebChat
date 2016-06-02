namespace WebChat.WebUI.ViewModels.Customer
{
    #region Using

    using Application;
    using Shared;

    #endregion

    public class CustomerAndAppViewModel
    {
        public RegisterViewModel Customer { get; set; }
        public ApplicationFieldsViewModel App { get; set; }
    }
}
