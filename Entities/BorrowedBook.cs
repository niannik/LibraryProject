using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BorrowedBook : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate {  get; set; }
        public Book? Book { get; set;}
        public int BookId { get; set; } 
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
