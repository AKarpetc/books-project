using AutoMapper;
using book_editor.service.ViewModels;
using book_editor.service.ViewModels.Base;
using book_editor.data.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_rditor.service.ViewModels
{
    public class BookViewModel: BaseViewModel
    {
        
        public string Header { get; set; }

        public int PageCount { get; set; }

        public string PublishingOffice { get; set; }

        public int PublishYear { get; set; }

        public string ISBN { get; set; }

        public IEnumerable<string> AuctorsShort { get; set; }
        public IEnumerable<Author> Auctors { get; set; }

    }
    public class BookViewModelProfile : Profile
    {
        public BookViewModelProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(des => des.AuctorsShort, sour => sour.MapFrom(prop => prop.Authors.Select(x => x.Name + " " + x.Surname)));

            CreateMap<BookViewModel, Book>()
                 .ForMember(des => des.AuditDateTime, sour => sour.Ignore());
 
        }
    }
}
