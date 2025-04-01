using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEE
{
    internal static class Extensions
    {
        public static string removeWhiteSpace(this string text) => string.Join("", text.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

        public static int countLast(this string text)
        {
            char c = text.Last();
            int count = 0; 

            for (int i = text.Length - 2; i >= 0; i--)
            {
                if (text[i] == c)
                    count++;

                else
                    break;
            }

            return count;
        }

        public static int countLast(this string text, char c)
        {
            int count = 0;

            for (int i = text.Length - 1; i >= 0; i--)
            {
                if (text[i] == c)
                    count++;

                else
                    break;
            }

            return count;
        }
    }
}
