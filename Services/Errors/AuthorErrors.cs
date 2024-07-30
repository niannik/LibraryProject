using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Errors
{
    public class AuthorErrors
    {
        public static readonly Error EmptyAuthorTable = new("نویسنده برای نمایش دادن در کتابخانه نیست", "Empty_Author_Table");
        public static readonly Error AuthorNotFound = new("نویسنده یافت نشد", "Author_NotFound");
    }
}
