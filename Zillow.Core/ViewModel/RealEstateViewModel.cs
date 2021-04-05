using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.ViewModel
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryViewModel Category { get; set; }
        public AddressViewModel Address { get; set; }

        public List<ImageViewModel> Images { get; set; }

        public List<ContractViewModel> Contracts { get; set; }
    }
}
