using Autofac;
using book_editor.service.Mapper;
using book_editor.service.Utility;
using book_editor.web.App_Start;
using book_rditor.service.BookServices;
using book_editor.data.DB;
using book_editor.data.DB.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using book_editor.data.DB.Models;
using book_editor.service.AuthorsServices;
using book_editor.service.CoverServices;

namespace book_editor.web.IOC
{
    public class ServiceRegistration
    {
        public ServiceRegistration(ContainerBuilder builder)
        {
            ImplementationRegistration implementationRegistration = new ImplementationRegistration(builder);
            builder.RegisterType<MapperConfigurator> ().As<IMapperConfigurator>().InstancePerLifetimeScope();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorsService>().As<IAuthorsService>().InstancePerLifetimeScope();
            builder.RegisterType<CoverService>().As<ICoverService>().InstancePerLifetimeScope();
            
            implementationRegistration.RegisterGenericImplementation<BaseTable>(typeof(Repository<>), typeof(IRepository<>), RegisterAssemblyType.FromGeneric);


        }
    }
}