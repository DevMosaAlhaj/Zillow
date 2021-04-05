using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Data.DbEntity
{
    public class AddressDbEntity : BaseEntity
    {
        
        public string CountryName { get; set; }
        
        public string CityName { get; set; }
        
        public List<RealEstateDbEntity> RealEstates { get; set; }
    }
}
