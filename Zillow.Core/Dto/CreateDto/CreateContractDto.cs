using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Zillow.Core.Enum;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateContractDto
    {
        public ContractType ContractType { get; set; }
        public double Price { get; set; }
        public int RealEstatesId { get; set; }
        public int CustomerId { get; set; }
    }
}
