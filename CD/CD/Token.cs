using System.Linq;

namespace CD
{
    internal class Token
    {
        private readonly string _token;

        public Token()
        {
            _token = System.IO.File.ReadLines("token.txt").First();
        }

        public string GetToken()
        {
            return _token;
        }
    }
}
