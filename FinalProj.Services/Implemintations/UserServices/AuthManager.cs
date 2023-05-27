using FinalApp.Api.Authentication;
using FinalApp.ApiModels.Auth.Models;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalProj.ApiModels.Auth.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalProj.Services.Implemintations.UserServices
{
    public class AuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<AuthResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                if (int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays))
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);

                    var authResult = new AuthResult
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                        Success = true
                    };
                }
                else
                {
                    return new AuthResult
                    {
                        Success = false,
                        ErrorMessage = "Invalid configuration for refresh token validity"
                    };
                }
            }
            return new AuthResult
            {
                Success = false,
                ErrorMessage = "Unauthorized"
            };

        }

        public async Task<AuthResult> Register(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User already exists!"
                };
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User creation failed! Please check user details and try again."
                };
            }

            return new AuthResult
            {
                Success = true,
                ErrorMessage = null
            };
        }

        public async Task<AuthResult> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User already exists!"
                };
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "User creation failed! Please check user details and try again."
                };
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return new AuthResult
            {
                Success = true,
                ErrorMessage = null
            };
        }

        public async Task<AuthResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Invalid client request"
                };
            }

            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Invalid access token or refresh token"
                };
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new AuthResult
                {
                    Success = false,
                    ErrorMessage = "Invalid access token or refresh token"
                };
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                ErrorMessage = null,
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };
        }




        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}




//public async Task<T> FindByLoginAsync(string login)
//{
//    try
//    {
//        StringValidator.CheckIsNotNull(login);
//        var user = await _repository.ReadAll().FirstOrDefaultAsync(user => user.Login == login);

//        ObjectValidator<T>.CheckIsNotNullObject(user);
//        return user;
//    }
//    catch (ArgumentNullException argNullException)
//    {
//        throw new ArgumentException("no records found in the database.\n\r" +
//            $"Error: {argNullException}");
//    }
//    catch (Exception exception)
//    {
//        throw new Exception(" internal server error.\n\r" +
//            $"Error: {exception}");
//    }
//}

//public async Task<bool> CheckPasswordAsync(T user, string password)
//{
//    try
//    {
//        ObjectValidator<T>.CheckIsNotNullObject(user);
//        StringValidator.CheckIsNotNull(password);

//        var hashedPassword = HashHelper.HashPassword(password);

//        return user.Password == hashedPassword;
//    }
//    catch (ArgumentNullException argNullException)
//    {
//        return false;
//    }
//    catch (Exception exception)
//    {
//        return false;
//    }

//}
