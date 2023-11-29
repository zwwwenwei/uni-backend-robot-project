using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using sit331_4._2.Persistence;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;

namespace sit331_4._2.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            base.Response.Headers.Add("WWW-Authenticate", @"Basic realm=""Access to the robot controller.""");
            var authHeader = base.Request.Headers["Authorization"].ToString();
            // Authentication logic will be here

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic "))
            {
                var base64Credentials = authHeader.Substring("Basic ".Length).Trim();
                try
                {
                    
                    byte[] credentials = Convert.FromBase64String(base64Credentials);
                    var base64String = Encoding.UTF8.GetString(credentials);

                    var credentialParts = base64String.Split(':');

                    if (credentialParts.Length >= 2)
                    {
                        var email = credentialParts[0];
                        var password = credentialParts[1];

                        var users = UserDataAccess.GetUserModelsByEmail(email);
                        var user = users.FirstOrDefault();

                        if (user == null)
                        {
                            Response.StatusCode = 401;
                            return Task.FromResult(AuthenticateResult.Fail("$\"User with {email} doesn't exist."));
                        }

                        var hasher = new PasswordHasher<UserModel>();
                        var pwVerificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash, password);

                        if (pwVerificationResult == PasswordVerificationResult.Success)
                        {
                            var claims = new List<Claim>
                        {
                            new Claim("name", $"{user.FirstName} {user.FirstName}")
                        };
                            if (user.Role != null)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                            }

                            var identity = new ClaimsIdentity(claims, "Basic");
                            var claimsPrincipal = new ClaimsPrincipal(identity);
                            var authTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

                            return Task.FromResult(AuthenticateResult.Success(authTicket));
                        }
                        else
                        {
                            return Task.FromResult(AuthenticateResult.Fail("Verify Password hash fail"));
                        }
                    }
                }
                catch
                {
                    
                }
            }
            /*else
            {*/
                Response.StatusCode = 401;
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization header."));
            //}
        }
    }
}
