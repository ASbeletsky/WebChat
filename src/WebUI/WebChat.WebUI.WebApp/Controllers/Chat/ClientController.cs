namespace WebChat.WebUI.Controllers.Chat
{
    #region Using

    using Microsoft.Owin.Security;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using WebChat.WebUI.WebApp.Controllers;
    using WebChat.WebUI.WebApp.Extentions;
    using System.Web.Mvc;
    using WebChat.WebUI.ViewModels.Сhat;
    using WebChat.Data.Storage.Identity;
    using WebChat.Data.Models.Identity;
    using Data.Interfaces;
    using Business.DomainModels;
    using Microsoft.AspNet.Identity;
    using ViewModels.Client;
    #endregion

    public class ClientController : MvcBaseController
    {
        private ClientDomainModel clientDomainModel;

        public ClientController()
        {
            this.clientDomainModel = DependencyResolver.Current.GetService<ClientDomainModel>();
        }
        private IDataStorage Storage
        {
            get { return DependencyResolver.Current.GetService<IDataStorage>(); }
        }

        public JsonpResult CompactView()
        {
            return new JsonpResult(RenderPartialToString("~/Views/Chat/Compact.cshtml", model: null));
        }

        [ActionName("info")]
        public JsonResult GetClientInfo(long Id)
        {
            var info = clientDomainModel.GetClientInfo(Id);
            return Json(info);
        }

        [ActionName("photo")]
        public string GetPhotoSource()
        {
            long userId = User.Identity.GetUserId<long>();
            return clientDomainModel.GetPhotoSrc(userId);
        }

        public JsonResult SetLocation(Location location)
        {
            long userId = User.Identity.GetUserId<long>();
            clientDomainModel.SetLocation(userId, location);
            return Json(true);
        }

        
    }
}