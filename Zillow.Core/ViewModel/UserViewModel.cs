using System;

namespace Zillow.Core.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string UserType { get; set; }
        
        public DateTime CreateAt { get; set; }
    }
}