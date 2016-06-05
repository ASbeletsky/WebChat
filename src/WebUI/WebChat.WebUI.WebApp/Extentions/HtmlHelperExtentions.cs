namespace WebChat.WebUI.WebApp.Extentions
{
    using Controllers;
    #region Using

    using System;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    #endregion

    public static class HtmlHelperExtentions
    {
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new System.Web.Mvc.TemplateInfo { HtmlFieldPrefix = name }
            };

            return helper.Partial(partialViewName, model, viewData);
        }

        public static string PartialViewToString(this HtmlHelper helper, string viewName, object model)
        {
            var httpContext = HttpContext.Current;
            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            var controller = (MvcBaseController)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);
            }

            return controller.RenderPartialToString(viewName, model);
        }
    }
}
