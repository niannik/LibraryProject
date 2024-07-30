using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookModels
{
    public class UpdateBookModel
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required int PublicationDate { get; set; }
        public required int AuthorId { get; set; }
        public required List<int> CategoriesId { get; set; }
    }
}
