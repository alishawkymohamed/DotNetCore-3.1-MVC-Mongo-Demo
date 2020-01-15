using BooksApi.Models;
using BooksApi.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_3._1_MVC_Mongo_Demo.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepo _bookRepo;

        public BookController(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_bookRepo.Get());
        }

        [HttpGet]
        public IActionResult AddOrEdit(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return View();
            else
                return View(_bookRepo.Get(Id));
        }

        [HttpPost]
        public IActionResult AddOrEdit(Book book)
        {
            if (string.IsNullOrEmpty(book.Id))
                _bookRepo.Create(book);
            else
                _bookRepo.Update(book.Id, book);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var book = _bookRepo.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepo.Remove(book.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}