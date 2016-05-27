namespace WebChat.Services.Common
{ 
    #region Using

    using System.Configuration;
    using Interfaces.Settings;

    #endregion

    public class AuthServices : ConfigurationSection, IAuthSettings
    {
        [ConfigurationProperty("FacebookService", IsRequired = true)]
        public IFacebookServiceSettings FacebookSettings
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
        public ITwitterServiceSettings TwitterSettings
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
    }
}
