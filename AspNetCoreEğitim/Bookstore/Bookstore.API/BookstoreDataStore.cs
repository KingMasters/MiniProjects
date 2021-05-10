using Bookstore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API
{
    public class BookstoreDataStore
    {
        public static BookstoreDataStore Current { get; } 
            = new BookstoreDataStore();

        public List<AuthorDto> Authors { get; set; }

        public BookstoreDataStore()
        {
            Authors = new List<AuthorDto>
            {
                new AuthorDto
                {
                    Id = 1,
                    Name = "Victor Hugo",
                    Books = new List<BookDto>
                    {
                        new BookDto
                        {
                            Id = 1,
                            Name = "Sefiller",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 2,
                            Name = "Notre Dame'ın Kamburu",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 3,
                            Name = "Bir İdam Mahkumunun Son Günü",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 4,
                            Name = "1793 Devrimi",
                            Description = "Açıklaması"
                        }
                    }
                },
                new AuthorDto
                {
                    Id = 2,
                    Name = "Khaled Hosseini",
                    Books = new List<BookDto>
                    {
                        new BookDto
                        {
                            Id = 5,
                            Name = "Uçurtma Avcısı",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 6,
                            Name = "Bin Muhteşem Güneş",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 7,
                            Name = "Ve Dağlar Yankılandı",
                            Description = "Açıklaması"
                        }
                    }
                },
                new AuthorDto
                {
                    Id = 3,
                    Name = "Paulo Coelho",
                    Books = new List<BookDto>
                    {
                        new BookDto
                        {
                            Id = 8,
                            Name = "Simyacı",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 9,
                            Name = "Veronika Ölmek İstiyor",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 10,
                            Name = "Aldatmak",
                            Description = "Açıklaması"
                        }
                    }
                },
                new AuthorDto
                {
                    Id = 4,
                    Name = "Dostoyevski",
                    Books = new List<BookDto>
                    {
                        new BookDto
                        {
                            Id = 11,
                            Name = "Suç ve Ceza",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 12,
                            Name = "Yeraltından Notlar",
                            Description = "Açıklaması"
                        },
                        new BookDto
                        {
                            Id = 13,
                            Name = "Beyaz Geceler",
                            Description = "Açıklaması"
                        }
                    }
                }
            };
        }
    }
}
