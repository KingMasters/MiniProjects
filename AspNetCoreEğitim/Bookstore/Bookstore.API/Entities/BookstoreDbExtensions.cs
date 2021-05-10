using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Entities
{
    public static class BookstoreDbExtensions
    {
        public static void EnsureSeedData(this BookstoreDbContext context)
        {
            if (context.Authors.Any())
            {
                return;
            }

            var authors = new List<Author>
            {
                new Author
                {
                    Name = "Victor Hugo",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Sefiller",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Notre Dame'ın Kamburu",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Bir İdam Mahkumunun Son Günü",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "1793 Devrimi",
                            Description = "Açıklaması"
                        }
                    }
                },
                new Author
                {
                    Name = "Khaled Hosseini",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Uçurtma Avcısı",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Bin Muhteşem Güneş",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Ve Dağlar Yankılandı",
                            Description = "Açıklaması"
                        }
                    }
                },
                new Author
                {
                    Name = "Paulo Coelho",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Simyacı",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Veronika Ölmek İstiyor",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Aldatmak",
                            Description = "Açıklaması"
                        }
                    }
                },
                new Author
                {
                    Name = "Dostoyevski",
                    Books = new List<Book>
                    {
                        new Book
                        {
                            Name = "Suç ve Ceza",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Yeraltından Notlar",
                            Description = "Açıklaması"
                        },
                        new Book
                        {
                            Name = "Beyaz Geceler",
                            Description = "Açıklaması"
                        }
                    }
                }
            };

            context.AddRange(authors);
            context.SaveChanges();
        }
    }
}
