namespace CollectorsCantina.Web.Models.Items
{
    public class ItemMetaDataViewModel
    {
        public int YearReleased { get; set; }

        public List<string> Accessories { get; set; } = new List<string>();
    }
}
