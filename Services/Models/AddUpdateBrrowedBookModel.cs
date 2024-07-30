using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services.Models
{
    public class AddUpdateBrrowedBookModel : IValidatableObject
    {
        public DateTime StartDate { get; set; }
        public required int BookId { get; set; }
        public required int UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BookId == 0 || UserId == 0)
            {
                yield return new ValidationResult("شناسه کتاب و شناسه کاربر نمیتواند 0 باشند");
            }
        }
    }
}
