using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Author : BaseEntity
    {
        public required string Name { get; set; }

        public ICollection<Book>? Books { get; set; }

    }
}
