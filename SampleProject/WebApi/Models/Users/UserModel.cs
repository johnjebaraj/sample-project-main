using BusinessEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "Email cannot be null")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        public UserTypes Type { get; set; }
        public decimal? AnnualSalary { get; set; }
        [Range(18, 120, ErrorMessage = "The value must be between 18 to 120")]
        public int Age { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}