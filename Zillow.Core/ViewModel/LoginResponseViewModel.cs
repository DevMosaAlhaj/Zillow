namespace Zillow.Core.ViewModel
{
    public class LoginResponseViewModel
    {

        public string UserId { get; set; }

        public string RefreshToken { get; set; }

        public TokenViewModel TokenViewModel { get; set; }

    }
}
