using AutoMapper;
using AutoMapper.QueryableExtensions;
using book_editor.service.Mapper;
using book_editor.service.Utility;
using book_rditor.service.ViewModels;
using book_editor.data.DB;
using book_editor.data.DB.Models;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using book_editor.service.Commom;

namespace book_rditor.service.BookServices
{
    public class BookService : CoomonRestService<Book, BookViewModel>, IBookService
    {
        #region ctor
        public BookService(IRepository<Book> bookRepository, IMapperConfigurator mapperConfigurator) : base(mapperConfigurator, bookRepository)
        {}
        #endregion

        public List<BookViewModel> Get()
        {
            return _repository.GetCollection().ProjectTo<BookViewModel>(_configurationProvider).ToList();
        }
        public override BookViewModel Create(BookViewModel model)
        {
            var book = _mapper.Map<Book>(model);
            if (model.Auctors.Count() > 0)
            {
                foreach (var author in model.Auctors)
                {
                    book.Authors.Add(author);
                }
            };
            _repository.Create(book);
            _repository.Save();
            model = _mapper.Map<BookViewModel>(book);
            return model;
        }
        public DataSourceResult Get(DataSourceRequest request)
        {
            return _repository.GetCollection().ProjectTo<BookViewModel>(_configurationProvider).OrderBy(x=>x.AuditDateTime).ToDataSourceResult(request);
        }
    }
}
