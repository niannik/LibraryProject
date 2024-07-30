using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Errors
{
    public class BookErrors
    {
        public static readonly Error BookNotFound = new("کتابی با این مشخصات یافت نشد", "Book_NotFound");
        public static readonly Error EmptyBookTable = new("کتابی برای نمایش دادن در کتابخانه نیست","Empty_Book_Table");
    }
}
