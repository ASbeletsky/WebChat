using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebChat.DataAccess.Abstract;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http.Results;


namespace WebChat.WebUI.Controllers.WebApi
{
    public class CustomerAppController : ApiController
    {
        private IDataService unitOfWork;
        public CustomerAppController()
        {
            this.unitOfWork = HttpContext.Current.GetOwinContext().Get<IDataService>();
        }
        [HttpGet]
        public string Key(int id)
        {            
            var CurrentApp = unitOfWork.CustomerApplications.Find(app => app.Id == id);
            return CurrentApp.AppKey;
        }
       
    }
}
