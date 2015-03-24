using System.Web;
using System.Web.Optimization;

namespace ImdbLite.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //TODO Turn true in Production
            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/Chosen/chosen.css",
                      "~/Scripts/fancybox/jquery.fancybox.css",
                      "~/Scripts/fancybox/jquery.fancybox-buttons.css",
                      "~/Scripts/fancybox/jquery.fancybox-thumbs.css",
                      "~/Scripts/owl-carousel/owl.carousel.css",
                      "~/Scripts/owl-carousel/owl.theme.css",
                      "~/Scripts/five-star-rating/star-rating.min.css",
                      "~/Content/bootstrap.darkly.css",
                      "~/Content/datepicker3.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/Site.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/movies-delete").Include(
            //"~/Scripts/custom/movies-delete.js"));

            //bundles.Add(new ScriptBundle("~/bundles/genres-delete").Include(
            //"~/Scripts/custom/genres-delete.js"));

            //bundles.Add(new ScriptBundle("~/bundles/cinemas-delete").Include(
            //"~/Scripts/custom/cinemas-delete.js"));

            //bundles.Add(new ScriptBundle("~/bundles/photos-delete").Include(
            //"~/Scripts/custom/photos-delete.js"));

            //bundles.Add(new ScriptBundle("~/bundles/celebrities-delete").Include(
            //"~/Scripts/custom/celebrities-delete.js"));

            //bundles.Add(new ScriptBundle("~/bundles/crudCelebrities").Include(
            //"~/Scripts/Modal/crudCelebrities.js"));

            bundles.Add(new ScriptBundle("~/bundles/submit-rate-form").Include(
                        "~/Scripts/custom/submit-rate-form.js"));

            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
                        "~/Scripts/five-star-rating/star-rating.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/owl-carousel").Include(
                        "~/Scripts/owl-carousel/owl.carousel.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                      "~/Scripts/fancybox/jquery.fancybox.pack.js",
                      "~/Scripts/fancybox/helpers/jquery.fancybox-buttons.js",
                      "~/Scripts/fancybox/helpers/jquery.fancybox-media.js",
                      "~/Scripts/fancybox/helpers/jquery.fancybox-thumbs.js"));

            bundles.Add(new ScriptBundle("~/bundles/first-child-collapsed-in").Include(
            "~/Scripts/custom/first-child-collapsed-in.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery.validate.unobtrusive").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/activate-fancybox-single-image").Include(
            "~/Scripts/custom/activate-fancybox-single-image.js"));

            bundles.Add(new ScriptBundle("~/bundles/scroll-to-top").Include(
            "~/Scripts/custom/scroll-to-top.js"));

            bundles.Add(new ScriptBundle("~/bundles/page-go-top").Include(
            "~/Scripts/custom/page-go-top.js"));

            bundles.Add(new ScriptBundle("~/bundles/cinema-quick-view").Include(
            "~/Scripts/custom/cinema-quick-view.js"));

            bundles.Add(new ScriptBundle("~/bundles/activate-chosen").Include(
            "~/Scripts/custom/activate-chosen.js"));

            bundles.Add(new ScriptBundle("~/bundles/activate-fancybox-gallery").Include(
            "~/Scripts/custom/activate-fancybox-gallery.js"));

            bundles.Add(new ScriptBundle("~/bundles/activate-fancybox-trailer").Include(
            "~/Scripts/custom/activate-fancybox-trailer.js"));

            bundles.Add(new ScriptBundle("~/bundles/picture-preview").Include(
            "~/Scripts/custom/picture-preview.js"));

            bundles.Add(new ScriptBundle("~/bundles/movies-create-server-side").Include(
            "~/Scripts/custom/movies-create-server-side.js"));

            bundles.Add(new ScriptBundle("~/bundles/item-delete").Include(
            "~/Scripts/custom/item-delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                        "~/Scripts/Modal/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Chosen").Include(
                        "~/Scripts/chosen/chosen.jquery.js"));
        }
    }
}
