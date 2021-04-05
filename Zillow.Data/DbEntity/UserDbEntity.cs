using Microsoft.AspNetCore.Identity;
using System;
using Zillow.Core.Enum;

namespace Zillow.Data.DbEntity
{
    public class UserDbEntity : IdentityUser
    {
        public UserDbEntity()
        {
            CreateAt = DateTime.Now;
            IsDeleted = false;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string RefreshToken { get; set; }
        public string FcmToken { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
