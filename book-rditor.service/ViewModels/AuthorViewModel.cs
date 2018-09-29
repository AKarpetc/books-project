using AutoMapper;
using book_editor.data.DB.Models;
using book_editor.service.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.ViewModels
{
    public class AuthorViewModel: BaseViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class AuthorViewModelProfile : Profile
    {
        public AuthorViewModelProfile()
        {
            CreateMap<Author, AuthorViewModel>().ReverseMap();
        }
    }
}
