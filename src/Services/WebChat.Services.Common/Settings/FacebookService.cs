namespace WebChat.Services.Common.Settings
{
    
    #region Using

    using System.Configuration;
    using Interfaces.Settings;

    #endregion

    public class FacebookServiceElement : ConfigurationElement, IFacebookServiceSettings
    {
        [ConfigurationProperty("appId", IsRequired = true)]
        public string AppId
        {
            get
            {
                return (string)this["appId"];
            }
            set
            {
                this["appId"] = value;
            }
        }

        [ConfigurationProperty("appSecret", IsRequired = true)]
        public string AppSecret
        {
            get
            {
                return (string)this["appSecret"];
            }
            set
            {
                this["appSecret"] = value;
            }
        }
    }
}
