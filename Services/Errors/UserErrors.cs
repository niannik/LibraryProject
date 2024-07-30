using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Errors
{
    public class UserErrors
    {
        public static readonly Error UserNotFound = new("کاربری با این مشخصات یافت نشد", "User_Not_Found"); 
        public static readonly Error EmptyUserTable = new("کاربری برای نمایش پیدا نشد", "Empty_User_Table"); 
    }
}
