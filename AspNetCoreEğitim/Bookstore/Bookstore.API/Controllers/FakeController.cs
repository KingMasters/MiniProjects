using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Fake")]
    public class FakeController : Controller
    {
        private BookstoreDbContext _context;

        public FakeController(BookstoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Fake()
        {
            var result = _context.Authors.Any();
            return NoContent();
        }
    }
}