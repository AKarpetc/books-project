using book_rditor.service.BookServices;
using book_rditor.service.ViewModels;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace book_editor.web.Controllers.api
{
    public class BooksController : ApiController
    {
        public readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IHttpActionResult GetBooks(DataSourceRequest request)
        {
             return Ok(_bookService.Get(request));
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_bookService.Get());
        }
        //[HttpPost]
        //public IHttpActionResult Post(DataSourceRequest request)
        //{
        //    return Ok(_bookService.Get(request));
        //}
        [HttpPut]
        public IHttpActionResult Put(BookViewModel request)
        {
             return Ok();
        }
    }
}
