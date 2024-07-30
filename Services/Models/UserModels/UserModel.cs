using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.UserModels
{
    public class UserModel : IValidatableObject
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null || Name.Length == 0)
            {
                yield return new ValidationResult("نام کاربر نمیتواند خالی باشد");
            }
            if (Address.Equals("Mashhad", StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("متاسفانه ارسال به مشهد نداریم");
            }
            if (Phone.Length != 11)
            {
                yield return new ValidationResult("شماره موبایل معتبر نیست");
            }

        }
    }
}
