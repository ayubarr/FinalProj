using FinalApp.ApiModels.Auth.Models;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalProj.ApiModels.Auth.Models;

namespace FinalProj.Services.Interfaces
{
    public interface IAuthManager<TModel>
        where TModel : ApplicationUser
    {
        public Task<IBaseResponse<AuthResultStruct>> Login(LoginModel model);
        public Task<IBaseResponse<bool>> Register(RegisterModel model);
        public Task<IBaseResponse<bool>> RegisterAdmin(RegisterModel model);
        public Task<IBaseResponse<AuthResultStruct>> RefreshToken(TokenModel tokenModel);
        public Task<IBaseResponse<bool>> RevokeRefreshTokenByUsernameAsync(string username);
        public Task<IBaseResponse<bool>> RevokeAllRefreshTokensAsync();
    }
}
