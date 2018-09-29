using book_editor.service.AuthorsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace book_editor.web.Controllers.api
{
    public class AuthorsController : ApiController
    {
        IAuthorsService _authorsService;
        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_authorsService.Get(id));
        }
    }
}
