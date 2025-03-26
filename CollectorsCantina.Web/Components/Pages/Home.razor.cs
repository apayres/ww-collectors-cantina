using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages
{
    public partial class Home
    {
        #region Dependency Injection

        [Inject]
        public ApplicationState _state { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            _state.HandlePageInitialize();
        }

        #endregion
    }
}
