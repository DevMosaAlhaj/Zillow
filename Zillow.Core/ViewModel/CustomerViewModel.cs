using System.Collections.Generic;

namespace Zillow.Core.ViewModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
        public string Address { get; set; }
        
        public List<ContractViewModel> Contracts { get; set; }
       


    }
}
