using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;

namespace CollectorsCantina.Web.Components.Pages.Account
{
    public partial class Logout
    {
        #region Dependency Injection

        [CascadingParameter]
        public HttpContext _httpContext { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            await _httpContext.SignOutAsync();
        }

        #endregion

    }
}

