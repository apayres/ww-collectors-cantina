using CollectorsCantina.Domain.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace CollectorsCantina.Web.Components.Pages.Account
{
    public partial class Login
    {

        #region Private Variables

        [SupplyParameterFromForm]
        private string Username { set; get; }

        [SupplyParameterFromForm]
        private string Password { set; get; }

        private string ErrorMessage { set; get; }

        #endregion

        #region Dependency Injection

        [CascadingParameter]
        public HttpContext _httpContext { set; get; }

        [Inject]
        public NavigationManager _navigationManager { set; get; }

        [Inject]
        public IConfiguration _configuration { set; get; }

        #endregion

        #region Lifecycle Methods

        protected override async Task OnInitializedAsync()
        {
            await _httpContext.SignOutAsync();
        }

        #endregion

        #region Local Methods & Events

        protected async Task OnSubmit()
        {
            var adminUser = _configuration["Authentication:AdminUser"].ToString();
            var adminPassword = _configuration["Authentication:AdminPassword"].ToString();

            if (Username.ToLower() == adminUser.ToLower() && Password == adminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.Role, AppRole.Administrator.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await _httpContext.SignInAsync(principal);

                _navigationManager.NavigateTo("/");
            }
            else
            {
                ErrorMessage = "Sorry, invalid credentials!";
            }
        }

        #endregion
    }
}
