using System;

namespace Zillow.Core.Exceptions
{
    public class UpdateEntityException : Exception
    {
        public UpdateEntityException(string message) : base(message)
        {
            
        }
    }
}