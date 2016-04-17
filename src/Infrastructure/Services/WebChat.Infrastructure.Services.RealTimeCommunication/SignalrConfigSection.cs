namespace WebChat.Infrastructure.Services.RealTimeCommunication
{
    #region Using

    using System.Configuration;

    #endregion

    /// <summary>
    /// Configuration section for Signalr
    /// </summary>
    internal class SignalrConfigSection : ConfigurationSection
    {        
        private const string hubPathParamName = "hubPath";
        private const string hubConfigurationElementName = "hubConfiguration";

        internal const string SectionName = "signalr/signalrConfig";

        [ConfigurationProperty(name: hubPathParamName)]
        internal string HubPath
        {
            get
            {
                return this[hubPathParamName].ToString();
            }
            set
            {
                this[hubPathParamName] = value;
            }
        }

        [ConfigurationProperty(name: hubConfigurationElementName)]
        internal HubConfigurationElement HubConfiguration
        {
            get
            {
                return (HubConfigurationElement) this[hubConfigurationElementName];
            }
            set
            {
                this[hubConfigurationElementName] = value;
            }
        }

        internal class HubConfigurationElement : ConfigurationElement
        {
            private const string enableJSONPParamName = "enableJSONP";
            private const string enableDetailedErrorsParamName = "enableDetailedErrors";

            [ConfigurationProperty(name: enableJSONPParamName)]
            internal bool EnableJSONP
            {
                get
                {
                    return (bool)this[enableJSONPParamName];
                }
                set
                {
                    this[enableJSONPParamName] = value;
                }
            }

            [ConfigurationProperty(name: enableDetailedErrorsParamName)]
            internal bool EnableDetailedErrors
            {
                get
                {
                    return (bool)this[enableDetailedErrorsParamName];
                }
                set
                {
                    this[enableDetailedErrorsParamName] = value;
                }
            }
        }
    }
}
