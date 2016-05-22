namespace WebChat.WebUI.WebApp.AppStart
{
    #region Using

    using System.Web.Optimization;

    #endregion

    public class BundleConfig
    {     
        static BundleConfig ()
        {
            BundleTable.EnableOptimizations = true;
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/*.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Styles/bootstrap/*.css",
                      "~/Content/Styles/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/CRUDhandler").Include(
                      "~/Scripts/OwnerBackend/*.js"
                      ));

        }
    }
}
