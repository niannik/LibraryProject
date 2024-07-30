using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookModels
{
    public class ShowFilteredBook
    {
        public string? Title {  get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
    }
}
