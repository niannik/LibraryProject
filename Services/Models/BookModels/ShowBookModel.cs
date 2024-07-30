using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookModels
{
    public class ShowBookModel
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public string[] Categories { get; set; }
    }
}
