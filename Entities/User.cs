using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : IdentityUser<int>
    {
        public  required string Address { get; set; }
        public required string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastLoginDate { get; set; }
        public ICollection<BorrowedBook>? BorrowedBooks { get; set;}
    }
}
