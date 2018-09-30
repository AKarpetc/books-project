using AutoMapper;
using book_editor.service.ViewModels;
using book_editor.service.ViewModels.Base;
using book_editor.data.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using book_editor.service.Utility;

namespace book_rditor.service.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        [Required]
        [StringLength(30)]
        [Display(Name ="Заголовок")]
        public string Header { get; set; }

        [Range(1, 10000, ErrorMessage = "Старниц должно быть от 1 до 10000")]
        [Display(Name = "Количество страниц")]
        public int PageCount { get; set; }

        [StringLength(30)]
        [Display(Name = "Издательство")]
        public string PublishingOffice { get; set; }

        [Min(1800)]
        [Display(Name = "Год публикации")]
        public int PublishYear { get; set; }

        [InputMaskAttribute("000-0-00-000000-0", ErrorMessage = "{0} значение не совпадаетс с форматом {1}.")]
        public string ISBN { get; set; }

        public IEnumerable<string> AuctorsShort { get; set; }

        public IEnumerable<AuthorViewModel> Auctors { get; set; }

        public bool IsWithCover { get; set; }
    }
    public class BookViewModelProfile : Profile
    {
        public BookViewModelProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(des => des.AuctorsShort, sour => sour.MapFrom(prop => prop.Authors.Where(x => x.IsDelete == false).Select(x => x.Name + " " + x.Surname)))
                .ForMember(des=>des.IsWithCover, sour => sour.MapFrom(prop =>prop.Covers.Any()));

            CreateMap<BookViewModel, Book>()
                 .ForMember(des => des.AuditDateTime, sour => sour.Ignore());

        }
    }
}
