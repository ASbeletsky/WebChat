using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebChat.DataAccess.Abstract;
using Microsoft.AspNet.Identity.Owin;

namespace WebChat.WebUI.Controllers
{
    public class CustomerAppAPIController : ApiController
    {
        private IUnitOfWork unitOfWork;


        public CustomerAppAPIController()
        {
            this.unitOfWork = HttpContext.Current.GetOwinContext().Get<IUnitOfWork>();
        }

        public string GetKey()
        {
            string customerAppUrl = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, String.Empty)).ToString();       
            var CurrentApp = unitOfWork.CustomerApplications.Find(app => app.WebsiteUrl == customerAppUrl);
            return CurrentApp.AppKey;
        }
    }
}
