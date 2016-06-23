
using System.Web.Mvc;

namespace WebChat.WebUI.Controllers.Chat
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {

    }
}