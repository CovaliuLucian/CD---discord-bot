using System.Text.RegularExpressions;

namespace CD
{
    public static class FancyText
    {
        public static string Fancy(this string input)
        {
            input = input.ToLower();
            var output = "";
            foreach (var ch in input)
                switch (ch)
                {
                    case 'a':
                        output += ":a: ";
                        break;
                    case 'b':
                        output += ":b: ";
                        break;
                    case ' ':
                        output += "        ";
                        break;
                    case '!':
                        output += ":exclamation:";
                        break;
                    case '?':
                        output += ":question:";
                        break;
                    default:
                        if (Regex.IsMatch(ch.ToString(), "[a-zA-Z]"))
                            output += $":regional_indicator_{ch}:";
                        else
                            output += ch;
                        break;
                }
            return output;
        }
    }
}