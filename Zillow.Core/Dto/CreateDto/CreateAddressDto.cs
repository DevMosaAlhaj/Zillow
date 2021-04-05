using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateAddressDto
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
}
