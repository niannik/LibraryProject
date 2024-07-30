using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class CategoryModel : IValidatableObject
    {
        public required string Name { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null || Name.Length == 0)
            {
                yield return new ValidationResult("نام ژانر نمیتواند خالی باشد");
            }
            else if (Name.Equals("university" , StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("نمیتوان کتاب درسی ای اضافه کرد");
            }
        }
    }
}
