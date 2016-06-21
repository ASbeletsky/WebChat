namespace WebChat.WebUI.WebApp.Extentions
{
    #region Using

    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Services.Interfaces;

    #endregion

    public class JsonpResult : ActionResult
    {
        public string CallbackFunction { get; set; }
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        

        public JsonpResult(object data) : this(data, null) { }
        public JsonpResult(object data, string callbackFunction)
        {
            Data = data;
            CallbackFunction = callbackFunction;
        }

        [Ninject.Inject]
        public IEntityConverter Converter
        {
            get { return DependencyContainer.Current.GetService<IEntityConverter>(); }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/x-javascript" : ContentType;

            if (ContentEncoding != null) response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                HttpRequestBase request = context.HttpContext.Request;

                var callback = CallbackFunction ?? request.Params["callback"] ?? "callback";

                response.Write(string.Format("{0}({1});", callback, Converter.ConvertToJson(Data)));

            }
        }
    }      
}
