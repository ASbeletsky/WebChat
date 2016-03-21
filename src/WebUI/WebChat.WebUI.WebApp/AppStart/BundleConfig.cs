namespace WebChat.WebUI.WebApp.AppStart
{
    #region Using

    using System.Web.Optimization;

    #endregion
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/*.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
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
