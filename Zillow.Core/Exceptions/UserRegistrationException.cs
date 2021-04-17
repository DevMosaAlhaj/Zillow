using System;
using Zillow.Core.Constant;

namespace Zillow.Core.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public UserRegistrationException(string messages) :
            base($"{ExceptionMessage.UserRegistrationFailure} {messages}")
        {
        }
    }
}