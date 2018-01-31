using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CD
{
    public static class ImagesUrl
    {
        private static Dictionary<string, string> urlDictionary;

        static ImagesUrl()
        {
            urlDictionary = new Dictionary<string, string>();
            Read();
        }

        private static void Read()
        {
            if (!File.Exists("images/images.txt"))
                throw new InvalidDataException("Images file not found");
            var lines = File.ReadAllLines("images/images.txt").ToList();
            for (var i = 0; i < lines.Count; i++)
            {
                if (i == lines.Count - 1)
                    break;
                urlDictionary.Add(lines[i],lines[i+1]);
                i++;
            }
        }

        public static string GetUrl(string name)
        {
            string value;
            return urlDictionary.TryGetValue(name, out value) ? value : null;
        }

        public static string Sanitize(this string input)
        {
            return input.Replace("<", "").Replace(">", "").Replace(":", "").Replace("\"", "").Replace("/", "")
                .Replace("\\", "").Replace("|", "").Replace("?", "").Replace("*", "");
        }
    }
}
