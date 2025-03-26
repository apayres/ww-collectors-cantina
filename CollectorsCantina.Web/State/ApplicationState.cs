namespace CollectorsCantina.Web.State
{
    public class ApplicationState
    {
        public CollectionListState CollectionListState = new CollectionListState();

        public UserCenterState UserMessageState = new UserCenterState();

        public MobileMenuState MobileMenuState = new MobileMenuState();

        public void HandlePageInitialize()
        {
            UserMessageState.ClearUserMessage();
            MobileMenuState.Close();
        }
    }
}
