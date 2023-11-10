using System.Security.Claims;

namespace BlazorApp.Shared.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static UserModel Get(this ClaimsPrincipal principal)
        {
            var user = new UserModel();

            foreach (var claim in principal.Claims)
            {
                if (claim.Type == "name")
                    user.Username = claim.Value;

                if (claim.Type == "email")
                    user.Email = claim.Value;
            }
            
            return user;
        }
    }
}
