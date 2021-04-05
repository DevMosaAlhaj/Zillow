using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
    }
}
