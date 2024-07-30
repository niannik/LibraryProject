using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Errors
{
    public class BorrowedBooksErrors
    {
        public static readonly Error InvalidBorrow = new Error("ورودی نامعتبر", "Invalid_Borrow");
    }
}
