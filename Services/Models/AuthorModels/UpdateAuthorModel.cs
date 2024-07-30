using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.AuthorModels
{
    public class UpdateAuthorModel : IValidatableObject
    {
        public required int Id { get; set; }
        public required string Name { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name!.Equals("Empty", StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("نام نویسنده نمیتواند Empty باشد");
            }
        }
    }
}
