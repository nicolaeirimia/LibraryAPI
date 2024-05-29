using System.Collections.Concurrent;

namespace LibraryAPI
{
    public static class TokenStore
    {
        private static readonly ConcurrentDictionary<string, DateTime> _tokens = new ();

        public static string GenerateToken()
        {
            var token = Guid.NewGuid().ToString();
            _tokens[token] = DateTime.UtcNow.AddMinutes(5);
            return token;
        }

        public static bool ValidateToken(string token)
        {
            if (_tokens.TryGetValue(token, out var expiration))
            {
                if (expiration > DateTime.UtcNow)
                {
                    return true;
                }

                _tokens.TryRemove(token, out _); 
            }

            return false;
        }
    }
}
