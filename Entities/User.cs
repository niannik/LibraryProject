using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public  required string Address { get; set; }
        public required string Phone { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<BorrowedBook>? BorrowedBooks { get; set;}
    }
}
