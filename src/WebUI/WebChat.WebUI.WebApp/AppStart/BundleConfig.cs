namespace WebChat.WebUI.WebApp.AppStart
{
    #region Using

    using System.Web.Optimization;

    #endregion

    public class BundleConfig
    {
        static BundleConfig()
        {
            BundleTable.EnableOptimizations = false;
        }

        public static string SharedStyles = "~/Content/css";
        public static string CustomerStyles = "~/Content/CustomerManagmentStyles";
        public static string CustomerAgentsStyles = "~/Content/CustomerAgentsStyles";     
        public static string ChartStyles = "~/Content/ChartStyles";
        public static string MapsScripts = "~/bundles/jvectormaps";
        public static string StatisticScripts = "~/bundles/user-statistic";
        public static string CustomerAppScripts = "~/bundles/CustomerAppManagmentScripts";
        public static string CustomerAgentsScripts = "~/bundles/CustomerAgentManagmentScripts";

        public static string ChatScriptsShared = "~/bundles/ChatScriptsShared";

        public static string ClientChatStyles = "~/Content/ClientChatStyles";        
        public static string ClientChatApp = "~/bundles/ClientChatApp";

        public static string AgentChatStyles = "~/Content/AgentChatStyles";
        public static string AgentChatApp = "~/bundles/AgentChatApp";

        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Shared 

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/Styles/Plugins/bootstrap/*.css")
                .Include("~/Content/Styles/Plugins/font-icons/font-awesome.css")
                .Include("~/Content/Styles/Shared/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/Plugins/jquery/*.js")
                .Include("~/Scripts/Plugins/notify.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Plugins/bootstrap/*.js",
                      "~/Scripts/Plugins/respond.js"));
            #endregion

            #region Customer

            bundles.Add(new StyleBundle("~/Content/CustomerManagmentStyles")
               .Include("~/Content/Styles/Customer/*.css")
               .Include("~/Content/Styles/Plugins/jquery-ui/jquery-jvectormap-2.0.3.css"));

            bundles.Add(new StyleBundle("~/Content/CustomerAgentsStyles")
               .Include("~/Content/Styles/Plugins/chosen/bootstrap-chosen.css")
               .Include("~/Content/Styles/Customer/Agents.css"));

            bundles.Add(new StyleBundle("~/Content/ChartStyles")
                .Include("~/Content/Styles/Plugins/charts/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/jvectormaps")
               .Include("~/Scripts/CustomerManagement/Maps/jquery-jvectormap-2.0.3.min.js")
               .Include("~/Scripts/CustomerManagement/Maps/jquery-jvectormap-world-mill.js")
               .Include("~/Scripts/CustomerManagement/Maps/jquery-jvectormap-ukraine.js")
               .Include("~/Scripts/CustomerManagement/Maps/vector-map.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-statistic")
                .Include("~/Scripts/Plugins/charts/*.js")
                .Include("~/Scripts/CustomerManagement/users-statistic.js"));

            bundles.Add(new ScriptBundle("~/bundles/CustomerAppManagmentScripts")
                               .Include("~/Scripts/CustomerManagement/application.js"));

            bundles.Add(new ScriptBundle("~/bundles/CustomerAgentManagmentScripts")
                .Include("~/Scripts/Plugins/chosen/chosen.jquery.js")
                .Include("~/Scripts/CustomerManagement/agent.js"));

            #endregion

            #region Chat Shared

            bundles.Add(new ScriptBundle("~/bundles/ChatScriptsShared")
                .Include("~/Scripts/Plugins/jquery/*.js")
                .Include("~/Scripts/Plugins/bootstrap/*.js")
                .Include("~/Scripts/Plugins/angular/angular.js", "~/Scripts/Plugins/angular/angular-route.js")
                .Include("~/Scripts/Plugins/signalr/*.js")
                .Include("~/signalr/hubs")
            );

            #endregion

            #region Chat Client

            bundles.Add(new StyleBundle("~/Content/ClientChatStyles")
               .Include("~/Content/Styles/Plugins/bootstrap/*.css")
               .Include("~/Content/Styles/Plugins/font-icons/font-awesome.css")
               .Include("~/Content/Styles/Chat/client/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/ClientChatApp")
               .Include("~/Scripts/Chat/client.js")
               .Include("~/app/ClientChat/app.js")
               .Include("~/app/ClientChat/Controllers/authController.js")
               .Include("~/app/ClientChat/Controllers/chatController.js")
               .Include("~/app/ClientChat/Controllers/waitAgentController.js")
               .Include("~/app/ClientChat/Controllers/endChatController.js")
               .Include("~/app/ClientChat/Services/authService.js")
               .Include("~/app/ClientChat/Services/dataService.js")
               .Include("~/app/ClientChat/Services/chatService.js")            
            );

            #endregion

            #region Chat Agent

            bundles.Add(new StyleBundle("~/Content/AgentChatStyles")
               .Include("~/Content/Styles/Chat/agent/*css"));

            bundles.Add(new ScriptBundle("~/bundles/AgentChatApp")
               .Include("~/Scripts/Chat/agent.js")
               .Include("~/app/AgentChat/app.js")
               .Include("~/app/AgentChat/Controllers/chatListController.js")
               .Include("~/app/AgentChat/Controllers/chatController.js")
               .Include("~/app/AgentChat/Controllers/clientController.js")
               .Include("~/app/AgentChat/Services/dataService.js")
               .Include("~/app/AgentChat/Services/chatService.js")
            );

            #endregion
        }
    }
}
