using book_editor.service.AuthorsServices;
using book_editor.service.ViewModels;
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

        [HttpPost]
        public IHttpActionResult Post(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model = _authorsService.Create(model);
            return Ok(model);
        }

        [HttpPut]
        public IHttpActionResult Put(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_authorsService.Update(model));
        }

        [HttpDelete]
        public IHttpActionResult Delete(AuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authorsService.Delete(model);
            return Ok();
        }
    }
}
