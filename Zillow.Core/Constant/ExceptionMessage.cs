namespace Zillow.Core.Constant
{
    public static class ExceptionMessage
    {
        public const string EntityNotFound = "Not Found or it's Deleted";
        public const string EmptyFile = "Can't Save an Empty File";
        public const string InvalidEmail = "Email not Valid";
        public const string UpdateEntityIdError = "Id property is a Primary Key and can't be modified";
        public const string LoginFailure = "Error When Login to Server\nPlease Make Sure Your Email & Password is Correct";
        public const string UserRegistrationFailure = "User registration failed for the following reasons:";
    }
}