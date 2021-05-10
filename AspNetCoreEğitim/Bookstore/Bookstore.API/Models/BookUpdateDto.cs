using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class BookUpdateDto
    {
        [Required(ErrorMessage = "Kitap adı zorunlu")]
        [MaxLength(20, ErrorMessage = "Kitap ismi en fazla 20 karakter olabilir.")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Kitap açıklaması en fazla 50 karakter olabilir.")]
        public string Description { get; set; }
    }
}
