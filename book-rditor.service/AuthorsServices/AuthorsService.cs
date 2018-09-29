using AutoMapper;
using AutoMapper.QueryableExtensions;
using book_editor.data.DB.Models;
using book_editor.service.Mapper;
using book_editor.service.Utility;
using book_editor.service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.AuthorsServices
{
    public class AuthorsService: IAuthorsService
    {
        #region ctor
        private readonly IMapper _mapper;
        private readonly IRepository<Author> _authorRepository;
        public AuthorsService(IRepository<Author> authorRepository, IMapperConfigurator mapperConfigurator)
        {
            _mapper = mapperConfigurator.GetMapper();
            _authorRepository = authorRepository;
        }
        #endregion

        public IEnumerable<AuthorViewModel> Get(int bookId)
        {
            return _authorRepository.GetCollection()/*.Where(x=>x.BookId== bookId)*/.ProjectTo<AuthorViewModel>(_mapper.ConfigurationProvider);
        }
        
    }
}
