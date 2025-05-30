using System.Security.Claims;

public class UserService
{
    public List<Claim> GetUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("ModuleAccess", "Dashboard"),
            new Claim("ModuleAccess", "Reports"),
        };
        return claims;
    }
}
