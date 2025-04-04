using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.Models.Items;
using CollectorsCantina.Web.Services.Items;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages.Item
{
    public partial class Create
    {
        #region Private Variables

        private ItemViewModel Model { set; get; }

        #endregion

        #region Dependency Injection

        [Parameter]
        public Guid CollectionId { get; set; }

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

            Model = await _service.LoadModel(null);
            Model.CollectionId = CollectionId;

            _state.CollectionListState.SetSelectedCollectionId(Model.CollectionId);
        }

        #endregion

        #region Local Methods & Events

        protected async Task OnSubmit()
        {
            try
            {
                Model = await _service.ProcessModel(Model, ProcessModelAction.Create);
                _navigationManager.NavigateTo("/item/index/" + Model.ItemId);
            }
            catch (Exception ex)
            {
                _state.UserMessageState.SetUserMessage($"Item Failed to Create: {ex.Message}", MessageType.Error);
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
            if(matchingTag != null)
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
            if(matchingTag == null)
            {
                return;
            }

            Model.Tags.Remove(matchingTag);
            return;
        }

        #endregion
    }
}
