namespace WebChat.WebUI.ViewModels.Customer
{
    #region Using

    using WebChat.WebUI.ViewModels.Shared;

    #endregion

    public class CustomerAndAppViewModel
    {
        public RegisterViewModel Customer { get; set; }
        public RegisterApplicationViewModel App { get; set; }
    }
}
