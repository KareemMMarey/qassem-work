
using QassimPrincipality.Web.Identity.Helpers.Localization;

namespace QassimPrincipality.Web.Identity.Configuration
{
    public class AdminConfiguration
    {
        public string PageTitle { get; set; }
        public string HomePageLogoUri { get; set; }
        public string FaviconUri { get; set; }
        public string IdentityAdminBaseUrl { get; set; }
        public string AdministrationRole { get; set; }

        public string Theme { get; set; }

        public string CustomThemeCss { get; set; }
    }
    public class RegisterConfiguration
    {
        public bool Enabled { get; set; } = true;
    }
    public class LoginConfiguration
    {
        public LoginResolutionPolicy ResolutionPolicy { get; set; } = LoginResolutionPolicy.Username;
    }
}