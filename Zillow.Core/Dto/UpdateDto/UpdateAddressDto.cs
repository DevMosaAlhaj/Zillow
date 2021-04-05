using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.Dto.UpdateDto
{
    public class UpdateAddressDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
}
