using CollectorsCantina.Web.Enums;
using CollectorsCantina.Web.State;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Shared
{
    public partial class MessageCenter
    {
        #region Private Variables

        private string Message { set; get; }

        private MessageType MessageType { set; get; }
        
        #endregion

        #region Dependency Injection
        
        [Inject]
        private ApplicationState AppState { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            AppState.UserMessageState.OnChange += () =>
            {
                Message = AppState.UserMessageState.UserMessage;
                MessageType = AppState.UserMessageState.UserMessageType;
                base.StateHasChanged();
            };
        }

        #endregion
    }
}
