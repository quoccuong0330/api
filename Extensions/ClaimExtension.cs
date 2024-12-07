using System.Security.Claims;

namespace WebAPI.Extensions;

public static class ClaimExtension {
    public static string getUserName(this ClaimsPrincipal user) {
        return user.Claims.SingleOrDefault(x=>
            x.Type.Equals("https://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}