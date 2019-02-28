namespace BattlEyeManager.Spa.Infrastructure.Utils
{
    public static class StringExtensions
    {
        public static string DefaultIfNullOrEmpty(this string source, string @default)
        {
            if (!string.IsNullOrWhiteSpace(source)) return source;
            return @default;
        }
    }
}