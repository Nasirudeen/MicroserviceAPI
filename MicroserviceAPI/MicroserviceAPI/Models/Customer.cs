using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceAPI.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "PhoneNo is required !")]
        [Display(Description = "PhoneNo", Name = "PhoneNo")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String PhoneNo { get; set; }
        [Required(ErrorMessage = "EmailAddress is required !")]
        [Display(Description = "EmailAddress", Name = "EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public String EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required !")]
        [Display(Description = "Password", Name = "Password")]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required(ErrorMessage = "State is required !")]
        [Display(Description = "State", Name = "State")]
        public String State { get; set; }
        [Required(ErrorMessage = "Lga is required !")]
        [Display(Description = "Lga", Name = "Lga")]
        public String Lga { get; set; }
        [Required(ErrorMessage = "OTPCode is required !")]
        [Display(Description = "OTPCode", Name = "Lga")]
        public String OTPCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
