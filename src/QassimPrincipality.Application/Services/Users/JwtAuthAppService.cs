using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QassimPrincipality.Application.Users
{
    public class JwtAuthAppService
    {
        private readonly UserAppService _userAppService;
        private IConfiguration _configuration;
        private readonly UserTokensAppService _userTokensAppService;
        private readonly AuthAppService _authAppService;
        private static double _sessionDuration = 5;
        public JwtAuthAppService(UserAppService userAppService,
            IConfiguration configuration,
            UserTokensAppService userTokensAppService,
            AuthAppService authAppService)
        {
            _userAppService = userAppService;
            _configuration = configuration;
            _userTokensAppService = userTokensAppService;
            _authAppService = authAppService;
        }

        public async Task<string> RefreshToken(string token)
        {
            var result = GetTokenPrincipal(token);

            var principal = result.principal;
            var tokenValue = result.securityToken as JwtSecurityToken;

            var usernameFromToken = principal.Identity?.Name;
            //var currentUsername = User.Identity.Name;
            if (usernameFromToken == _authAppService.CurrentUserName)
            {
                var currentUserId = _authAppService.GetClaimValue("nameidentifier");
                if (await _userAppService.IsActiveUser(_authAppService.CurrentUserName))
                {
                    string jtiFromToken = tokenValue.Claims.FirstOrDefault(claim => claim.Type == "jti")?.Value;
                    UserTokensDto savedRefreshToken = await _userTokensAppService.GetUserToken(usernameFromToken, jtiFromToken);
                    if (savedRefreshToken != null)
                    {
                        if (savedRefreshToken.LoginProvider == jtiFromToken)
                        {
                            await _userTokensAppService.DeleteAsync(savedRefreshToken);
                            var tokenString = await GenerateJWTToken(usernameFromToken);
                            return tokenString;
                        }
                    }
                }
            }
            return String.Empty;
        }

        public async Task<string> GenerateJWTToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claim = new List<Claim>();
            ClaimDto userClaim = await _userAppService.GetClaimByUsername(username);
            if (userClaim != null)
            {
                if (userClaim.UserRole != null && userClaim.UserRole.Count > 0)
                {
                    claim.Add(new Claim("userId", userClaim.UserId != Guid.Empty ? userClaim.UserId.ToString() : ""));
                    claim.Add(new Claim("fullName", userClaim.FullName ?? ""));
                    claim.Add(new Claim("fullNameAr", userClaim.FullNameAr ?? ""));
                    claim.Add(new Claim("email", userClaim.Email ?? ""));
                    claim.Add(new Claim("departement", userClaim.Departement ?? ""));
                    claim.Add(new Claim("departementAr", userClaim.DepartementAr ?? ""));
                    claim.Add(new Claim("jobTitle", userClaim.JobTitle ?? ""));
                    claim.Add(new Claim("jobTitleAr", userClaim.JobTitleAr ?? ""));
                    claim.Add(new Claim("username", username));
                    claim.Add(new Claim("sub", username));
                    claim.Add(new Claim("nbf", DateTime.Now.AddSeconds(5).Ticks.ToString()));
                    claim.Add(new Claim("iat", DateTime.Now.Ticks.ToString()));
                    string JTI = Guid.NewGuid().ToString();
                    claim.Add(new Claim("jti", JTI));
                    claim.Add(new Claim("nameidentifier", userClaim.UserId.ToString()));

                    if (userClaim.UserRole != null && userClaim.UserRole.Count > 0)
                    {
                        List<string> roles = new List<string>();
                        foreach (var role in userClaim.UserRole)
                        {
                            if (!string.IsNullOrEmpty(role))
                            {
                                roles.Add(role);
                            }
                        }
                        claim.Add(new Claim("roles", string.Join(",", roles.ToArray())));
                    }

                    await _userTokensAppService.InsertAsync(new UserTokensDto { UserId = userClaim.UserId, Name = username, LoginProvider = JTI });

                    var tokenOptions = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claim, null, DateTime.Now.AddMinutes(_sessionDuration), signingCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return tokenString;
                }
                else
                {
                    return Guid.Empty.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };
            return tokenValidationParameters;
        }

        private (ClaimsPrincipal principal, SecurityToken securityToken) GetTokenPrincipal(string token)
        {
            var ValidationParameters = GetTokenValidationParameters();
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, ValidationParameters, out SecurityToken securityTokenValue);
            JwtSecurityToken jwtSecurityToken = securityTokenValue as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return (principal, securityTokenValue);
        }
    }
}