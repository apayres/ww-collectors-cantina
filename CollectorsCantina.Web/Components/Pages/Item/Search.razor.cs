using CollectorsCantina.Web.Models.Items;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages.Item
{
    public partial class Search
    {
        #region Private Variables

        private SearchViewModel Model = new SearchViewModel();

        #endregion

        #region Dependency Injection

        [Parameter]
        public string SearchTerm { set; get; }

        [Inject]
        public ApplicationState _state { set; get; }

        [Inject]
        private NavigationManager _navigationManager { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            _state.HandlePageInitialize();
            _state.CollectionListState.ClearSelectedCollectionId();
        }

        #endregion

        #region Local Methods & Events

        protected void SearchItems()
        {
            if (string.IsNullOrWhiteSpace(Model.OnPageSearchTerm))
            {
                return;
            }

            _navigationManager.NavigateTo("/items/search/" + Model.OnPageSearchTerm);
        }

        #endregion
    }
}
