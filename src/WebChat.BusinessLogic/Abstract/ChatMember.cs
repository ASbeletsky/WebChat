using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChat.BusinessLogic.Chat;
using WebChat.DataAccess.Concrete.Entities.Identity;

namespace WebChat.BusinessLogic.Abstract
{
    public abstract class ChatMember
    {
        public string HubConnectionId { get; set; }
        public string HubUserId
        {
            get
            {
                if (AsUser == null)
                    return null;
                return AsUser.Id.ToString();
            }
        }
        public AppUser AsUser { get; set; }

        public string GetPhotoSource()
        {
            var userClaims = this.AsUser.Claims;
            var photoSrcCalaim = userClaims.FirstOrDefault(c => c.ClaimType == "PhotoUrl");
            if (photoSrcCalaim == null)
                return "../../../Content/Images/default-user-image.png";
            else
                return photoSrcCalaim.ClaimValue;
        }
    }
}
