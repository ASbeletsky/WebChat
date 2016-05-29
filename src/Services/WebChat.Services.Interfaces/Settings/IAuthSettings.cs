

namespace WebChat.Services.Interfaces.Settings
{
    public interface IAuthSettings
    {
        IFacebookServiceSettings FacebookSettings
        {
            get;
        }

        ITwitterServiceSettings TwitterSettings
        {
            get;
        }
    }
}
