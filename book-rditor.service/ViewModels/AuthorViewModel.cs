using AutoMapper;
using book_editor.data.DB.Models;
using book_editor.service.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.ViewModels
{
    public class AuthorViewModel: BaseViewModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        public int BookId { get; set; }
    }
    public class AuthorViewModelProfile : Profile
    {
        public AuthorViewModelProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>()
                .ForMember(des => des.AuditDateTime, sour => sour.Ignore());
        }
    }
}
