namespace WebChat.Services.Interfaces
{
    #region Using

    using System;
    using WebChat.Services.Interfaces.Settings;

    #endregion

    public interface IApplicationSettings
    {
        IAuthSettings AuthSettings
        {
            get;
        }

        Uri AppUrl
        {
            get;
        }
    }
}
