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
        { }
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
                    var authorEntity = _mapper.Map<Author>(author);
                    book.Authors.Add(authorEntity);
                }
            };
            _repository.Create(book);
            _repository.Save();
            model = _mapper.Map<BookViewModel>(book);
            return model;
        }

        public DataSourceResult Get(DataSourceRequest request)
        {

            var bookCollection = _repository.GetCollection();

            #region filter for complicated entity
            var filter = request?.Filter?.Filters?.FirstOrDefault(x => x.Field == "AuctorsShort")?.Value?.ToString();
            if (filter != null)
            {
                request.Filter.Filters = request.Filter.Filters.Where(x => x.Field != "AuctorsShort");
                bookCollection = bookCollection.Where(x => x.Authors.Where(a => (a.Name + a.Surname).Contains(filter)).Count() > 0);
            }
            if (request?.Filter?.Filters?.Count() == 0)
            {
                request.Filter = null;
            }
            #endregion

            return bookCollection.ProjectTo<BookViewModel>(_configurationProvider).OrderBy(x => x.AuditDateTime).ToDataSourceResult(request);
        }
    }
}
