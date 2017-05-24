using System;
using System.Web;
using StructureMap;
using StructureMap.Web;
using System.Security.Principal;
using System.Threading;
using System.Data.Entity;
using StoreManagement.InfraStructure.Context;
using StoreManagement.Application.Products;

namespace StoreManagement.web.IocContainer
{


    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {

                ioc.For<IUnitOfWork>().HttpContextScoped().Use<MainContext>();
                ioc.For<MainContext>().HttpContextScoped().Use(context => (MainContext)context.GetInstance<IUnitOfWork>());
                ioc.For<DbContext>().HttpContextScoped().Use(context => (MainContext)context.GetInstance<IUnitOfWork>());


                ioc.For<IProductService>().HttpContextScoped().Use<ProductService>();
                
            });
        }
    }
}

         
 