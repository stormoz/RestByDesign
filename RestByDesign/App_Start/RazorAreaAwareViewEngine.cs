using System.Web.Mvc;

namespace RestByDesign
{
    public class RazorAreaAwareViewEngine : AreaAwareViewEngine
    {
        public RazorAreaAwareViewEngine()
        {
            MasterLocationFormats = new string[]
            {
                //"~/Views/{1}/{0}.cshtml",
                //"~/Views/Shared/{0}.cshtml",
                //"~/Areas/{1}Page/Views/{1}/{0}.cshtml",
                "~/Areas/{1}Page/Views/_ViewStart.cshtml",

            };
            ViewLocationFormats = new string[]
            {
                //"~/{2}/{1}/{0}.cshtml",
                ////"~/{2}/{1}/{0}.ascx",
                //"~/{2}/{0}.cshtml",
                ////"~/{2}/{0}.ascx",
                //"~/Views/{1}/{0}.cshtml",
                ////"~/Views/{1}/{0}.ascx",
                //"~/Views/Shared/{0}.cshtml",
                ////"~/Views/Shared/{0}.ascx",
                "~/Areas/{1}Page/Views/{1}/{0}.cshtml",
                "~/Areas/{1}Page/Views/Shared/{0}.cshtml",
            };
            PartialViewLocationFormats = ViewLocationFormats;
        }

        protected override IView CreatePartialView(
            ControllerContext controllerContext, string partialPath)
        {
            return new WebFormView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext,
            string viewPath, string masterPath)
        {
            return new WebFormView(controllerContext, masterPath);
        }
    }
}