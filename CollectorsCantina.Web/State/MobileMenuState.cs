namespace CollectorsCantina.Web.State
{
    public class MobileMenuState
    {
        public bool IsMobileMenuVisible { set; get; }

        public event Action OnChange;

        public void Open()
        {
            IsMobileMenuVisible = true;
            NotifyStateChanged();
        }

        public void Close()
        { 
            IsMobileMenuVisible =false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
