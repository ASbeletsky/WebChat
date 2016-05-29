namespace WebChat.Services.Common.Settings
{
    #region Using

    using System.Configuration;
    using Interfaces.Settings;

    #endregion

    public class AuthServices : ConfigurationSection, IAuthSettings
    {
        [ConfigurationProperty("FacebookService", IsRequired = true)]
        public FacebookServiceElement FacebookService
        {
            get
            {
                return (FacebookServiceElement) this["FacebookService"];
            }
            set
            {
                this["FacebookService"] = value;
            }
        }

        [ConfigurationProperty("TwitterService", IsRequired = true)]
        public TwitterServiceElement TwitterService
        {
            get
            {
                return (TwitterServiceElement) this["TwitterService"];
            }
            set
            {
                this["TwitterService"] = value;
            }
        }

        IFacebookServiceSettings IAuthSettings.FacebookSettings
        {
            get
            {
                return this.FacebookService;
            }
        }

        ITwitterServiceSettings IAuthSettings.TwitterSettings
        {
            get
            {
                return this.TwitterService;
            }
        }
    }
}
