using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Application.Common.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime Dob { get; set; } = new DateTime(2000, 1, 1);

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Gender { get; set; } = "Nam";

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public IFormFile Avatar { get; set; }
    }
}