using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Items;
using CollectorsCantina.Web.Services.Items;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CollectorsCantina.Web.Components.Pages.Item
{
    public partial class Index
    {
        #region Private Variables

        private ItemViewModel Model { set; get; } = new ItemViewModel();

        private IReadOnlyList<IBrowserFile> ImagesToUpload { set; get; }

        #endregion

        #region Dependency Injection

        [Parameter]
        public Guid? Id { get; set; }

        [Inject]
        private IItemService _service { get; set; }

        [Inject]
        public ApplicationState _state { set; get; }

        [Inject]
        private NavigationManager _navigationManager { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            _state.HandlePageInitialize();
            Model = await _service.LoadModel(Id);
            Model.IsLoading = false;
        }

        #endregion

        #region Local Methods & Events

        protected void LoadFile(InputFileChangeEventArgs e)
        {
            ImagesToUpload = e.GetMultipleFiles();
        }

        protected async Task SaveItem()
        {
            try
            {
                Model = await _service.ProcessModel(Model, ProcessModelAction.Update);
                Model.SaveComplete = true;
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Item Failed to Save: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task DeleteItem()
        {
            try
            {
                await _service.ProcessModel(Model, ProcessModelAction.Delete);
                _navigationManager.NavigateTo("/collection/index/" + Model.CollectionId);
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Item Failed to Delete: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task DeleteImage(string imageUrl)
        {
            var image = Model.Images.FirstOrDefault(x => x == imageUrl);
            if (image == null)
            {
                return;
            }

            try
            {
                Model.Images.Remove(image);
                await _service.ProcessModel(Model, ProcessModelAction.Update);
                Model.DeleteImageComplete = true;
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Item Image failed to Delete: {ex.Message}", MessageType.Error);
            }
        }

        protected async Task UploadImages()
        {
            try
            {
                List<string> urls = new List<string>();
                var currentImageCount = Model.Images == null ? 0 : Model.Images.Count();

                var container = Model.CollectionId.ToString();
                for (var i = 0; i < ImagesToUpload.Count(); i++)
                {
                    var fileName = Model.ItemId + "_" + (i + currentImageCount).ToString();
                    var url = await _service.UploadImage(ImagesToUpload[i], fileName, container);
                    urls.Add(url);
                }

                if (Model.Images == null)
                {
                    Model.Images = new List<string>();
                }

                Model.Images.AddRange(urls);

                await _service.ProcessModel(Model, ProcessModelAction.Update);
                Model.UploadComplete = true;
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Images failed to Upload: {ex.Message}", MessageType.Error);
            }
        }

        protected void AddTag()
        {
            if (string.IsNullOrWhiteSpace(Model.NewTag))
            {
                return;
            }

            if (Model.Tags == null)
            {
                Model.Tags = new List<string>();
            }

            var matchingTag = Model.Tags.FirstOrDefault(x => x == Model.NewTag);
            if (matchingTag != null)
            {
                return;
            }

            Model.Tags.Add(Model.NewTag.ToString());

            Model.NewTag = string.Empty;
            return;
        }

        protected void RemoveTag(string tag)
        {
            var matchingTag = Model.Tags.FirstOrDefault(x => x == tag);
            if (matchingTag == null)
            {
                return;
            }

            Model.Tags.Remove(matchingTag);
            return;
        }

        #endregion
    }
}
