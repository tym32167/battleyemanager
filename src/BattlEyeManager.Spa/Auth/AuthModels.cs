using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BattlEyeManager.Spa.Auth
{
    public class AuthOptions
    {       
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 300; // время жизни токена - 5 часов
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}