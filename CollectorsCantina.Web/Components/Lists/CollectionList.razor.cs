using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Collections;
using CollectorsCantina.Web.Services.Collections;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Lists
{
    public partial class CollectionList
    {
        #region Private Variables

        private CollectionsViewModel Model { get; set; } = new CollectionsViewModel();

        #endregion

        #region Dependency Injection

        [Parameter]
        public CollectionRenderType RenderType { set; get; }

        [Inject]
        private ICollectionsService _service { get; set; }

        [Inject]
        private ApplicationState _state { set; get; }
        
        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            Model = await _service.LoadModel();
            Model.IsLoading = false;

            _state.CollectionListState.OnSelectedCollectionChange += () =>
            {
                Model.SelectedCollectionId = _state.CollectionListState.SelectedCollectionId;
                base.StateHasChanged();
            };

            _state.CollectionListState.OnCollectionAdd += () =>
            {
                Model.IsLoading = true;
                
                var task = Task.Run(async () => Model = await _service.LoadModel());
                task.Wait();

                Model.IsLoading = false;
            };
        }

        #endregion
    }
}
