using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using WebChat.DataAccess.Abstract;
using Microsoft.AspNet.Identity.Owin;
using WebChat.WebUI.Models;
using WebChat.DataAccess.Concrete.Entities.Identity;
using WebChat.DataAccess.Managers;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using WebChat.BusinessLogic;
using WebChat.BusinessLogic.Managers;

namespace WebChat.WebUI.WebApi.Chat
{
    public class SupportAgentController : ApiController
    {

        private IDataService _unitOfWork;
        private AppRoleManager _roleManager;
        private IDataService UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = HttpContext.Current.GetOwinContext().Get<IDataService>();
                return _unitOfWork;
            }
        }

        public AppRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                    return _roleManager = HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
                return _roleManager;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAgentInfo(int id)
        {
            AppUser agent = UnitOfWork.Users.GetById(id);
            if(agent != null)               
                if (RoleManager.IsUserInRole(agent, "SupportAgent"))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        Name = agent.UserName,
                        Rank = "Support Agent"
                    });
                }
                else
                {
                    HttpError error = new HttpError("User with this id is not support agent");
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
                }
            string errorMessage = string.Format("Agent with id {0} not found", id);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError(errorMessage));
        }

    }
}
