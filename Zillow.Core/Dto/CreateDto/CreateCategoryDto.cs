using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zillow.Core.Dto.CreateDto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
