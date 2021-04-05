using System;
using System.Collections.Generic;
using System.Text;

namespace Zillow.Core.ViewModel
{
    public class ApiResponseViewModel
    {
        public ApiResponseViewModel(bool status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ApiResponseViewModel(bool status, string message)
        {
            Status = status;
            Message = message;
        }
        
        public ApiResponseViewModel(bool status)
        {
            Status = status;
            
        }

        public bool Status { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
