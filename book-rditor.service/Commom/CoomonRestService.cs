using AutoMapper;
using book_editor.data.DB.Models.Base;
using book_editor.service.Mapper;
using book_editor.service.Utility;
using book_editor.service.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.Commom
{
    public class CoomonRestService<TModel, TView>
        where TModel : BaseTable
        where TView : BaseViewModel
    {
        protected readonly IRepository<TModel> _repository;
        protected readonly IMapper _mapper;
        protected readonly IConfigurationProvider _configurationProvider;
        public CoomonRestService(IMapperConfigurator mapper, IRepository<TModel> repository)
        {
            _mapper = mapper.GetMapper();
            _configurationProvider = mapper.ConfigurationProvider;
            _repository = repository;
        }
        public virtual TView Create(TView view)
        {
            var model = _mapper.Map<TModel>(view);
            _repository.Create(model);
            _repository.Save();
            view = _mapper.Map<TView>(model);
            return view;
        }

        public virtual TView Update(TView view)
        {
            var model = _repository.Get(view.Id);
            model = _mapper.Map(view, model);
            _repository.Update(model);
            _repository.Save();
            view = _mapper.Map<TView>(model);
            return view;
        }

        public virtual void Delete(TView view)
        {
            var model = _repository.Get(view.Id);
            model = _mapper.Map(view, model);
            _repository.Delete(model);
            _repository.Save();
        }
    }
}
