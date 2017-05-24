using StoreManagement.InfraStructure.Context;
using StoreManagement.web.IocContainer;
using StructureMap.Web.Pipeline;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StoreManagement.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);




            setDbInitializer();

            //Set current Controller factory as StructureMapControllerFactory
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());



        }


        private static void setDbInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainContext, StoreManagement.InfraStructure.Migrations.Configuration>());
            SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
        }


        private void Application_EndRequest(object sender, EventArgs e)
        {
            //تخریب تمام اشیا ساخته شده توسط استراکچرمپ و برگشت منابع در صورتی که طول عمر شی براساس درخواست باشد
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}
