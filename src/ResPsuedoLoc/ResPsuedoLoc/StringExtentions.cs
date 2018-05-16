using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ResPsuedoLoc
{
    public static class StringExtentions
    {
        // https://stackoverflow.com/a/15111719/1755
        public static IEnumerable<string> GetGraphemeClusters(this string s)
        {
            var enumerator = StringInfo.GetTextElementEnumerator(s);
            while (enumerator.MoveNext())
            {
                yield return (string)enumerator.Current;
            }
        }

        public static string ReverseGraphemeClusters(this string s)
        {
            return string.Join("", GetGraphemeClusters(s).Reverse().ToArray());
        }
    }
}
