using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zillow.Core.ViewModel
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        public string ContractType { get; set; }
        public double Price { get; set; }
        public RealEstateViewModel RealEstate { get; set; }
        public CustomerViewModel Customer { get; set; }
        

    }
}
