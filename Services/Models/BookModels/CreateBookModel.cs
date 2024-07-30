using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookModels
{
    public record CreateBookModel : IValidatableObject
    {
        public required string Title { get; set; }
        public required int PublicationDate { get; set; }
        public required int AuthorId { get; set; }
        public required List<int> CategoriesId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title == null || Title.Length == 0)
            {
                yield return new ValidationResult("نام کتاب نمیتواند خالی باشد");
            }
            if (PublicationDate > 2024 || PublicationDate == null)
            {
                yield return new ValidationResult("کتابی که هنوز چاپ نشده است را نمیتوان افزود");
            }
        }
    }
}
