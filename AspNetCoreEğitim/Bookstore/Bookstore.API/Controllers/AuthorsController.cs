using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bookstore.API.Models;
using Bookstore.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private IBookstoreRepository _repository;

        public AuthorsController(IBookstoreRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _repository.GetAuthors();
            var result = Mapper.Map<IEnumerable<AuthorWithoutBooksDto>>(authors);
            return Ok(result);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(int authorId)
        {
            var author = _repository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<AuthorWithoutBooksDto>(author);

            return Ok(result);
        }

    }
}
