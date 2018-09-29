using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.Mapper
{
    public class MapperConfigurator: IMapperConfigurator
    {
        public MapperConfigurator()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetExecutingAssembly());

            });
        }

        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IMapper GetMapper()
        {
            return ConfigurationProvider.CreateMapper();
        }
    }
}
