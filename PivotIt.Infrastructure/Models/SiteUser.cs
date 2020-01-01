using Microsoft.AspNetCore.Identity;
using System;

namespace PivotIt.Infrastructure.Entities
{
    public class SiteUser : IdentityUser
    {
        public string PasswordSalt { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
