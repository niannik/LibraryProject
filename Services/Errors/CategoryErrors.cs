using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Errors
{
    public class CategoryErrors
    {
        public static readonly Error CategoryNotFound = new("ژانری با این نام یافت نشد", "Category_NotFound");
        public static readonly Error EmptyCategoryTable = new("ژانری برای نمایش وجود ندارد", "Empty_Category_Table");
    }
}
