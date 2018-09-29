using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.Mapper
{
    public interface IMapperConfigurator
    {
        IMapper GetMapper();
        IConfigurationProvider ConfigurationProvider { get; }
    }
}
