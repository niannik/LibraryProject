using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public record BorrowedBookModel
    {
        public required string Username { get; set; }
        public required BookDto BookInfo { get; set; }

        public record BookDto
        { 
            public required string Author { get; set; }
            public required string Title { get; set; }
            public required List<string> Categories { get; set; }
        }
    }
}
