using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Layout
{
    public partial class Header
    {
        private string SearchTerm = string.Empty;

        private bool IsMobileMenuVisible = false;

        [Inject]
        private ApplicationState _state { set; get; }

        [Inject]
        private NavigationManager _navigationManager { set; get; }

        protected override async Task OnInitializedAsync()
        {
            _state.MobileMenuState.OnChange += () =>
            {
                IsMobileMenuVisible = _state.MobileMenuState.IsMobileMenuVisible;
                base.StateHasChanged();
            };
        }

        protected void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return;
            }

            _navigationManager.NavigateTo("/items/search/" + SearchTerm);
        }

        protected void ShowMenu()
        {
            if (IsMobileMenuVisible)
            {
                _state.MobileMenuState.Close();
            }
            else
            {
                _state.MobileMenuState.Open();
            }
        }
    }
}
