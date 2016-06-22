namespace WebChat.Business.DomainModels
{
    using Data.Storage.Managers;
    using Microsoft.AspNet.Identity;
    using Newtonsoft.Json.Linq;
    using Services.Interfaces;
    using System;
    using System.IO;
    #region Using

    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using WebChat.Data.Storage.Identity;
    using WebChat.WebUI.ViewModels.Client;

    #endregion
    public class ClientDomainModel : BaseDomainModel
    {
        public ClientInfo GetClientInfo(long userId)
        {
            UserModel client = Storage.Users.GetById(userId);
                var addressClaim = client.Claims.FirstOrDefault(c => c.ClaimType == "Location:Address");
                var SocialNetworkClaims = client.Claims.Where(c => c.ClaimType.StartsWith("SocialNetworkLink"));

                var clientInfo = new ClientInfo
                {
                    Email = client.Email,
                    Address = (addressClaim != null) ? addressClaim.ClaimValue : null,
                    SocialNetworkLinks = SocialNetworkClaims.Select(c => c.ClaimValue).ToArray(),
                    UserType = "Новый"
                };

                bool hasDialogs = client.Dialogs.Any();
                if (hasDialogs)
                {
                    var closedDialogsQuery = client.Dialogs.Where(d => d.IsClosed).AsQueryable();
                    clientInfo.DialogsCount = closedDialogsQuery.Count();
                    clientInfo.LastDialogDate = closedDialogsQuery.Max(d => d.ClosedAt);
                    clientInfo.UserType = "Вернувшийся";
                }

            return clientInfo;
         }

        public string GetPhotoSrc(long userId)
        {
            var userClaims = Storage.Users.GetById(userId).Claims;
            string photoSrc = userClaims.FirstOrDefault(c => c.ClaimType == "PhotoUrl").ClaimValue;
            if (photoSrc == null)
                photoSrc = "~/Content/Images/default-user-image.png";
            return photoSrc;
        }

        public void SetLocation(long userId, Location location)
        {
            UserModel client = Storage.Users.GetById(userId);
            var userManager = DependencyContainer.Current.GetService<AppUserManager>();

            string address = GetAddressByGoogleApi(location);           

            var locationClaims = userManager.GetClaims(userId).Where(c => c.Type.Contains("Location"));
            foreach (var claim in locationClaims)
            {
                userManager.RemoveClaim(userId, claim);
            }

            userManager.AddClaim(userId, new Claim("Location:Latitude", location.Longitude.ToString(), ClaimValueTypes.Double));
            userManager.AddClaim(userId, new Claim("Location:Longitude", location.Latitude.ToString(), ClaimValueTypes.Double));
            userManager.AddClaim(userId, new Claim("Location:Address", address, ClaimValueTypes.String));
        }

        private string GetAddressByGoogleApi(Location location)
        {
            string apiKey_param = "AIzaSyCMXNUk5s8kRZChT5XUTQjREInbLaMjiPk";
            string latitude_param = Uri.EscapeDataString(location.Latitude.ToString("F5"));
            string longitude_param = Uri.EscapeDataString(location.Longitude.ToString("F5"));
            string language_param = "ru";

            var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?language={0}&latlng={1},{2}&location_type=ROOFTOP&result_type=street_address&key={3}",
                                                 language_param,
                                                 latitude_param,
                                                 longitude_param,
                                                 apiKey_param
                                            );

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var data = response.GetResponseStream();
            StreamReader sr = new StreamReader(data);
            var json = Converter.ConvertFromJson<dynamic>(sr.ReadToEnd());
            var reslut = ((JArray)json.results).Children();
            var address = reslut.Select(x => x.SelectToken("formatted_address")).First();
            return address.ToObject<string>();
        }
    }
}
