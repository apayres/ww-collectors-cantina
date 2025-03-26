using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Layout
{
    public partial class MobileMenu
    {
        private bool ShowMobileMenu = false;

        [Inject]
        private ApplicationState _state { set; get; }

        protected override async Task OnInitializedAsync()
        {
            _state.MobileMenuState.OnChange += () =>
            {
                ShowMobileMenu = _state.MobileMenuState.IsMobileMenuVisible;
                base.StateHasChanged();
            };
        }

    }
}
