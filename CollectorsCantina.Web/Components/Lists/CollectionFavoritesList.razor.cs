using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Collections;
using CollectorsCantina.Web.Services.Collections;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Lists
{
    public partial class CollectionFavoritesList
    {
        #region Private Variables

        private CollectionsViewModel Model { get; set; } = new CollectionsViewModel();

        #endregion

        #region Dependency Injection

        [Inject]
        private ICollectionsService _service { get; set; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            Model = await _service.LoadFavorites();
            Model.IsLoading = false;
        }

        #endregion
    }
}
