using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.API.Entities;
using Bookstore.API.Models;
using Bookstore.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookstore.API.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/authors")]
    public class BooksController : Controller
    {
        private ILogger<BooksController> _logger;
        private IMailService _mailService;
        private IBookstoreRepository _repository;

        public BooksController(ILogger<BooksController> logger,
            IMailService mailService,
            IBookstoreRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{authorId}/books")]
        public IActionResult GetBooks(int authorId)
        {
            try
            {
                if (!_repository.AuthorExists(authorId))
                {
                    return NotFound();
                }

                var books = _repository.GetBooksForAuthor(authorId);

                var result = new List<BookDto>();
                foreach (var book in books)
                {
                    result.Add(new BookDto {
                        Id = book.Id,
                        Name = book.Name,
                        Description = book.Description
                    });
                }

                return Ok(result);
            }
            catch(Exception ex)
            {
                //_logger.LogCritical($"{authorId} numaralı " +
                //    $"yazarın kitapları aranırken hata oldu.");
                _mailService.Send("Hata oluştu", $"{authorId} numaralı " +
                    $"yazarın kitapları aranırken hata oldu.");
                return StatusCode(500, "Teknik hata oldu.");
            }

        }

        [HttpGet("{authorId}/books/{bookId}", Name = "GetBook")]
        public IActionResult GetBook(int authorId, int bookId)
        {
            var author = BookstoreDataStore.Current.Authors
                .FirstOrDefault(o => o.Id == authorId);

            if (author == null)
            {
                return NotFound();
            }

            var book = author.Books
                .FirstOrDefault(o => o.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost("{authorId}/books")]
        public IActionResult CreateBook(int authorId,
            [FromBody] BookCreationDto book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            if(book.Name == "Mert")
            {
                ModelState.AddModelError("Name", "İsmi Mert olamaz.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _repository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            var bookForInsert = Mapper.Map<Book>(book);

            author.Books.Add(bookForInsert);
            _repository.Save();

            var result = Mapper.Map<BookDto>(bookForInsert);

            return CreatedAtRoute("GetBook", 
                new { authorId = authorId , bookId = bookForInsert.Id},
                result);
        }

        [HttpPut("{authorId}/books/{bookId}")]
        public IActionResult UpdateBook(int authorId, int bookId,
            [FromBody] BookUpdateDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (book.Name == "Mert")
            {
                ModelState.AddModelError("Name", "İsmi Mert olamaz.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = BookstoreDataStore.Current.Authors
                .FirstOrDefault(o => o.Id == authorId);

            if (author == null)
            {
                return NotFound();
            }

            var bookForUpdate = author.Books
                .FirstOrDefault(o => o.Id == bookId);

            if (bookForUpdate == null)
            {
                return NotFound();
            }

            bookForUpdate.Name = book.Name;
            bookForUpdate.Description = book.Description;

            return NoContent();
        }

        [HttpPatch("{authorId}/books/{bookId}")]
        public IActionResult UpdateBookPartially(int authorId, int bookId,
            [FromBody] JsonPatchDocument<BookUpdateDto> patch)
        {
            if (patch == null)
            {
                return BadRequest();
            }

            var author = BookstoreDataStore.Current.Authors
                .FirstOrDefault(o => o.Id == authorId);

            if (author == null)
            {
                return NotFound();
            }

            var bookForUpdate = author.Books
                .FirstOrDefault(o => o.Id == bookId);

            if (bookForUpdate == null)
            {
                return NotFound();
            }

            var bookForPatch = new BookUpdateDto
            {
                Name = bookForUpdate.Name,
                Description = bookForUpdate.Description
            };

            patch.ApplyTo(bookForPatch, ModelState);

            TryValidateModel(bookForPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bookForUpdate.Name = bookForPatch.Name;
            bookForUpdate.Description = bookForPatch.Description;

            return NoContent();
        }

        [HttpDelete("{authorId}/books/{bookId}")]
        public IActionResult DeleteBook(int authorId, int bookId)
        {
            if (!_repository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForDelete = _repository
                .GetBookForAuthor(authorId, bookId);

            if (bookForDelete == null)
            {
                return NotFound();
            }

            _repository.Delete(bookForDelete);
            if (!_repository.Save())
            {
                return StatusCode(500, "Teknik hata");
            }

            return NoContent();
        }
    }
}