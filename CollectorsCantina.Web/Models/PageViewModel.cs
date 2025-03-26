namespace CollectorsCantina.Web.Models
{
    public class PageViewModel
    {
        public bool IsLoading { set; get; } = true;

        public bool SaveComplete { set; get; }

        public bool UploadComplete { set; get; }

        public bool DeleteComplete { set; get; }
    }
}
