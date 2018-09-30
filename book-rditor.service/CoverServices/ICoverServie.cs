using book_editor.service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace book_editor.service.CoverServices
{
    public interface ICoverService
    {
        int Save(int bookId, HttpPostedFileBase cover);
        CoverViewModel Get(int bookId);
        void Delete(int bookId);
    }
}
