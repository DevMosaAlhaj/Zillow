using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateCustomerDto 
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
