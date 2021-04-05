using System;
using System.Collections.Generic;
using System.Text;

namespace Zillow.Core.Dto.UpdateDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
    }
}
