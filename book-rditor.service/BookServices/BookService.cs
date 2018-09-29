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

namespace book_rditor.service.BookServices
{
    public class BookService : IBookService
    {
        #region ctor
        private readonly IMapper _mapper;
        private readonly IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> bookRepository, IMapperConfigurator mapperConfigurator)
        {
            _mapper = mapperConfigurator.GetMapper();
            _bookRepository = bookRepository;
        }
        #endregion

        public List<BookViewModel> Get()
        {
           return _bookRepository.GetCollection().ProjectTo<BookViewModel>(_mapper.ConfigurationProvider).ToList();
        }
        public BookViewModel Create(BookViewModel model)
        {
            var book = Mapper.Map<BookViewModel, Book>(model);
            _bookRepository.Create(book);
            _bookRepository.Save();
            return model;
        }
        public DataSourceResult Get(DataSourceRequest request)
        {
            return _bookRepository.GetCollection().ProjectTo<BookViewModel>(_mapper.ConfigurationProvider).ToDataSourceResult(request);
        }
    }
}
