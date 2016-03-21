namespace WebChat.RealTimeWebService
{
    #region Using

    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    #endregion

    /// <summary>
    /// Represents RealtimeService
    /// </summary>
    public class SignalrUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (request.User != null & request.User.Identity != null)
            {
                return request.User.Identity.GetUserId<long>().ToString();
            }

            return null;
        }
    }
}
