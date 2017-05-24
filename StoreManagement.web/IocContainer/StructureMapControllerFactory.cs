using StoreManagement.web.Controllers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoreManagement.web.IocContainer
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //برای انتقال آدرس هایی که کنترلری برای آنها یافت نمیشود به صفحه خطا
            if (controllerType == null)
            {
               // throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.Url.AbsoluteUri.ToString(CultureInfo.InvariantCulture)));
            
                string url = requestContext.HttpContext.Request.RawUrl;
                requestContext.RouteData.Values["controller"] = "error";
                requestContext.RouteData.Values["action"] = "Notfound";
                return SmObjectFactory.Container.GetInstance(typeof(HomeController)) as Controller;
            }

            return SmObjectFactory.Container.GetInstance(controllerType) as Controller;
        }
    }
}