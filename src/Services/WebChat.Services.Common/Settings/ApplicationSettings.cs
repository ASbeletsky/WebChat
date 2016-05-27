namespace WebChat.Services.Common.Settings
{

    #region Using

    using System;
    using System.Configuration;
    using Interfaces;
    using Interfaces.Settings;

    #endregion

    public class ApplicationSettings : IApplicationSettings
    {
        private AuthServices authSettings;
        private Uri appUrl;

        public IAuthSettings AuthSettings
        {
            get
            {
                if (authSettings == null)
                {
                    authSettings = ConfigurationManager.GetSection("authorizationServices") as AuthServices;
                }

                return authSettings;
            }
        }

        public Uri AppUrl
        {
            get
            {
                if(appUrl == null)
                {
                    string appUrlString = ConfigurationManager.AppSettings["BaseUrlPath"].ToString();
                    Uri appUrl = new Uri(appUrlString);
                }

                return appUrl;
            }
        }
     }
}
