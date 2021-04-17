using System;
using Zillow.Core.Constant;

namespace Zillow.Core.Exceptions
{
    public class LoginFailureException : Exception
    {
        public LoginFailureException() : base(ExceptionMessage.LoginFailure)
        {
            
        }
    }
}