using System.Linq;

namespace CD
{
    class Token
    {
        private readonly string token;

        Token()
        {
            token = System.IO.File.ReadLines("token.txt").First();
        }
    }
}
