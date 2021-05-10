using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Entities
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) 
            : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
