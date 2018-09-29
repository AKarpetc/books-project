using book_editor.service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.AuthorsServices
{
    public interface IAuthorsService
    {
        IEnumerable<AuthorViewModel> Get(int bookId);
        AuthorViewModel Update(AuthorViewModel model);
        AuthorViewModel Create(AuthorViewModel model);
        void Delete(AuthorViewModel model);
    }
}
