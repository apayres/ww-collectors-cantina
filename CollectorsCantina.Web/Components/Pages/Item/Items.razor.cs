using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages.Item
{
    public partial class Items
    {
        #region Dependency Injection

        [Parameter]
        public Guid CollectionId { get; set; }

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
