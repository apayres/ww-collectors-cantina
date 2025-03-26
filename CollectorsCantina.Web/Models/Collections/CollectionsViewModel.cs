namespace CollectorsCantina.Web.Models.Collections
{
    public class CollectionsViewModel : PageViewModel
    {
        public List<CollectionViewModel>? Collections { get; set; }

        public Guid? SelectedCollectionId { set; get; }
    }
}
