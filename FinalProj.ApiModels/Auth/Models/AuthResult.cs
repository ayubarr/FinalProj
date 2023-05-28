namespace FinalProj.ApiModels.Auth.Models
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
