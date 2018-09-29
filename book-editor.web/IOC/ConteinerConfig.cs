using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using book_rditor.service.BookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace book_editor.web.IOC
{
    public class ConteinerConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(typeof(BookService).Assembly);

            new ServiceRegistration(builder);

            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}