using System.IdentityModel.Tokens.Jwt;

namespace Web.Application.Helpers;

public static class JwtValidator
{
    public static bool ValidateJwt(this string jwt)
    {
        var claims = ParseJwtPayload.ParseClaimsFromJwt(jwt);

        var exp = claims.FirstOrDefault(x => x.Type == "exp");
       
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(jwt);
        var tokenS = jsonToken as JwtSecurityToken;

        if (long.Parse(exp!.Value) < DateTimeOffset.Now.ToUnixTimeSeconds())
        {
            Console.WriteLine("token has expired");
            return false;
        }
        

        if (tokenS.Header.Alg is "" or "None")
        {
            Console.WriteLine("invalid signature alg");
            return false;
        }

        return true;
    }
}