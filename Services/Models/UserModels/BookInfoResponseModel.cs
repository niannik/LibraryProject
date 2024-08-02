using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.UserModels
{
    public class BookInfoResponseModel
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required List<string> Categories { get; set; }
        public required string Accessibility { get; set; }
    }
}
