using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Collections;
using CollectorsCantina.Web.Services.Collections;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages.Collection
{
    public partial class Create
    {
        #region Private Variables

        private CollectionViewModel Model { set; get; } = new CollectionViewModel();

        #endregion

        #region Dependency Injection

        [Inject]
        private ICollectionService _service { get; set; }

        [Inject]
        private NavigationManager _navigationManager { set; get; }

        [Inject]
        private ApplicationState _state { set; get; }
        
        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            _state.HandlePageInitialize();
            _state.CollectionListState.ClearSelectedCollectionId();
            Model = await _service.LoadModel(null);
        }

        #endregion

        #region Local Methods & Events

        protected async Task OnSubmit()
        {
            try
            {
                Model = await _service.ProcessModel(Model, ProcessModelAction.Create);

                _state.UserMessageState.SetUserMessage("Collection Created Successfully!", MessageType.Success);
                _state.CollectionListState.RefreshCollectionList();

                _navigationManager.NavigateTo("/collection/index/" + Model.CollectionId);
            }
            catch(Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Collection Failed to Create: {ex.Message}", MessageType.Error);
            }
        }

        #endregion
    }
}
