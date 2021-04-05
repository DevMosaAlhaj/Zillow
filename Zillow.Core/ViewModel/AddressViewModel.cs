using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.ViewModel
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public List<RealEstateViewModel> RealEstates { get; set; }

    }
}
