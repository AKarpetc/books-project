using book_rditor.service.ViewModels;
using book_editor.data.DB.Models;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_rditor.service.BookServices
{
    public interface IBookService
    {
        DataSourceResult Get(DataSourceRequest request);
        List<BookViewModel> Get();
        BookViewModel Create(BookViewModel model);
        BookViewModel Update(BookViewModel model);
        void Delete(BookViewModel model);

    }
}
