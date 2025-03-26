namespace CollectorsCantina.Web.State
{
    public class CollectionListState
    {
        public Guid? SelectedCollectionId { set; get; }

        public event Action OnSelectedCollectionChange;

        public event Action OnCollectionAdd;

        public void SetSelectedCollectionId(Guid? selectedCollectionId)
        {
            SelectedCollectionId = selectedCollectionId;
            OnSelectedCollectionChange?.Invoke();
        }

        public void RefreshCollectionList()
        {
            OnCollectionAdd?.Invoke();
        }

        public void ClearSelectedCollectionId()
        {
            SetSelectedCollectionId(null);
        }

    }
}
