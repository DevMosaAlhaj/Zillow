using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zillow.Core.Options
{
    public class MyJwtBearerOptions
    {

        public string Issuer { get; set; }

        public string Site { get; set; }

        public string SigningKey { get; set; }




    }
}
