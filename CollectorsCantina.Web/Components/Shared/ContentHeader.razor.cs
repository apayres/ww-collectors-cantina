using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Shared
{
    public partial class ContentHeader
    {
        #region Dependency Injection

        [Parameter]
        public string Title { set; get; } = string.Empty;

        #endregion
    }
}
