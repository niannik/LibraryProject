using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class AddUpdateBrrowedBookModel
    {
        public DateTime StartDate { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
