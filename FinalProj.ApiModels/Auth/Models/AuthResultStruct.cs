namespace FinalProj.ApiModels.Auth.Models
{
    public struct AuthResultStruct
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
