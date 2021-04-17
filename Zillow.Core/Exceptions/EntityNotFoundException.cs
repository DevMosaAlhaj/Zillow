using System;
using Zillow.Core.Constant;

namespace Zillow.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName) :
            base($"This {entityName} {ExceptionMessage.EntityNotFound}")
        {
        }
    }
}