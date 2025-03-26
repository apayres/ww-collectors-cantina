using CollectorsCantina.Web.Enums;

namespace CollectorsCantina.Web.State
{
    public class UserCenterState
    {
        public string UserMessage { set; get; }

        public MessageType UserMessageType { set; get; }

        public event Action OnChange;

        public void SetUserMessage(string message, MessageType messageType)
        {
            UserMessage = message;
            UserMessageType = messageType;
            NotifyStateChanged();
        }

        public void ClearUserMessage()
        {
            UserMessage = string.Empty;
            UserMessageType = MessageType.None;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
