using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProj.ApiModels.Auth.Constants
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
