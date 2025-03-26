using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Collections;
using CollectorsCantina.Web.Services.Collections;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Components.Pages.Collection
{
    public partial class Index
    {
        #region Private Variables
        
        private CollectionViewModel Model { set; get; } = new CollectionViewModel();

        private IBrowserFile ImageToUpload { set; get; }
        
        #endregion

        #region Dependency Injection

        [Parameter]
        public Guid Id { set; get; }

        [Inject]
        private ICollectionService _service { get; set; }

        [Inject]
        private ApplicationState _state { set; get; }

        [Inject]
        private NavigationManager _navigationManager { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            _state.HandlePageInitialize();
            _state.CollectionListState.SetSelectedCollectionId(Id);

            Model = await _service.LoadModel(Id);
            Model.IsLoading = false;
        }

        #endregion

        #region Local Methods & Events

        protected void LoadFile(InputFileChangeEventArgs e)
        {
            ImageToUpload = e.File;
        }

        protected async Task UploadImage()
        {
            try
            {
                var url = await _service.UploadImage(ImageToUpload, Model.CollectionId.ToString(), "collection-images");
                Model.ImageUrl = url;

                await _service.ProcessModel(Model, ProcessModelAction.Update);
                Model.UploadComplete = true;
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Image Failed to Upload: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task SaveCollection()
        {
            try
            {
                await _service.ProcessModel(Model, ProcessModelAction.Update);
                Model.SaveComplete = true;

                _state.UserMessageState.SetUserMessage("Collection Updated Successfully!", MessageType.Success);
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Collection Failed to Save: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task DeleteCollection()
        {
            try
            {
                await _service.ProcessModel(Model, ProcessModelAction.Delete);
                Model.DeleteComplete = true;

                _navigationManager.NavigateTo("/collection/create");
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Collection Failed to Delete: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task SetFavorite()
        {
            try
            {
                Model.IsFavorite = !Model.IsFavorite;
                await _service.ProcessModel(Model, ProcessModelAction.Update);
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Favorite could not be Saved: {ex.Message}", MessageType.Error);
            }
        }

        #endregion
    }
}
