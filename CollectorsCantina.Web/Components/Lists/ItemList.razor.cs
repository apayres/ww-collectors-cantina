using CollectorsCantina.Domain.Enums;
using CollectorsCantina.Web.Models.Items;
using CollectorsCantina.Web.Models.Lists;
using CollectorsCantina.Web.Services.Items;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Lists
{
    public partial class ItemList
    {
        #region Private Variables

        private ItemListViewModel Model = new ItemListViewModel();

        #endregion

        #region Dependency Injection

        [Parameter]
        public string? SearchTerm { get; set; }

        [Parameter]
        public Guid? CollectionId { get; set; }

        [Inject]
        private IItemsService _service { get; set; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            var itemCollection = new ItemsViewModel();
            if (CollectionId.HasValue)
            {
                itemCollection = await _service.LoadModel(CollectionId.Value);
            }
            else if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                itemCollection = await _service.LoadModel(SearchTerm);                
            }

            Model.ActionFigures = itemCollection.Items.Where(x => x.Type == ItemType.ActionFigure).ToList();
            Model.ShipsAndVehicles = itemCollection.Items.Where(x => x.Type == ItemType.Ship).ToList();
            Model.Other = itemCollection.Items.Where(x => x.Type == ItemType.Other).ToList();
            Model.IsLoading = false;
        }

        #endregion
    }
}
