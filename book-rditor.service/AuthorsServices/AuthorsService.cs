using AutoMapper;
using AutoMapper.QueryableExtensions;
using book_editor.data.DB.Models;
using book_editor.service.Commom;
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
    public class AuthorsService : CommonRestService<Author, AuthorViewModel>, IAuthorsService
    {
        #region ctor
        public AuthorsService(IMapperConfigurator mapperConfigurator, IRepository<Author> repository) : base(mapperConfigurator, repository)
        {

        }
        #endregion

        public IEnumerable<AuthorViewModel> Get(int bookId)
        {
            return _repository.GetCollection().Where(x => x.BookId == bookId).ProjectTo<AuthorViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
