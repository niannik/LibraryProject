using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.AuthorModels
{
    public class GetAuthorModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int BooksCount { get; set; }
    }
}
