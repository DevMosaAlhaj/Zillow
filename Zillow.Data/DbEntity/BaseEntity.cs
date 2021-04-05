using System;
using System.Collections.Generic;
using System.Text;

namespace Zillow.Data.DbEntity
{
    public class BaseEntity
    {
        
        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDelete = false;
        }
        
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; }

    }
}
