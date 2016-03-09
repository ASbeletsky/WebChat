using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebChat.DataAccess.Abstract;
using WebChat.DataAccess.Concrete.DataBase.Statistic_Entities;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using WebChat.WebUI.Models.Statistic;

namespace WebChat.WebUI.WebApi.Statistic
{
    public class StatisticController : ApiController
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
        [HttpPost]
        public HttpResponseMessage ChatDuration(AppDateTimeStatisticSelector parameters)
        {
            List<ChatDurationPerDay> chatDuration = UnitOfWork.CustomerApplications
                                                              .ChatDurationInPeriod(parameters.appId, parameters.startDate, parameters.endDate)
                                                              .ToList();

            if (chatDuration.Count == 0 || chatDuration == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var JsonFormatedData = new
            {
                Dates = chatDuration.Select(data => data.CurrentDate).ToArray(),
                ChatDurationValues = chatDuration.Select(data => data.ChatDuration).ToArray()
            };

            return Request.CreateResponse(HttpStatusCode.OK, JsonFormatedData);
        }
    }
}
