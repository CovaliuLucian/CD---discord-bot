using System.Linq;

namespace CD
{
    internal class Token
    {
        private readonly string token;

        public Token()
        {
            token = System.IO.File.ReadLines("token.txt").First();
        }

        public string GetToken()
        {
            return token;
        }
    }
}
