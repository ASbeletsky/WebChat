namespace WebChat.Services.Common
{
    #region Using

    using System.Configuration;

    #endregion

    public class TwitterServiceElement : ConfigurationElement
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
