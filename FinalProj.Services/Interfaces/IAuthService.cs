namespace FinalApp.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> AccessToken(string email, string password);
    }
}
