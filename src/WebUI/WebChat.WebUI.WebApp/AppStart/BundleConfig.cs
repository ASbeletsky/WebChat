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

        public static string CustomerStyles = "~/Content/CustomerManagmentStyles";
        public static string CustomerAppScripts = "~/bundles/CustomerAppManagmentScripts";
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Shared 

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/Styles/bootstrap/*.css")
                .Include("~/Content/Styles/site.css")
                .Include("~/Content/Styles/font-icons/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery/*.js")
                .Include("~/Scripts/notify.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/*.js",
                      "~/Scripts/respond.js"));
            #endregion

            #region Customer

            bundles.Add(new StyleBundle("~/Content/CustomerManagmentStyles")
                   .Include("~/Content/Styles/Customer/*.css"));
                   

            bundles.Add(new ScriptBundle("~/bundles/CustomerManagmentScripts")
                   .Include("~/Scripts/CustomerManagement/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/CustomerAppManagmentScripts")                  
                   .Include("~/Scripts/jquery/map/jquery-jvectormap-2.0.3.min.js")
                   .Include("~/Scripts/jquery/map/jquery-jvectormap-world-mill.js")
                   .Include("~/Scripts/CustomerManagement/Application.js"));

            #endregion
        }
    }
}
