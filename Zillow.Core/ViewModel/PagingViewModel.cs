namespace Zillow.Core.ViewModel
{
    public class PagingViewModel
    {
        public int CurrentPage{ get; set; }

        public int PagesCount { get; set; }

        public object Data { get; set; }
    }
}