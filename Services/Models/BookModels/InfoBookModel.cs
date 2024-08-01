using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookModels
{
    public class InfoBookModel
    {
        public GetBookModel? BookModel { get; set; }
        public required int BorrowedCount { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Borrowed,
        Refunded
    }
}
