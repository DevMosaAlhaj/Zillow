using System;
using Zillow.Core.Constant;

namespace Zillow.Core.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() :
            base(ExceptionMessage.EmptyFile)
        {
        }
    }
}