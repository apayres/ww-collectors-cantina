using CollectorsCantina.Web.Models.Items;

namespace CollectorsCantina.Web.Models.Lists
{
    public class ItemListViewModel : PageViewModel
    {
        public List<ItemViewModel> ActionFigures { get; set; } = new List<ItemViewModel>();

        public List<ItemViewModel> ShipsAndVehicles { get; set; } = new List<ItemViewModel>();
        
        public List<ItemViewModel> Other { get; set; } = new List<ItemViewModel>();
    }
}
