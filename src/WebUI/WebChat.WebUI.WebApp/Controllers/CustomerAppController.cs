namespace WebChat.WebUI.Controllers
{

    #region Using

    using System.Threading.Tasks;
    using System.Web.Mvc;
    using WebChat.WebUI.ViewModels.Customer;
    using WebChat.WebUI.WebApp.Controllers;
    using Infrastructure.CQRS.Commands.CustomerApp;
    using Infrastructure.CQRS.Queries.CustomerApp;

    #endregion

    public class CustomerAppController : BaseMvcController
    {
        //GET: /chat-script
        public ActionResult GetMainScript()
        {
            string mainStriptPath = "~/Scripts/Chat/main.js";
            return new FilePathResult(mainStriptPath, contentType: "application/javascript");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(EditCustomerAppQuery query)
        {
            var model = await QueryDispatcher.Execute<EditCustomerAppQuery, ApplicationViewModel>(query);
            if (model == null)
            {
                return Json("Приложение не найдено.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationViewModel model)
        {
            // First check if view model is valid
            if (ModelState.IsValid)
            {
                var editApplicationCommand = EntityConverter.Convert<ApplicationViewModel, AddOrEditCustomerAppCommand>(model);
                var editResult = await CommandDispatcher.Execute(editApplicationCommand);
                return Json(editResult);
            }

            // View model not valid, so return to view
            return View("Edit", model);
        }
    }
}
