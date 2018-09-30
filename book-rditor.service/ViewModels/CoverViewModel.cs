using AutoMapper;
using book_editor.data.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_editor.service.ViewModels
{
   public class CoverViewModel
    {
        public int BookId { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

    }
    public class CoverViewModelProfile : Profile
    {
        public CoverViewModelProfile()
        {
            CreateMap<Cover, CoverViewModel>();
        }
    }
}
