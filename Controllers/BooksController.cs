using BooksApi.Models;
using BooksApi.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;

        public BooksController(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookRepo.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookRepo.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookRepo.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookRepo.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepo.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookRepo.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepo.Remove(book.Id);

            return NoContent();
        }
    }
}