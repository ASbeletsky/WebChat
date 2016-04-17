namespace WebChat.Infrastructure.Services.Common
{
    #region Using

    using System.Configuration;

    #endregion

    public class AuthServices : ConfigurationSection
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
    }
}
