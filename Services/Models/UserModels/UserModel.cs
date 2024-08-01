using Entities;
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
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName == null || UserName.Length == 0)
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
            if(Password == "123456")
            {
                yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد");
            }

        }
    }
}
