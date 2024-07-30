using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required int PublicationYear { get; set; }

        public bool IsDeleted { get; set; }

        public Author? Author { get; set; }
        public int AuthorId { get; set; }

        public ICollection <BorrowedBook>? BorrowedBooks { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}
