using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc2Inlupp2.ViewModels
{
    public class IsAllowedGenderAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string gender = Convert.ToString(value);
            if (gender == "other" || gender == "female" || gender == "male" || gender == "")
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("gender has to be: 'other', 'female' or 'male'");
        }
    }
    /*
        CustomerId	int	Unchecked
        Gender	nvarchar(6)	Unchecked
        Givenname	nvarchar(100)	Unchecked
        Surname	nvarchar(100)	Unchecked
        Streetaddress	nvarchar(100)	Unchecked
        City	nvarchar(100)	Unchecked
        Zipcode	nvarchar(15)	Unchecked
        Country	nvarchar(100)	Unchecked
        CountryCode	nvarchar(2)	Unchecked
        Birthday	date	Checked
        NationalId	nvarchar(20)	Checked
        Telephonecountrycode	nvarchar(10)	Checked
        Telephonenumber	nvarchar(25)	Checked
        Emailaddress	nvarchar(100)	Checked
    */
    public class CustomerEditViewModel
    {
        [Required]
        [StringLength(100)]
        public string Streetaddress { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [StringLength(6)]
        [Required]
        [IsAllowedGender]
        public string Gender { get; set; }
        [StringLength(100)]
        [Required]
        public string Givenname { get; set; }
        [StringLength(100)]
        [Required]
        public string Surname { get; set; }
        [StringLength(100)]
        [Required]
        public string City { get; set; }
        [StringLength(15)]
        [Required]
        public string Zipcode { get; set; }
        [StringLength(100)]
        [Required]
        public string Country { get; set; }
        [StringLength(2)]
        [Required]
        public string CountryCode { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }
        [StringLength(20)]
        public string NationalId { get; set; }
        [StringLength(10)]
        public string Telephonecountrycode { get; set; }
        [StringLength(25)]
        public string Telephonenumber { get; set; }
        [StringLength(100)]
        [EmailAddressAttribute]
        public string Emailaddress { get; set; }
    }
}
