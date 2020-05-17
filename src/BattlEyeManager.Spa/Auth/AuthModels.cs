using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BattlEyeManager.Spa.Auth
{
    public class AuthOptions
    {
        private static string KEY = Guid.NewGuid().ToString();   // ключ для шифрации
        public const int LIFETIME = 300; // время жизни токена - 5 часов
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}