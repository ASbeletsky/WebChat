using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using WebChat.DataAccess.Concrete.Entities.Identity;
using Microsoft.AspNet.Identity;
using WebChat.DataAccess.Abstract;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace WebChat.WebUI.WebApi.Chat
{
    public class ClientController : ApiController
    {
        private IDataService _unitOfWork;
        private IDataService UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = HttpContext.Current.GetOwinContext().Get<IDataService>();
                return _unitOfWork;
            }
        }

        [ActionName("info")]
        public HttpResponseMessage GetClientInfo(long Id)
        {
            AppUser client = UnitOfWork.Users.GetById(Id);
            if(client != null)
            {
                int dialogsCount = client.Dialogs.TakeWhile(d => d.StartedAt != d.ClosedAt).Count();
                
                bool hasDialogs = (dialogsCount > 0);
                DateTime lastChatDate = new DateTime();
                if (hasDialogs) 
                     lastChatDate =  client.Dialogs.Max(d => d.ClosedAt);

                var addressClaim = client.Claims.FirstOrDefault(c => c.ClaimType == "Location:Address");
                var SocialNetworkClaims = client.Claims.Where(c => c.ClaimType.StartsWith("SocialNetworkLink"));

                var clientInfo = new
                {
                    Email = client.Email,
                    Address = (addressClaim != null) ? addressClaim.ClaimValue : null,
                    SocialNetworkLinks = SocialNetworkClaims.Select(c => c.ClaimValue).ToArray(),
                    UserType = hasDialogs ? "Вернувшийся" : "Новый",
                    DialogsCount = dialogsCount,
                    LastChatDate = hasDialogs ? lastChatDate.ToShortDateString() : null
                };
                return Request.CreateResponse(HttpStatusCode.OK, clientInfo);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [ActionName("photo")]
        public string GetPhotoSource()
        {
            long userId = User.Identity.GetUserId<long>();
            var userClaims = UnitOfWork.Users.GetById(userId).Claims;
            string photoSrc = userClaims.FirstOrDefault(c => c.ClaimType == "PhotoUrl").ClaimValue;
            if (photoSrc == null)
                photoSrc = "~/Content/Images/default-user-image.png";
            return photoSrc;
        }

        public HttpResponseMessage SetLocation(JObject location)
        {
            long userId = User.Identity.GetUserId<long>();
            AppUser client = UnitOfWork.Users.GetById(userId);

            string lat = location.GetValue("latitude").Value<string>();
            string lng = location.GetValue("longitude").Value<string>();
            string address = GetAddressByGoogleApi(lat, lng);

            var claimsManager = UnitOfWork.Users.GetUserManager();

            var locationClaims = claimsManager.GetClaims(userId).Where(c => c.Type.Contains("Location"));
            foreach(var claim in locationClaims)
            {
                claimsManager.RemoveClaim(userId, claim);
            }

            claimsManager.AddClaim(userId, new Claim("Location:Latitude", lat, ClaimValueTypes.Double));
            claimsManager.AddClaim(userId, new Claim("Location:Longitude", lng, ClaimValueTypes.Double));
            claimsManager.AddClaim(userId, new Claim("Location:Address", address, ClaimValueTypes.String));

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private string GetAddressByGoogleApi(string latitude, string longitude)
        {
            string apiKey_param = "AIzaSyCMXNUk5s8kRZChT5XUTQjREInbLaMjiPk";
            string latitude_param = Uri.EscapeDataString(latitude);
            string longitude_param = Uri.EscapeDataString(longitude);
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
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var json = JsonConvert.DeserializeObject<dynamic>(sr.ReadToEnd());
            var reslut = ((JArray)json.results).Children();
            var address = reslut.Select(x => x.SelectToken("formatted_address")).First();
            return address.ToObject<string>();
        }
    }
}
